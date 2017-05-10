using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender
{
    public partial class UiScheduele : Form
    {
        CreateRecommendations Recommender;
        int ID;
        List<RoskildeArtist> HardSelected;
        List<RecommendGUI> GUIList;

        List<SchedueleElement> Elements;
        Scheduele FullScheduele;
        
        public UiScheduele(int ID, CreateRecommendations Recommender, List<RoskildeArtist> HardSelected)
        {
            //Starting a new thread to show loading screen
            LoadingScreen loading = new LoadingScreen("Getting your recommendations");
            Thread loadThread = new Thread(() => loading.ShowDialog());
            loadThread.Start();

            //Init
            this.ID = ID;
            this.Recommender = Recommender;
            this.HardSelected = HardSelected;
            
            InitializeComponent();

            Elements = new List<SchedueleElement>();
            FullScheduele = new Scheduele();

            label1.Text = "Select a date from above!";
            label1.Location = new Point(Width / 2 - label1.Width / 2, label1.Location.Y);
            GUIList = new List<RecommendGUI>();
            
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            //Adding the headphones icon if the user has listed to this artist before
            foreach (RoskildeArtist artist in Recommender.GetRoskildeArtists())
            {
                foreach (var userArtist in Recommender.GetUser(ID).Artists)
                {
                    if (userArtist.Value.ThisArtist.Name.ToUpper() == artist.Name.ToUpper())
                    {
                        RecommendedArtist TempArtist = new RecommendedArtist(artist);
                        Elements.Add(new SchedueleElement(TempArtist, artist.TimeOfConcert, 60, "HeardBefore"));
                    }
                }
            }

            //Getting the artist the user added
            foreach (RoskildeArtist artist in HardSelected)
            {
                Elements.Add(new SchedueleElement(new RecommendedArtist(artist), artist.TimeOfConcert, 60, "HardAdd"));
            }

            //Getting Recommendations
            Recommender.GenerateRecommendations(ID);
            foreach (var artist in Recommender.RecommendedCollabArtists)
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, "Collab"));
            }
            foreach (var artist in Recommender.RecommendedContetArtists)
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, "Content"));
            }
            
            //Addig the individual concerts to a full scheduele
            Elements.ForEach(x => FullScheduele.AddConcert(x));
            
            //From scheduele to GUIlist, in order to show it
            foreach (SchedueleElement element in FullScheduele.Concerts)
            {
                RecommendGUI Temp = new RecommendGUI(element.Artist, element.AddedFrom, $" - {element.EndTime.TimeOfDay.ToString()}", this);
                
                if (element.Exclamation)
                {
                    element.OverlappingArtist.ForEach(x => Temp.Overlapping.Add(x));
                    Temp.Exclamation = true;
                }
                
                GUIList.Add(Temp);
            }
            
            //Adding the lock icon if the user have added this
            foreach (RecommendGUI item in GUIList)
            {
                if (item.RatingFrom == "HardAdd")
                    item.Lock = true;
            }

            //Adding Buttons
            List<DateTime> Days = new List<DateTime>();
            
            foreach (RecommendGUI item in GUIList.OrderBy(x => x.artist.TimeOfConcert))
            {
                if (!Days.Any(x => x.Month == item.artist.TimeOfConcert.Month && x.Day == item.artist.TimeOfConcert.Day))
                {
                    Days.Add(item.artist.TimeOfConcert);
                }
            }

            int top = 5;
            int left = 5;

            foreach (DateTime item in Days)
            {
                Button button = new Button();
                button.Left = left;
                button.Top = top;
                button.Text = item.DayOfWeek.ToString() + " - " + item.Date.Day;
                button.Name = item.Day.ToString();
                button.Click += new EventHandler(NewButton_Click);
                button.Tag = item.Date.Day.ToString();
                ButtonPanel.Controls.Add(button);
                button.Width += 20;
                left += button.Width + 2;
            }

            //Closeing the thead with loading screen
            loadThread.Abort();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            panel1.Controls.Clear();
            label1.Text = btn.Text;

            int counter = 0;
            foreach (RecommendGUI item in GUIList.Where(x => x.artist.TimeOfConcert.Date.Day.ToString() == btn.Tag.ToString()).OrderBy(x => x.TimeOfConcertLabel.ToString()))
            {
                item.calcLocation(new Point(5, 105 * counter), new Size(400, 100));
                panel1.Controls.Add(item.Element);
                counter++;
            }
        }
    }

    class RecommendGUI
    {
        public PictureBox Picture;
        public Label NameLabel;
        public Label RatingLabel;
        public Label TimeOfConcertLabel;
        public Label EndTimeLabel;
        public Label Scene;
        public Panel Element;
        public Color color;
        public string RatingFrom;
        public RecommendedArtist artist;
        public List<SchedueleElement> Overlapping;
        string startupPath;
        public Form SomeForm;

        public PictureBox LockIcon;
        public bool Lock;
        public PictureBox HeadphonesIcon;
        public bool Headphones;
        public PictureBox ExclamationIcon;
        public bool Exclamation;
        
        public void ExclamationHover(object sender, EventArgs e)
        {
            Panel PanelShown = new Panel();

            int i = 0;
            foreach (SchedueleElement item in Overlapping)
            {
                RecommendGUI Temp = new RecommendGUI(item.Artist, item.AddedFrom, $" - {item.EndTime.ToString()}", SomeForm);
                Temp.calcLocation(new Point(5, 105 * i), new Size(400, 100));
                PanelShown.Controls.Add(Temp.Element);
                i++;
            }

            PanelShown.AutoScroll = true;

            PictureBox pic = (PictureBox)sender;

            HoverWindow Window = new HoverWindow(PanelShown);
            Window.Location = new Point(Cursor.Position.X -10, Cursor.Position.Y -10);
            Window.StartPosition = FormStartPosition.Manual;

            Window.Show();
        }

        public RecommendGUI(RecommendedArtist artist, string RatingFrom, string EndTimeString, Form form)
        {
            SomeForm = form;
            Overlapping = new List<SchedueleElement>();

            //Init
            this.artist = artist;
            this.RatingFrom = RatingFrom;
            Element = new Panel();
            Picture = new PictureBox();
            NameLabel = new Label();
            RatingLabel = new Label();
            TimeOfConcertLabel = new Label();
            EndTimeLabel = new Label();
            Scene = new Label();
            color = new Color();
            
            //Creating color
            if (RatingFrom == "Collab")
            {
                RatingLabel.Text = $"Collaborative: {artist.CollaborativeFilteringRating}";
                if (artist.CollaborativeFilteringRating > 5)
                {
                    color = Color.FromArgb(238, 113, 3);
                }
            }
            else if (RatingFrom == "Content")
            {
                RatingLabel.Text = $"ContentBased: { artist.ContentBasedFilteringRating}";
                if (artist.ContentBasedFilteringRating > 0.5)
                {
                    color = Color.FromArgb(238, 113, 3);
                }
            }
            else if (RatingFrom == "HardAdd")
            {
                RatingLabel.Text = "You have added this";
                color = Color.FromArgb(238, 113, 3);
            }
            else if (RatingFrom == "HeardBefore")
            {
                RatingLabel.Text = "You have heard this before";
                color = Color.FromArgb(238, 113, 3);
                Headphones = true;
            }
            
            //Creating the visual
            startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);
            Element.BackColor = color;
            
            try
            {
                Picture.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + $"\\pics\\{artist.Name.ToUpper()}.png"), 100, 100);
            }
            catch (FileNotFoundException)
            {
                Picture.Image = Image.FromFile(startupPath + @"\Images\UnkownImage.jpg");
            }
            
            Scene.Text = artist.Scene;
            NameLabel.Text = artist.Name;
            EndTimeLabel.Text = EndTimeString;
            TimeOfConcertLabel.Text = artist.TimeOfConcert.TimeOfDay.ToString();

            Scene.Visible = true;
            Scene.AutoSize = true;
            NameLabel.Visible = true;
            NameLabel.AutoSize = true;
            RatingLabel.Visible = true;
            RatingLabel.AutoSize = true;
            TimeOfConcertLabel.Visible = true;
            TimeOfConcertLabel.AutoSize = true;
            EndTimeLabel.Visible = true;
            EndTimeLabel.AutoSize = true;
        }
        
        public void calcLocation(Point TopLeft, Size size)
        {
            //Panel
            Element.Location = TopLeft;
            Element.Size = size;
            Element.BackColor = color;

            //Icons
            Size IconSize = new Size(20,20);
            int Spacing = 2;
            
            if (Headphones == true)
            {
                HeadphonesIcon = new PictureBox();
                HeadphonesIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Headphones.jpg"), IconSize.Width, IconSize.Height);
                HeadphonesIcon.Location = new Point(size.Width - IconSize.Width - Spacing, Spacing);
                Element.Controls.Add(HeadphonesIcon);

                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(HeadphonesIcon, "This artist is in your playlist!");

            }
            if (Lock == true)
            {
                LockIcon = new PictureBox();
                LockIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Lock.jpg"), IconSize.Width, IconSize.Height);
                LockIcon.Location = new Point(size.Width - (IconSize.Width * 2) - (Spacing * 2), Spacing);
                Element.Controls.Add(LockIcon);
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(LockIcon, "You selected this artist!");
            }
            if (Exclamation == true)
            {
                ExclamationIcon = new PictureBox();
                ExclamationIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Exclamation.jpg"), IconSize.Width, IconSize.Height);
                ExclamationIcon.MouseHover += new EventHandler(ExclamationHover);
                ExclamationIcon.Location = new Point(size.Width - (IconSize.Width * 3) - (Spacing * 3), Spacing);
                Element.Controls.Add(ExclamationIcon);
            }
            
            //Artist picture
            Picture.Location = new Point(0, 0);
            Picture.Size = new Size(size.Height, size.Height);

            //Labels
            NameLabel.Location = new Point(size.Height + 2, 5);
            RatingLabel.Location = new Point(size.Height + 2, 25);
            TimeOfConcertLabel.Location = new Point(size.Height + 2, 45);
            EndTimeLabel.Location = new Point(size.Height + 4 + TimeOfConcertLabel.Size.Width, 45);
            Scene.Location = new Point(size.Height + 2, 65);

            //Adding to the pannel
            Element.Controls.Add(Picture);
            Element.Controls.Add(NameLabel);
            Element.Controls.Add(RatingLabel);
            Element.Controls.Add(TimeOfConcertLabel);
            Element.Controls.Add(Scene);
            Element.Controls.Add(EndTimeLabel);
        }
    }
}
