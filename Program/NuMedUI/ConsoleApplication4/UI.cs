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
    public partial class UI : Form
    {
        public int ID;
        
        public UI()
        {
            InitializeComponent();
            ErrorLabelFrontPage.Text = "";
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(UserIdTextbox.Text, out ID))
            {
                ErrorLabelFrontPage.Text = "Please enter a number";
            }
            else
            {
                UIAfterLogin frm2 = new UIAfterLogin(ID);
                frm2.Show();
            }
        }
    }
}
