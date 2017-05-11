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
        private PictureBox _loadingGif;
        private Label _textLabel;

        public LoadingScreen(string text)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            _loadingGif = new PictureBox();
            _loadingGif.Image = Image.FromFile(startupPath + @"\Images\spin.gif");
            _loadingGif.Location = new Point(0, 10);
            _loadingGif.Visible = true;
            _loadingGif.AutoSize = true;

            _textLabel = new Label();
            _textLabel.Text = text;
            _textLabel.Visible = true;
            _textLabel.Location = new Point(0,0);
            _textLabel.AutoSize = true;

            Controls.Add(_textLabel);
            Controls.Add(_loadingGif);

            Width = 200;
            Height = 200;

            InitializeComponent();
        }
    }
}
