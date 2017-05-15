using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender
{
    public partial class UI : Form
    {
        private int _userID;
        RecommenderSystem Recommender;
        
        public UI()
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            InitializeComponent();
            pictureBox1.Image = Image.FromFile(startupPath + @"\Images\Orange.jpg");

            //Showing loading screen
            
            LoadingScreen loading = new LoadingScreen("ReadingFiles");
            try
            {
                Thread loadThread = new Thread(() => loading.ShowDialog());
                loadThread.Start();

                IRecommendationsMethods recommandationsMethods = new MethodsForRecommending();

                //Reading files
                Recommender = new RecommenderSystem(recommandationsMethods);
                Recommender.LoadFiles();

                //Closing loading screen
                loadThread.Abort();
            }
            catch (Exception)
            {
                //Get reconmedations only - shows no loading screen
                IRecommendationsMethods recommandationsMethods = new MethodsForRecommending();

                //Reading files
                Recommender = new RecommenderSystem(recommandationsMethods);
                Recommender.LoadFiles();
            }
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(UserIdTextbox.Text, out _userID))
            {
                MessageBox.Show("Please enter a valid number");
            }
            else if (!Recommender.CheckForUserId(_userID))
            {
                MessageBox.Show("User not found!");
            }
            else
            {
                UIAfterLogin nextWindow = new UIAfterLogin(_userID, Recommender);
                nextWindow.Show();
            }
        }

        private void UpdateDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Showing loading screen
                LoadingScreen loading = new LoadingScreen("ReadingFiles");
                Thread loadThread = new Thread(() => loading.ShowDialog());
                loadThread.Start();

                //Update and read files
                var data = new DataHandling();
                data.MakeBinaryFiles();
                Recommender.LoadFiles();

                //Close loading screen
                loadThread.Abort();
            }
            catch (Exception)
            {
                //Fails to make new thread then just make files
                //Update and read files
                var data = new DataHandling();
                data.MakeBinaryFiles();
                Recommender.LoadFiles();
            }
        }

        private void UserIdTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyData)
            {
                LoginButton_Click(sender, e);
            }
        }
        
        private void newUserButton_Click(object sender, EventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(Recommender);
            newUserWindow.Show();
        }
    }
}
