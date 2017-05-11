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
    public partial class HoverWindow : Form
    {
        private Panel _background;
        private int _spacing;
        public HoverWindow(Panel Content)
        {   
            _spacing = 2;   
            _background = new Panel();

            //Sets width and height of HoverWindow
            Height = 300;
            Width = 500;
            
            _background.Location = new Point(_spacing, _spacing);
            _background.Size = new Size(Width-_spacing, Height - _spacing);
            Content.Size = new Size(Width - (_spacing*2), Height - (_spacing*2));
            _background.Controls.Add(Content);

            Controls.Add(_background);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            _background.MouseLeave += new EventHandler(Leaves);
            MouseLeave += new EventHandler(Leaves);

            InitializeComponent();
        }

        public void Leaves(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
