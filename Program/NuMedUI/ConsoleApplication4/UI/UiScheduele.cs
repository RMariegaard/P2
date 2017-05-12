using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Recommender
{
    public partial class UiScheduele : Form
    {
        private List<RecommendedArtistUiElement> _artistGUIList;
        private List<SchedueleElement> _elements;
        private Scheduele _fullScheduele;
        
        public UiScheduele(int ID, RecommenderSystem recommender, List<RoskildeArtist> hardSelected)
        {
            //Starting a new thread to show loading screen
            LoadingScreen loading = new LoadingScreen("Getting your recommendations");
            Thread loadThread = new Thread(() => loading.ShowDialog());
            loadThread.Start();
            
            InitializeComponent();

            _elements = new List<SchedueleElement>();
            _fullScheduele = new Scheduele();

            label1.Text = "Select a date from above!";
            label1.Location = new Point(Width / 2 - label1.Width / 2, label1.Location.Y);
            _artistGUIList = new List<RecommendedArtistUiElement>();
            
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            //Adding the headphones icon if the user has listed to this artist before
            foreach (var userArtist in recommender.Users[ID].Artists)
            {
                if (recommender.RoskildeArtists.ContainsKey(userArtist.Key))
                {
                    RoskildeArtist artist = recommender.RoskildeArtists[userArtist.Key];
                    RecommendedArtist tempArtist = new RecommendedArtist(artist);
                    _elements.Add(new SchedueleElement(tempArtist, artist.TimeOfConcert, 60, ElementOrigin.HeardBefore));
                }
            }
            
            //Getting the artist the user added
            foreach (RoskildeArtist artist in hardSelected)
            {
                _elements.Add(new SchedueleElement(new RecommendedArtist(artist), artist.TimeOfConcert, 60, ElementOrigin.HardSelected));
            }

            //Getting Recommendations
            recommender.GenerateRecommendations(ID);
            foreach (var artist in recommender.RecommendedCollabArtists)
            {
                _elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, ElementOrigin.Collab));
            }
            foreach (var artist in recommender.RecommendedContetArtists)
            {
                _elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, ElementOrigin.Content));
            }
            
            //Addig the individual concerts to a full scheduele
            _elements.ForEach(x => _fullScheduele.AddConcert(x));
            
            //From scheduele to artistGUIlist, in order to show it
            foreach (SchedueleElement element in _fullScheduele.Concerts)
            {
                RecommendedArtistUiElement Temp = new RecommendedArtistUiElement(element.Artist, element.AddedFrom, $" - {element.EndTime.TimeOfDay}");
                
                if (element.Exclamation)
                {
                    element.OverlappingArtist.ForEach(x => Temp.Overlapping.Add(x));
                    Temp.Exclamation = true;
                }
                
                _artistGUIList.Add(Temp);
            }
            
            //Adding the lock icon if the user have added this
            foreach (RecommendedArtistUiElement item in _artistGUIList)
            {
                if (item.RatingFrom == ElementOrigin.HardSelected)
                    item.Lock = true;
            }

            //Adding Buttons
            List<DateTime> Days = new List<DateTime>();
            
            foreach (RecommendedArtistUiElement item in _artistGUIList.OrderBy(x => x.Artist.TimeOfConcert))
            {
                if (!Days.Any(x => x.Month == item.Artist.TimeOfConcert.Month && x.Day == item.Artist.TimeOfConcert.Day))
                {
                    Days.Add(item.Artist.TimeOfConcert);
                }
            }

            int top = 5;
            int left = 5;

            foreach (DateTime item in Days)
            {
                Button button = new Button();
                button.Left = left;
                button.Top = top;
                button.Text = item.DayOfWeek + " - " + item.Date.Day;
                button.Name = item.Day.ToString();
                button.Click += new EventHandler(DayButton_Click);
                button.Tag = item.Date.Day.ToString();
                ButtonPanel.Controls.Add(button);
                button.Width += 20;
                left += button.Width + 2;
            }

            //Closeing the thead with loading screen
            loadThread.Abort();
        }

        //Listend for button presses
        private void DayButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                panel1.Controls.Clear();
                label1.Text = btn.Text;

                int counter = 0;
                foreach (RecommendedArtistUiElement item in _artistGUIList
                    .Where(x => x.Artist.TimeOfConcert.Date.Day.ToString() == btn.Tag.ToString())
                    .OrderBy(x => x.TimeOfConcertLabel.ToString()))
                {
                    item.CalcLocation(new Point(5, 105 * counter), new Size(400, 100));
                    panel1.Controls.Add(item.Element);
                    counter++;
                }
            }
        }
    }
}
