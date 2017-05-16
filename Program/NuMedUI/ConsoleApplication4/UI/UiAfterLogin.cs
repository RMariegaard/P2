using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender
{
    public partial class UIAfterLogin : Form
    {
        private RecommenderSystem _recommender;
        private int _userID;
        private Dictionary<int, RoskildeArtist> _roskildeArtists;
        private List<RoskildeArtist> _hardSelected;

        public UIAfterLogin(int userId, RecommenderSystem Recommender)
        {
            this._userID = userId;
            this._recommender = Recommender;

            _roskildeArtists = Recommender.RoskildeArtists;
            _hardSelected = new List<RoskildeArtist>();

            InitializeComponent();

            GreetingLabel.Text = $"Welcome: {userId}";

            SearchBar.Text = "Search";
            SearchBar.ForeColor = Color.LightGray;
            SearchBar.GotFocus += new EventHandler(RemoveText);
            
            var artists = _roskildeArtists.Values.OrderBy(x => x.Name);
            foreach (RoskildeArtist artist in artists)
            {
                RoskildeArtistsList.Items.Add(artist.Name);
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            if (SearchBar.Text == "Search")
            {
                SearchBar.Text = "";
                SearchBar.ForeColor = Color.Black;
            }
        }

        private void GetRecommendationButton_Click(object sender, EventArgs e)
        {
            //Shows scheduele windows
            UiScheduele scheduele = new UiScheduele(_userID, _recommender, _hardSelected);
            scheduele.Show();
            this.Close();
        }

        private void SeachBar_TextChanged(object sender, EventArgs e)
        {
            //Find mathches from searhced text and display only the matches in the list
            RoskildeArtistsList.Items.Clear();
            var searchedText = SearchBar.Text.ToUpper();
            var matches = _roskildeArtists.Values.Where(x => x.Name.ToUpper().Contains(searchedText));
            foreach (RoskildeArtist item in matches.OrderBy(x => x.Name))
            {
                RoskildeArtistsList.Items.Add(item.Name);
            }

            //Set checkpoint for artists that allready have been selected
            foreach (RoskildeArtist artist in _hardSelected)
            {
                RoskildeArtistsList.SetItemChecked(RoskildeArtistsList.Items.IndexOf(artist.Name), true);
            }
        }
        
        private void RoskildeArtistsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Add previous selected artists that is not visible because of search
            //to an empty hard selected list
            List<RoskildeArtist> temp = new List<RoskildeArtist>();
            foreach (RoskildeArtist item in _hardSelected)
            {
                if (!RoskildeArtistsList.Items.Contains(item.Name))
                {
                    temp.Add(item);
                }
            }
            _hardSelected.Clear();
            temp.ForEach(x => _hardSelected.Add(x));
            
            //Add visible selected artists and adds them to hardselected
            foreach (var selectedArtistName in RoskildeArtistsList.CheckedItems)
            {
                //Object is string
                string name = selectedArtistName as string;
                if (name != null)
                {
                    RoskildeArtist artist = _roskildeArtists.Values.First(x => x.Name == name);
                    _hardSelected.Add(artist);
                }
            }

            //adds all hard selected artist to list view of selected artists
            hardSelectedListView.Items.Clear();
            foreach (RoskildeArtist item in _hardSelected)
            {
                hardSelectedListView.Items.Add(item.Name);
            }
        }

    }

}
