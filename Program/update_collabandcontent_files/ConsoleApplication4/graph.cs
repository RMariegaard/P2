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
    public partial class graph : Form
    {
        public graph(List<double> aay, List<double> aay1)
        {
            InitializeComponent();
            array = aay;
            array1 = aay1;
        }
        private List<double> array = new List<double>();
        private List<double> array1 = new List<double>();
        private void graphTest_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (var element in array)
            {
                chart1.Series["Series1"].Points.AddXY(i, element);
                i++;
            }


        }

        private void chart2_Click(object sender, EventArgs e)
        {
            chart2.ChartAreas[0].AxisY.Maximum = 1;
            int i = 1;
            foreach (var element in array1)
            {
                chart2.Series["Series1"].Points.AddXY(i, element);
                i++;
            }
        }
    }
}
