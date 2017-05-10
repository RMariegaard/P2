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
    public partial class LoadingScreen : Form
    {
        private PictureBox loadingGif;
        private Label textLabel;

        public LoadingScreen(string text)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);
            loadingGif = new PictureBox();
            loadingGif.Image = Image.FromFile(startupPath + @"\Images\spin.gif");
            loadingGif.Location = new Point(0, 10);
            loadingGif.Visible = true;
            loadingGif.AutoSize = true;

            textLabel = new Label();
            textLabel.Text = text;
            textLabel.Visible = true;
            textLabel.Location = new Point(0,0);
            textLabel.AutoSize = true;

            Controls.Add(textLabel);
            Controls.Add(loadingGif);

            Width = 200;
            Height = 200;

            InitializeComponent();
        }
    }
}
