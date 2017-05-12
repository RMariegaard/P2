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
        private PictureBox _picture;
        public Label RatingLabel, TimeOfConcertLabel, EndTimeLabel, Scene, NameLabel;
        public Panel Element;
        private Color _color;
        public ElementOrigin RatingFrom;
        public RecommendedArtist Artist;
        public List<SchedueleElement> Overlapping;
        private string _startupPath;

        public PictureBox LockIcon, HeadphonesIcon, ExclamationIcon;
        public bool Lock, Headphones, Exclamation;
        
        public RecommendedArtistUiElement(RecommendedArtist artist, ElementOrigin RatingFrom, string EndTimeString)
        {
            Overlapping = new List<SchedueleElement>();

            //Init
            this.Artist = artist;
            this.RatingFrom = RatingFrom;
            Element = new Panel();
            _picture = new PictureBox();
            NameLabel = new Label();
            RatingLabel = new Label();
            TimeOfConcertLabel = new Label();
            EndTimeLabel = new Label();
            Scene = new Label();
            _color = new Color();

            //Creating color
            int starNeededForColor = 8;
            switch (RatingFrom)
            {
                case ElementOrigin.HeardBefore:
                    RatingLabel.Text = "You have heard this before";
                    _color = Color.FromArgb(238, 113, 3);
                    Headphones = true;
                    break;

                case ElementOrigin.Collab:
                case ElementOrigin.Content:
                    if (artist.Stars >= starNeededForColor)
                        _color = Color.FromArgb(238, 113, 3);
                    break;

                case ElementOrigin.HardSelected:
                    RatingLabel.Text = "You have added this";
                    _color = Color.FromArgb(238, 113, 3);
                    break;
            }

            //Text for label
            if (ElementOrigin.Content == this.RatingFrom)
                RatingLabel.Text = $"Based on your prefrences: {artist.Stars}";
            else if (ElementOrigin.Collab == this.RatingFrom)
                RatingLabel.Text = $"Based on similar users: {artist.Stars}";

            //Images of artists from file
            _startupPath = Environment.CurrentDirectory;
            _startupPath = Path.GetDirectoryName(_startupPath);
            _startupPath = Path.GetDirectoryName(_startupPath);
            Element.BackColor = _color;

            try
            {
                _picture.Image = ResizeBitmap.ResizeImage(Image.FromFile(_startupPath + $"\\pics\\{artist.Name.ToUpper()}.png"), 100, 100);
            }
            catch (FileNotFoundException)
            {
                _picture.Image = Image.FromFile(_startupPath + @"\Images\UnkownImage.jpg");
            }

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

        public void CalcLocation(Point TopLeft, Size size)
        {
            //Panel
            Element.Location = TopLeft;
            Element.Size = size;
            Element.BackColor = _color;

            //Icons
            Size IconSize = new Size(20, 20);
            int Spacing = 2;

            if (Headphones)
            {
                HeadphonesIcon = new PictureBox();
                HeadphonesIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(_startupPath + @"\Images\Headphones.jpg"), IconSize.Width, IconSize.Height);
                HeadphonesIcon.Location = new Point(size.Width - IconSize.Width - Spacing, Spacing);
                Element.Controls.Add(HeadphonesIcon);

                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(HeadphonesIcon, "This artist is in your playlist!");
            }
            if (Lock)
            {
                LockIcon = new PictureBox();
                LockIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(_startupPath + @"\Images\Lock.jpg"), IconSize.Width, IconSize.Height);
                LockIcon.Location = new Point(size.Width - (IconSize.Width * 2) - (Spacing * 2), Spacing);
                Element.Controls.Add(LockIcon);
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(LockIcon, "You selected this artist!");
            }
            if (Exclamation)
            {
                ExclamationIcon = new PictureBox();
                ExclamationIcon.Image = ResizeBitmap.ResizeImage(Image.FromFile(_startupPath + @"\Images\Exclamation.jpg"), IconSize.Width, IconSize.Height);
                ExclamationIcon.MouseHover += new EventHandler(ExclamationHover);
                ExclamationIcon.Location = new Point(size.Width - (IconSize.Width * 3) - (Spacing * 3), Spacing);
                Element.Controls.Add(ExclamationIcon);
            }

            //Creating stars
            for (int i = 0; i <= Artist.Stars; i++)
            {
                PictureBox Star = new PictureBox();
                Star.Image = ResizeBitmap.ResizeImage(Image.FromFile(_startupPath + @"\Images\Star.jpg"), IconSize.Width, IconSize.Height);
                Star.Visible = true;
                Star.Size = IconSize;
                Star.Location = new Point(size.Width - (IconSize.Width * i) - Spacing * i, size.Height - Spacing - IconSize.Height);
                Element.Controls.Add(Star);
            }

            //Artist picture
            _picture.Location = new Point(0, 0);
            _picture.Size = new Size(size.Height, size.Height);

            //Labels
            NameLabel.Location = new Point(size.Height + 2, 5);
            RatingLabel.Location = new Point(size.Height + 2, 25);
            TimeOfConcertLabel.Location = new Point(size.Height + 2, 45);
            EndTimeLabel.Location = new Point(size.Height + 4 + TimeOfConcertLabel.Size.Width, 45);
            Scene.Location = new Point(size.Height + 2, 65);

            //Adding to the pannel
            Element.Controls.Add(_picture);
            Element.Controls.Add(NameLabel);
            Element.Controls.Add(RatingLabel);
            Element.Controls.Add(TimeOfConcertLabel);
            Element.Controls.Add(Scene);
            Element.Controls.Add(EndTimeLabel);
        }
        //Creates and shows panel in a hover window for the artists that the current artist overlap
        public void ExclamationHover(object sender, EventArgs e)
        {
            //Creates the panel 
            //Overlapping list is populated when the scheduele is created
            Panel PanelShown = new Panel();
            int i = 0;
            foreach (SchedueleElement item in Overlapping)
            {
                RecommendedArtistUiElement Temp = new RecommendedArtistUiElement(item.Artist, item.AddedFrom, $" - {item.EndTime}");
                Temp.CalcLocation(new Point(5, 105 * i), new Size(400, 100));
                PanelShown.Controls.Add(Temp.Element);
                i++;
            }
            PanelShown.AutoScroll = true;
            //Creates and shows the window with the panel
            HoverWindow Window = new HoverWindow(PanelShown);
            Window.Location = new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10);
            Window.StartPosition = FormStartPosition.Manual;
            Window.Show();
        }
    }
}
