using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            this.ID = ID;
            this.Recommender = Recommender;
            this.HardSelected = HardSelected;
            HardSelected.ForEach(x => Console.WriteLine(x.Name));

            InitializeComponent();

            Elements = new List<SchedueleElement>();
            FullScheduele = new Scheduele();

            label1.Text = "Select a date from above!";
            label1.Location = new Point(Width / 2 - label1.Width / 2, label1.Location.Y);
            GUIList = new List<RecommendGUI>();
            
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            //Getting the artist the user added
            foreach (RoskildeArtist artist in HardSelected)
            {
                Elements.Add(new SchedueleElement(new RecommendedArtist(artist), artist.TimeOfConcert, 60, "HardAdd"));
            }
            //Getting Recommendations
            Recommender.Recommender(ID);
            foreach (var artist in Recommender.GetCollabRecommendedArtists())
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, "Collab"));
            }
            foreach (var artist in Recommender.GetcontentRecommendedArtists())
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, "Content"));
            }
            Elements.ForEach(x => FullScheduele.AddConcert(x));
            
            //Lægger det fra scheduele til liste der bliver vist
            foreach (SchedueleElement element in FullScheduele.Concerts)
            {
                GUIList.Add(new RecommendGUI(element.Artist, element.AddedFrom, " - " + element.EndTime.TimeOfDay.ToString()));
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
        public Rectangle Rect;
        public string RatingFrom;
        public RecommendedArtist artist;

        public RecommendGUI(RecommendedArtist artist, string RatingFrom, string EndTimeString)
        {
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
            
            //Creating the visual
            string startupPath = Environment.CurrentDirectory;
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
            Element.Location = TopLeft;
            Element.Size = size;
            Element.BackColor = color;

            Rect = new Rectangle(TopLeft, size);
            Picture.Location = new Point(0, 0);
            Picture.Size = new Size(size.Height, size.Height);

            NameLabel.Location = new Point(size.Height + 2, 5);
            RatingLabel.Location = new Point(size.Height + 2, 25);
            TimeOfConcertLabel.Location = new Point(size.Height + 2, 45);
            EndTimeLabel.Location = new Point(size.Height + 4 + TimeOfConcertLabel.Size.Width, 45);

            Element.Controls.Add(Picture);
            Element.Controls.Add(NameLabel);
            Element.Controls.Add(RatingLabel);
            Element.Controls.Add(TimeOfConcertLabel);
            Element.Controls.Add(EndTimeLabel);
        }
    }
}
