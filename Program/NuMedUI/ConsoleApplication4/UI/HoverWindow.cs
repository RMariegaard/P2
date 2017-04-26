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
        Panel BackGround;
        int Spacing;
        public HoverWindow(Panel Content)
        {
            
            Spacing = 2;
            
            BackGround = new Panel();
            Height = 300;
            Width = 500;
            
            BackGround.Location = new Point(Spacing, Spacing);
            BackGround.Size = new Size(Width-Spacing, Height - Spacing);
            Content.Size = new Size(Width - (Spacing*2), Height - (Spacing*2));
            BackGround.Controls.Add(Content);

            Controls.Add(BackGround);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            BackGround.MouseLeave += new EventHandler(Leves);
            MouseLeave += new EventHandler(Leves);

            InitializeComponent();
        }

        public void Leves(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
