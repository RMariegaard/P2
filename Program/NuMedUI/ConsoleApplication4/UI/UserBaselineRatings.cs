﻿using System;
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
        private Label artistNameLabel;
        private Label infoLabel;
        private Button ratingButton;
        private Button doneRatingButton;
        private Dictionary<int, Userartist> theRatedArtists;
        private RecommenderSystem Recommender;
        private Random randomNumber;
        private RadioButton[] ratingButtons;
        private Artist artist;
        private Button rateMoreButton;

        //For the search
        private TextBox SearchBar;
        private CheckedListBox ArtistList;
        private Button SearchButton;


        private int numberOfMustHaveRatings;

        public UserBaselineRatings(string username, RecommenderSystem Recommender)
        {
            artistNameLabel = new Label();
            ratingButton = new Button();
            doneRatingButton = new Button();
            infoLabel = new Label();
            SearchButton = new Button();
            SearchBar = new TextBox();
            ArtistList = new CheckedListBox();
            rateMoreButton = new Button();

            numberOfMustHaveRatings = 15;

            InitializeComponent();

            randomNumber = new Random();
            this.Recommender = Recommender;
            theRatedArtists = new Dictionary<int, Userartist>();
            
            //Info label
            infoLabel.Text = $"Please rate the following artist. You have recommended: {theRatedArtists.Count}/{numberOfMustHaveRatings}";
            infoLabel.Location = new Point(5, 5);
            infoLabel.AutoSize = true;

            //Artist name label
            artistNameLabel.Location = new Point(5, infoLabel.Size.Height + 5);
            artistNameLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            artistNameLabel.AutoSize = true;

            //Rating buttons
            ratingButtons = new RadioButton[11];
            ////First 10 buttons
            for (int i = 0; i < 10; i++)
            {
                ratingButtons[i] = new RadioButton();
                ratingButtons[i].Text = $"{i + 1}";
                ratingButtons[i].Location = new Point(i*50 + 5, artistNameLabel.Location.Y + artistNameLabel.Size.Height + 20);
                ratingButtons[i].AutoSize = true;
            }
            ////Lastbutton
            ratingButtons[10] = new RadioButton();
            ratingButtons[10].Text = "Dont know";
            ratingButtons[10].Location = new Point(10 * 50 + 5, artistNameLabel.Location.Y + artistNameLabel.Size.Height + 20);
            ratingButtons[10].AutoSize = true;
            
            //Rate button
            ratingButton.Text = "Rate!";
            ratingButton.Location = new Point(5, ratingButtons[0].Location.Y + ratingButtons[0].Size.Height + 5);
            ratingButton.Size = new Size(100, 40);
            ratingButton.Click += new EventHandler(ratingButton_Click);
            ratingButton.Visible = true;

            //Done rating button
            doneRatingButton.Text = "Create User";
            doneRatingButton.Location = new Point(5, ratingButton.Location.Y + ratingButton.Size.Height + 5);
            doneRatingButton.Click += new EventHandler(doneRating);
            doneRatingButton.Enabled = false;
            doneRatingButton.Size = ratingButton.Size;

            //Rate more buttom
            rateMoreButton.Location = new Point(5, doneRatingButton.Location.Y + doneRatingButton.Size.Height + 5);
            rateMoreButton.Size = doneRatingButton.Size;
            rateMoreButton.Text = "Rate more artists";
            rateMoreButton.Enabled = false;
            rateMoreButton.Click += new EventHandler(rateMoreButton_Click);

            //Searchbar
            SearchBar.Location = new Point(5, doneRatingButton.Location.Y + doneRatingButton.Size.Height + 5);
            SearchBar.Size = new Size(200, 20);

            //Search button
            SearchButton.Text = "Search for artist";
            SearchButton.Click += SearchButton_Click;
            SearchButton.Location = new Point(5, SearchBar.Location.Y + SearchBar.Size.Height + 5);
            SearchButton.Size = new Size(200, 20);

            //Artist list when Search
            ArtistList.Location = new Point(5, SearchButton.Location.Y + SearchButton.Size.Height + 5);
            ArtistList.Size = new Size(200, 400);
            ArtistList.SelectedIndexChanged += new EventHandler(ArtistList_SelectedIndexChanged);
            ArtistList.CheckOnClick = true;

            //Window size
            Height = 800;
            Width = 1000;

            //Add to window
            Controls.Add(artistNameLabel);
            Controls.Add(ratingButton);
            Controls.Add(doneRatingButton);
            Controls.Add(infoLabel);
            Controls.Add(rateMoreButton);
            foreach (RadioButton item in ratingButtons)
            {
                Controls.Add(item);
            }

            //The first artist for recommendation
            RandomNewArtist();
        }

        private void rateMoreButton_Click(object sender, EventArgs e)
        {
            Controls.Add(SearchButton);
            Controls.Add(SearchBar);
            Controls.Add(ArtistList);

            Controls.Remove(rateMoreButton);
        }
        
        private void doneRating(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ratingButton_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton selected = ratingButtons.First(x => x.Checked == true);

                int rating;

                if (int.TryParse(selected.Text, out rating))
                {
                    if (!theRatedArtists.ContainsKey(artist.Id))
                        theRatedArtists.Add(artist.Id, new Userartist(artist.Id, rating, artist));

                    if (theRatedArtists.Count >= numberOfMustHaveRatings)
                    {
                        infoLabel.Text = $"You are done with the requried ratings, but the more ratings we get from you, the better recommendations we can provide. You have recommended {theRatedArtists.Count}";
                        doneRatingButton.Enabled = true;
                        rateMoreButton.Enabled = true;
                    }
                    else
                    {
                        infoLabel.Text = $"Please rate the following artist. You have recommended: {theRatedArtists.Count}/{numberOfMustHaveRatings}";
                        RandomNewArtist();
                    }
                }
                else
                {
                    RandomNewArtist();
                }
            }
            catch (Exception){}
        }

        private void RandomNewArtist()
        {
            bool foundArtist = false;
            while (!foundArtist)
            {
                int ranNum = randomNumber.Next(0, Recommender.Artists.Count);
                if (Recommender.Artists.ContainsKey(ranNum) && !theRatedArtists.ContainsKey(ranNum))
                {
                    artist = Recommender.Artists[ranNum];
                    foundArtist = true;
                }
            }
            updateArtist();
        }

        private void updateArtist()
        {
            artistNameLabel.Text = artist.Name;
            artistNameLabel.AutoSize = true;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ArtistList.Items.Clear();

            foreach (var item in Recommender.Artists.Where(x => x.Value.Name.ToUpper().Contains(SearchBar.Text.ToUpper())))
            {
                ArtistList.Items.Add(item.Value.Name);
            }
        }

        private void ArtistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                artist = Recommender.Artists.Values.First(x => x.Name.ToUpper() == ArtistList.CheckedItems[0].ToString().ToUpper());
                ArtistList.SetItemCheckState(ArtistList.SelectedIndex, CheckState.Unchecked);
                //ArtistList.ClearSelected();
                updateArtist();
            }
            catch (Exception){}
        }
    }
}