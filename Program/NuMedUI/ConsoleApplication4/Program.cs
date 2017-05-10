using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Recommender
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*************************************** The Program ***************************************/
            Application.EnableVisualStyles();
            Application.Run(new UI());
        }
    }
}
