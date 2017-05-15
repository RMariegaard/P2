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
    public partial class NewUserWindow : Form
    {
        Label usernameLabel;
        TextBox usernameTextBox;
        Button createUserButton;
        RecommenderSystem Recommender;

        public NewUserWindow(RecommenderSystem Recommender)
        {
            InitializeComponent();

            this.Recommender = Recommender;

            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            createUserButton = new Button();
            
            //Username label
            usernameLabel.Text = "Username:";
            usernameLabel.Location = new Point(5,5);
            usernameLabel.AutoSize = true;
            usernameLabel.Visible = true;

            //username textbox
            usernameTextBox.Location = new Point(10 + usernameLabel.Size.Width, 5);
            usernameTextBox.Size = new Size(60, usernameLabel.Size.Height);
            usernameTextBox.Visible = true;

            //button
            createUserButton.Location = new Point(5, usernameTextBox.Height + 10);
            createUserButton.Click += new EventHandler(createUserButton_click);
            createUserButton.Size = new Size(usernameLabel.Size.Width + 5 +usernameTextBox.Size.Width, usernameLabel.Size.Height);
            createUserButton.Text = "Create";
            createUserButton.Visible = true;

            //Adding to window
            Controls.Add(usernameLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(createUserButton);
        }

        public void createUserButton_click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != "")
            {
                UserBaselineRatings baselineRatings = new UserBaselineRatings(usernameTextBox.Text, Recommender);
                baselineRatings.Show();
                this.Close();
            }
        }
    }
}
