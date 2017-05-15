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
    public partial class UserBaselineRatings : Form
    {
        Label artistNameLabel;
        Label infoLabel;
        Button ratingButton;
        Dictionary<int, Userartist> theRatedArtists;
        RecommenderSystem Recommender;
        Random randomNumber;
        RadioButton[] ratingButtons;
        Artist artist;

        public UserBaselineRatings(string username, RecommenderSystem Recommender)
        {
            artistNameLabel = new Label();
            ratingButton = new Button();
            infoLabel = new Label();

            InitializeComponent();

            randomNumber = new Random();
            this.Recommender = Recommender;
            theRatedArtists = new Dictionary<int, Userartist>();

            //Info label
            infoLabel.Text = $"Please rate the following artist. You have recommended: {theRatedArtists.Count}/10";
            infoLabel.Location = new Point(5, 5);
            infoLabel.AutoSize = true;

            //Artist name label
            artistNameLabel.Location = new Point(5, infoLabel.Size.Height + 5);
            artistNameLabel.AutoSize = true;
            
            //Rating buttons
            ratingButtons = new RadioButton[11];
            ////First 10 buttons
            for (int i = 0; i < 10; i++)
            {
                ratingButtons[i] = new RadioButton();
                ratingButtons[i].Text = $"{i}";
                ratingButtons[i].Location = new Point(i*50 + 5, artistNameLabel.Size.Height + infoLabel.Size.Height + 5);
                ratingButtons[i].AutoSize = true;
            }
            ////Lastbutton
            ratingButtons[10] = new RadioButton();
            ratingButtons[10].Text = "Dont know";
            ratingButtons[10].Location = new Point(10 * 50 + 5, artistNameLabel.Size.Height + infoLabel.Size.Height + 5);
            ratingButtons[10].AutoSize = true;
            
            //Button
            ratingButton.Text = "Rate!";
            ratingButton.Location = new Point(5, artistNameLabel.Size.Height + ratingButtons[0].Size.Height + infoLabel.Size.Height + 10);
            ratingButton.Size = new Size(100, 40);
            ratingButton.Click += new EventHandler(ratingButton_Click);
            ratingButton.Visible = true;

            //Window size
            Height = 800;
            Width = 1000;

            //Add to window
            Controls.Add(artistNameLabel);
            Controls.Add(ratingButton);
            Controls.Add(infoLabel);
            foreach (RadioButton item in ratingButtons)
            {
                Controls.Add(item);
            }

            //The first artist for recommendation
            updateArtist();
        }
        
        private void doneRating()
        {
            
        }

        public void ratingButton_Click(object sender, EventArgs e)
        {
            RadioButton selected = ratingButtons.First(x => x.Checked == true);
            int rating;

            if (int.TryParse(selected.Text, out rating))
            {
                theRatedArtists.Add(artist.Id, new Userartist(artist.Id, rating, artist));

                if (theRatedArtists.Count == 10)
                    doneRating();
                else
                {
                    infoLabel.Text = $"Please rate the following artist. You have recommended: {theRatedArtists.Count}/10";
                    updateArtist();
                }
            }
            else
            {
                updateArtist();
            }
        }

        private void updateArtist()
        {
            bool foundArtist = false;
            int ranNum;
            while (!foundArtist)
            {
                ranNum = randomNumber.Next(0, Recommender.Artists.Count);
                if (Recommender.Artists.ContainsKey(ranNum) && !theRatedArtists.ContainsKey(ranNum))
                {
                    artist = Recommender.Artists[ranNum];
                    foundArtist = true;
                }
            }
            artistNameLabel.Text = artist.Name;
            artistNameLabel.AutoSize = true;
        }
    }
}
