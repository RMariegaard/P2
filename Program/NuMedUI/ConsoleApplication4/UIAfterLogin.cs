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
        List<string> RecTekst;
        List<string> RoskildeNames;

        public UIAfterLogin(int ID)
        {
            this.ID = ID;
            Recommender = new CreateRecommendations();
            ReadFileGetRoskildeArtists();

            InitializeComponent();

            GreetingLabel.Text = $"Welcome: {ID}";
            
            foreach (string item in RoskildeNames.OrderBy(x => x))
            {
                RoskildeArtistsList.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            RecTekst = Recommender.Recommender(ID);
            foreach (string item in RecTekst)
            {
                AddStringToList(item);
            }
            */
        }

        private void HardAddButton_Click(object sender, EventArgs e)
        {
            foreach (var item in RoskildeArtistsList.SelectedItems)
            {
                AddStringToList(item.ToString());
            }
        }

        private void ReadFileGetRoskildeArtists()
        {
            RoskildeNames = Recommender.LoadFiles();
        }

        private void AddStringToList(string text)
        {
            /*
            if (!RecommendationsList.Items.Contains(text))
            {
                RecommendationsList.Items.Add(text);
            }
            */
        }

        private void RoskildeArtistsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
