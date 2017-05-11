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
        List<RecommendedArtistUiElement> GUIList;
        List<SchedueleElement> Elements;
        Scheduele FullScheduele;
        
        public UiScheduele(int ID, RecommenderSystem Recommender, List<RoskildeArtist> HardSelected)
        {
            //Starting a new thread to show loading screen
            LoadingScreen loading = new LoadingScreen("Getting your recommendations");
            Thread loadThread = new Thread(() => loading.ShowDialog());
            loadThread.Start();
            
            InitializeComponent();

            Elements = new List<SchedueleElement>();
            FullScheduele = new Scheduele();

            label1.Text = "Select a date from above!";
            label1.Location = new Point(Width / 2 - label1.Width / 2, label1.Location.Y);
            GUIList = new List<RecommendedArtistUiElement>();
            
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            //Adding the headphones icon if the user has listed to this artist before
            foreach (RoskildeArtist artist in Recommender.GetRoskildeArtists())
            {
                foreach (var userArtist in Recommender.GetUser(ID).Artists)
                {
                    if (userArtist.Key == artist.Id)
                    {
                        RecommendedArtist TempArtist = new RecommendedArtist(artist);
                        Elements.Add(new SchedueleElement(TempArtist, artist.TimeOfConcert, 60, ElementOrigin.HeardBefore));
                    }
                }
            }

            //Getting the artist the user added
            foreach (RoskildeArtist artist in HardSelected)
            {
                Elements.Add(new SchedueleElement(new RecommendedArtist(artist), artist.TimeOfConcert, 60, ElementOrigin.HardSelected));
            }

            //Getting Recommendations
            Recommender.GenerateRecommendations(ID);
            foreach (var artist in Recommender.RecommendedCollabArtists)
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, ElementOrigin.Collab));
            }
            foreach (var artist in Recommender.RecommendedContetArtists)
            {
                Elements.Add(new SchedueleElement(artist.Value, artist.Value.TimeOfConcert, 60, ElementOrigin.Content));
            }
            
            //Addig the individual concerts to a full scheduele
            Elements.ForEach(x => FullScheduele.AddConcert(x));
            
            //From scheduele to GUIlist, in order to show it
            foreach (SchedueleElement element in FullScheduele.Concerts)
            {
                RecommendedArtistUiElement Temp = new RecommendedArtistUiElement(element.Artist, element.AddedFrom, $" - {element.EndTime.TimeOfDay}");
                
                if (element.Exclamation)
                {
                    element.OverlappingArtist.ForEach(x => Temp.Overlapping.Add(x));
                    Temp.Exclamation = true;
                }
                
                GUIList.Add(Temp);
            }
            
            //Adding the lock icon if the user have added this
            foreach (RecommendedArtistUiElement item in GUIList)
            {
                if (item.RatingFrom == ElementOrigin.HardSelected)
                    item.Lock = true;
            }

            //Adding Buttons
            List<DateTime> Days = new List<DateTime>();
            
            foreach (RecommendedArtistUiElement item in GUIList.OrderBy(x => x.Artist.TimeOfConcert))
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
                button.Click += new EventHandler(NewButton_Click);
                button.Tag = item.Date.Day.ToString();
                ButtonPanel.Controls.Add(button);
                button.Width += 20;
                left += button.Width + 2;
            }

            //Closeing the thead with loading screen
            loadThread.Abort();
        }

        //Listend for button presses
        private void NewButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            panel1.Controls.Clear();
            label1.Text = btn.Text;

            int counter = 0;
            foreach (RecommendedArtistUiElement item in GUIList.Where(x => x.Artist.TimeOfConcert.Date.Day.ToString() == btn.Tag.ToString()).OrderBy(x => x.TimeOfConcertLabel.ToString()))
            {
                item.calcLocation(new Point(5, 105 * counter), new Size(400, 100));
                panel1.Controls.Add(item.Element);
                counter++;
            }
        }
    }
}
