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

            foreach (RoskildeArtist artist in HardSelected)
            {
                RecommendedArtist temp = new RecommendedArtist(artist);
                temp.CollaborativeFilteringRating = 10.00;
                temp.ContentBasedFilteringRating = 10.00;
                GUIList.Add(new RecommendGUI(temp, Color.Blue));
            }

            Recommender.Recommender(ID);
            foreach (var artist in Recommender.GetCollabRecommendedArtists())
            {
                GUIList.Add(new RecommendGUI(artist.Value, Color.Green));
            }
            foreach (var artist in Recommender.GetcontentRecommendedArtists())
            {
                GUIList.Add(new RecommendGUI(artist.Value, Color.Orange));
            }

            int i = 0;
            foreach (RecommendGUI test in GUIList)
            {
                test.calcLocation(new Point(5,105*i), new Size(400, 100));
                panel1.Controls.Add(test.Background);
                i++;
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

        public RecommendGUI(RecommendedArtist artist, Color SelectColor)
        {
            Background = new Panel();
            Picture = new PictureBox();
            NameLabel = new Label();
            RatingLabel = new Label();
            TimeOfConcertLabel = new Label();
            Scene = new Label();
            color = new Color();
            color = SelectColor;

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
            
            
            try
            {
                Scene.Text = artist.Scene;
            }
            catch (Exception)
            {
                Scene.Text = "Scene NULL";
            }
            
            try
            {
                NameLabel.Text = artist.Name;
            }
            catch (Exception)
            {
                NameLabel.Text = "Insert name here";
            }

            TimeOfConcertLabel.Text = artist.TimeOfConcert.ToString();
            
            RatingLabel.Text = $"ContentBased: {artist.ContentBasedFilteringRating}\t     Collaborative: {artist.CollaborativeFilteringRating}";
            
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
