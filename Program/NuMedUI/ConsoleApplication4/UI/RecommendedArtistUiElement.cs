using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender
{
    class RecommendedArtistUiElement
    {
        public PictureBox Picture;
        public Label RatingLabel, TimeOfConcertLabel, EndTimeLabel, Scene, NameLabel;
        public Panel Element;
        public Color color;
        public string RatingFrom;
        public RecommendedArtist artist;
        public List<SchedueleElement> Overlapping;
        string startupPath;

        public PictureBox LockIcon, HeadphonesIcon, ExclamationIcon;
        public bool Lock, Headphones, Exclamation;
        
        public RecommendedArtistUiElement(RecommendedArtist artist, string RatingFrom, string EndTimeString)
        {
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
            int starNeededForColor = 8;
            if (RatingFrom == "Collab" || RatingFrom == "Content")
                if (artist.Stars >= starNeededForColor)
                    color = Color.FromArgb(238, 113, 3);
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

            //tekst
            else if (RatingFrom == "Content")
                RatingLabel.Text = $"ContentBased: { artist.Stars}";
            else if (RatingFrom == "Collab")
                RatingLabel.Text = $"Collaborative: { artist.Stars}";

            //Images of artists from file
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

            //Size, text
            Scene.Text = artist.Scene;
            NameLabel.Text = artist.Name;
            EndTimeLabel.Text = EndTimeString;
            TimeOfConcertLabel.Text = artist.TimeOfConcert.TimeOfDay.ToString();

            Scene.AutoSize = true;
            NameLabel.AutoSize = true;
            RatingLabel.AutoSize = true;
            TimeOfConcertLabel.AutoSize = true;
            EndTimeLabel.AutoSize = true;
        }

        public void calcLocation(Point TopLeft, Size size)
        {
            //Panel
            Element.Location = TopLeft;
            Element.Size = size;
            Element.BackColor = color;

            //Icons
            Size IconSize = new Size(20, 20);
            int Spacing = 2;

            if (Headphones)
            {
                HeadphonesIcon = new PictureBox();
                HeadphonesIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Headphones.jpg"), IconSize.Width, IconSize.Height);
                HeadphonesIcon.Location = new Point(size.Width - IconSize.Width - Spacing, Spacing);
                Element.Controls.Add(HeadphonesIcon);

                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(HeadphonesIcon, "This artist is in your playlist!");
            }
            if (Lock)
            {
                LockIcon = new PictureBox();
                LockIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Lock.jpg"), IconSize.Width, IconSize.Height);
                LockIcon.Location = new Point(size.Width - (IconSize.Width * 2) - (Spacing * 2), Spacing);
                Element.Controls.Add(LockIcon);
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(LockIcon, "You selected this artist!");
            }
            if (Exclamation)
            {
                ExclamationIcon = new PictureBox();
                ExclamationIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Exclamation.jpg"), IconSize.Width, IconSize.Height);
                ExclamationIcon.MouseHover += new EventHandler(ExclamationHover);
                ExclamationIcon.Location = new Point(size.Width - (IconSize.Width * 3) - (Spacing * 3), Spacing);
                Element.Controls.Add(ExclamationIcon);
            }

            //Creating stars
            for (int i = 0; i <= artist.Stars; i++)
            {
                PictureBox Star = new PictureBox();
                Star.Image = ResizeBitmap.ResizeImage(Image.FromFile(startupPath + @"\Images\Star.jpg"), IconSize.Width, IconSize.Height);
                Star.Visible = true;
                Star.Size = IconSize;
                Star.Location = new Point(size.Width - (IconSize.Width * i) - Spacing * i, size.Height - Spacing - IconSize.Height);
                Element.Controls.Add(Star);
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

        public void ExclamationHover(object sender, EventArgs e)
        {
            Panel PanelShown = new Panel();

            int i = 0;
            foreach (SchedueleElement item in Overlapping)
            {
                RecommendedArtistUiElement Temp = new RecommendedArtistUiElement(item.Artist, item.AddedFrom, $" - {item.EndTime}");
                Temp.calcLocation(new Point(5, 105 * i), new Size(400, 100));
                PanelShown.Controls.Add(Temp.Element);
                i++;
            }
            PanelShown.AutoScroll = true;

            HoverWindow Window = new HoverWindow(PanelShown);
            Window.Location = new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10);
            Window.StartPosition = FormStartPosition.Manual;
            Window.Show();
        }
    }
}
