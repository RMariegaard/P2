using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class PearsonCor
    {


        public double Calculate(Artist artist1, Artist artist2)
        {

            double Artist1Mean = 0.0;
            artist1.Tags.Values.ToList().ForEach(tag => Artist1Mean += tag.Weight);
            Artist1Mean /= artist1.TotalTagAmount;

            double Artist2Mean = 0.0;
            artist2.Tags.Values.ToList().ForEach(tag => Artist2Mean += tag.Weight);
            Artist2Mean /= artist2.TotalTagAmount;

            double Top = 0.0;

            foreach(var tag in artist1.Tags)
            {
                if (artist2.Tags.ContainsKey(tag.Key))
                {
                    Top += (tag.Value.Weight - Artist1Mean) * (artist2.Tags[tag.Value.Id].Weight - Artist2Mean);
                }
            }
            double Buttom = Math.Sqrt(Top);



            return Top / Buttom;
        }


        public double CalculateUser(User A1, User A2) //Baseret på Tags og ikke Artists
        {

            double A1Mean = 0.0;
            A1.Tags.Values.ToList().ForEach(tag => A1Mean += tag.Weight);
            A1Mean /= A1.TotalTagAmount;

            double A2Mean = 0.0;
            A2.Tags.Values.ToList().ForEach(tag => A2Mean += tag.Weight);
            A2Mean /= A2.TotalTagAmount;

            double Top = 0.0;

            foreach (var tagA1 in A1.Tags)
            {
                if (A2.Tags.ContainsKey(tagA1.Key))
                {
                    Top += (tagA1.Value.Weight - A1Mean) * (A2.Tags[tagA1.Key].Weight - A2Mean);
                }
            }
            double Buttom = Math.Sqrt(Top);



            return Top / Buttom;
        }
        

    }
}
