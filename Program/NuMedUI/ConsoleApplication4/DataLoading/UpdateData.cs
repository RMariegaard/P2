using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class UpdateData
    {
        public static void UpdateDataFiles()
        {
            /*************************************** Write file ***************************************/
            
            BinaryData Data = new BinaryData();
            Data.MakeBinaryFiles();
        }
    }
}
