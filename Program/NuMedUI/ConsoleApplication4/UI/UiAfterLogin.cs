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
        RecommenderSystem Recommender;
        private int _userID;
        List<RoskildeArtist> RoskildeNames;
        List<string> Names;
        List<RoskildeArtist> HardSelected;

        public UIAfterLogin(int userId, RecommenderSystem Recommender)
        {
            this._userID = userId;
            this.Recommender = Recommender;

            RoskildeNames = Recommender.GetRoskildeArtists();
            HardSelected = new List<RoskildeArtist>();

            InitializeComponent();

            GreetingLabel.Text = $"Welcome: {userId}";

            SeachBar.Text = "Seach";
            SeachBar.ForeColor = Color.LightGray;
            SeachBar.GotFocus += new EventHandler(RemoveText);
            
            Names = new List<string>();

            RoskildeNames.OrderBy(x => x.Name).ToList().ForEach(x => Names.Add(x.Name));
            foreach (string item in Names)
            {
                RoskildeArtistsList.Items.Add(item);
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            if (SeachBar.Text == "Seach")
            {
                SeachBar.Text = "";
                SeachBar.ForeColor = Color.Black;
            }
        }

        private void GetRecommendationButton_Click(object sender, EventArgs e)
        {
            UiScheduele Scheduele = new UiScheduele(_userID, Recommender, HardSelected);
            Scheduele.Show();
            this.Close();
        }

        private void SeachBar_TextChanged(object sender, EventArgs e)
        {
            Names.RemoveRange(0, Names.Count());
            RoskildeArtistsList.Items.Clear();

            RoskildeNames.Where(x => x.Name.ToUpper().Contains(SeachBar.Text.ToUpper())).OrderBy(x => x.Name).ToList().ForEach(x => Names.Add(x.Name));
            foreach (string item in Names)
            {
                RoskildeArtistsList.Items.Add(item);
            }

            foreach (RoskildeArtist artist in HardSelected)
            {
                if (RoskildeArtistsList.Items.IndexOf(artist.Name) != -1)
                {
                    RoskildeArtistsList.SetItemChecked(RoskildeArtistsList.Items.IndexOf(artist.Name), true);
                }
            }
        }
        
        private void RoskildeArtistsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<RoskildeArtist> Temp = new List<RoskildeArtist>();

            foreach (RoskildeArtist item in HardSelected)
            {
                if (!RoskildeArtistsList.Items.Contains(item.Name))
                {
                    Temp.Add(item);
                }
            }
            
            HardSelected.Clear();
            Temp.ForEach(x => HardSelected.Add(x));

            foreach (RoskildeArtist Artist in RoskildeNames)
            {
                foreach (var SelectedArtist in RoskildeArtistsList.CheckedItems)
                {
                    if (Artist.Name == SelectedArtist.ToString())
                    {
                        HardSelected.Add(Artist);
                    }
                }
            }

            listView1.Items.Clear();
            foreach (RoskildeArtist item in HardSelected)
            {
                listView1.Items.Add(item.Name);
            }
        }

    }

}
