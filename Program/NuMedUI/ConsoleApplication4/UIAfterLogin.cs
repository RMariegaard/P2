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
        CreateRecommendations Recommender;
        int ID;
        List<RoskildeArtist> RoskildeNames;
        List<string> Names;

        public UIAfterLogin(int ID, CreateRecommendations Recommender)
        {
            this.ID = ID;
            this.Recommender = Recommender;

            RoskildeNames = Recommender.GetRoskildeArtists();

            InitializeComponent();

            GreetingLabel.Text = $"Welcome: {ID}";

            Names = new List<string>();

            RoskildeNames.OrderBy(x => x.Name).ToList().ForEach(x => Names.Add(x.Name));
            foreach (string item in Names)
            {
                RoskildeArtistsList.Items.Add(item);
            }
        }
        
        private void GetRecommendationButton_Click(object sender, EventArgs e)
        {
            List<RoskildeArtist> HardSelected = new List<RoskildeArtist>();
            
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

            UiScheduele Scheduele = new UiScheduele(ID, Recommender, HardSelected);
            Scheduele.Show();
            this.Close();
        }

        private void SeachBar_TextChanged(object sender, EventArgs e)
        {
            Names.RemoveRange(0, Names.Count());
            int test = RoskildeArtistsList.Items.Count;
            for(int i = 0; i < test; i++)
            {
                RoskildeArtistsList.Items.RemoveAt(0);
            }
            
            RoskildeNames.Where(x => x.Name.Contains(SeachBar.Text)).OrderBy(x => x.Name).ToList().ForEach(x => Names.Add(x.Name));
            foreach (string item in Names)
            {
                RoskildeArtistsList.Items.Add(item);
            }
        }
    }
}
