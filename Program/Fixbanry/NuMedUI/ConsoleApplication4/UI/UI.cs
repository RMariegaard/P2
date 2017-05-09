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
    public partial class UI : Form
    {
        public int ID;
        CreateRecommendations Recommender;
        
        public UI()
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            InitializeComponent();
            pictureBox1.Image = Image.FromFile(startupPath + @"\Images\Orange.jpg");
            
            Recommender = new CreateRecommendations();
            Recommender.LoadFiles();
            ErrorLabelFrontPage.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(UserIdTextbox.Text, out ID))
            {
                MessageBox.Show("Please enter a valid number");
            }
            else if (!Recommender.CheckForUserId(ID))
            {
                MessageBox.Show("User not found!");
            }
            else
            {
                
                UIAfterLogin frm2 = new UIAfterLogin(ID, Recommender);
                frm2.Show();
            }
        }

        private void UpdateDataButton_Click(object sender, EventArgs e)
        {
            UpdateData.UpdateDataFiles();
            Recommender.LoadFiles();
        }

        private void UserIdTextbox_KeyDown(object sender, KeyEventArgs e)
        {


            if (Keys.Enter == e.KeyData)
            {
                button1_Click(sender, e);
            }
        }
    }
}
