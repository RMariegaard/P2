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

        public UiScheduele(int ID, CreateRecommendations Recommender, List<RoskildeArtist> HardSelected)
        {
            this.ID = ID;
            this.Recommender = Recommender;
            this.HardSelected = HardSelected;
            HardSelected.ForEach(x => Console.WriteLine(x.Name));

            InitializeComponent();

            GUIList = new List<RecommendGUI>();
            
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            //Getting the artist the user added
            foreach (RoskildeArtist artist in HardSelected)
            {
                RecommendedArtist temp = new RecommendedArtist(artist);
                GUIList.Add(new RecommendGUI(temp, "HardAdd"));
            }
            //Getting Recommendations
            Recommender.Recommender(ID);
            foreach (var artist in Recommender.GetCollabRecommendedArtists())
            {
                GUIList.Add(new RecommendGUI(artist.Value, "Collab"));
            }
            foreach (var artist in Recommender.GetcontentRecommendedArtists())
            {
                GUIList.Add(new RecommendGUI(artist.Value, "Content"));
            }

            //Adding Buttons
            List<DayOfWeek> Days = new List<DayOfWeek>();
            foreach (RecommendGUI item in GUIList)
            {
                if (!Days.Contains(item.artist.TimeOfConcert.DayOfWeek))
                {
                    Days.Add(item.artist.TimeOfConcert.DayOfWeek);
                }
            }
            
            int top = 5;
            int left = 5;

            foreach (DayOfWeek item in Days)
            {
                Button button = new Button();
                button.Left = left;
                button.Top = top;
                button.Text = item.ToString();
                button.Name = item.ToString();
                button.Click += new EventHandler(NewButton_Click);
                ButtonPanel.Controls.Add(button);
                left += button.Width + 2;
            }
            
            //Adding to the Scheduele window
            int i = 0;
            foreach (RecommendGUI test in GUIList.OrderBy(x => x.TimeOfConcertLabel.Text).ToList())
            {
                test.calcLocation(new Point(5,105*i), new Size(400, 100));
                panel1.Controls.Add(test.Background);
                i++;
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            panel1.Controls.Clear();

            int counter = 0;
            foreach (RecommendGUI item in GUIList.Where(x => x.artist.TimeOfConcert.DayOfWeek.ToString() == btn.Name).OrderBy(x => x.TimeOfConcertLabel.ToString()))
            {
                item.calcLocation(new Point(5, 105 * counter), new Size(400, 100));
                panel1.Controls.Add(item.Background);
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
        public Label Scene;
        public Panel Background;
        public Color color;
        public Rectangle Rect;
        public string RatingFrom;
        public RecommendedArtist artist;

        public RecommendGUI(RecommendedArtist artist, string RatingFrom)
        {
            this.artist = artist;
            this.RatingFrom = RatingFrom;
            Background = new Panel();
            Picture = new PictureBox();
            NameLabel = new Label();
            RatingLabel = new Label();
            TimeOfConcertLabel = new Label();
            Scene = new Label();
            color = new Color();
<<<<<<< HEAD
            color = SelectColor;

=======

            if (RatingFrom == "Collab")
            {
                RatingLabel.Text = $"Collaborative: {artist.CollaborativeFilteringRating}";
                if ((int)((255 / 10) * artist.CollaborativeFilteringRating) > 255)
                    color = Color.FromArgb(255, 255, 255);
                else
                    color = Color.FromArgb(255, (int)((255 / 10) * artist.CollaborativeFilteringRating), (int)((255 / 10) * artist.CollaborativeFilteringRating));
            }
            else if (RatingFrom == "Content")
            {
                RatingLabel.Text = $"ContentBased: { artist.ContentBasedFilteringRating}";
                color = Color.FromArgb(255, (int)(255 * artist.ContentBasedFilteringRating), (int)(255 * artist.ContentBasedFilteringRating));
            }
            else if (RatingFrom == "HardAdd")
            {
                RatingLabel.Text = "You have added this";
                color = Color.FromArgb(255, 255, 255);
            }
            
>>>>>>> origin/master
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            Background.BackColor = color;
            try
            {
                Picture.Image = Image.FromFile(startupPath + $"\\pics\\{artist.Name.ToUpper()}.png");
            }
            catch (FileNotFoundException)
            {
                Picture.Image = Image.FromFile(startupPath + @"\Images\UnkownImage.jpg");
            }
            Scene.Visible = true;
            Scene.AutoSize = true;
            NameLabel.Visible = true;
            NameLabel.AutoSize = true;
            RatingLabel.Visible = true;
            RatingLabel.AutoSize = true;
            TimeOfConcertLabel.Visible = true;
            TimeOfConcertLabel.AutoSize = true;


            Scene.Text = artist.Scene;
            NameLabel.Text = artist.Name;

            TimeOfConcertLabel.Text = artist.TimeOfConcert.ToString();
            
        }
        
        public void calcLocation(Point TopLeft, Size size)
        {
            Background.Location = TopLeft;
            Background.Size = size;
            Background.BackColor = color;

            Rect = new Rectangle(TopLeft, size);
            Picture.Location = new Point(0, 0);
            Picture.Size = new Size(size.Height, size.Height);

            NameLabel.Location = new Point(size.Height + 2, 5);
            RatingLabel.Location = new Point(size.Height + 2, 25);
            TimeOfConcertLabel.Location = new Point(size.Height + 2, 45);

            Background.Controls.Add(Picture);
            Background.Controls.Add(NameLabel);
            Background.Controls.Add(RatingLabel);
            Background.Controls.Add(TimeOfConcertLabel);

        }
    }
}
