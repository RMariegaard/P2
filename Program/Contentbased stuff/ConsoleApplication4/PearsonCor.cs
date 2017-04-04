using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class PearsonCor
    {


        public double Calculate(Artist A1, Artist A2)
        {

            double A1Mean = 0.0;
            A1.TagIds.ForEach(tag => A1Mean += tag.weight);
            A1Mean /= A1.total_tag_amount;

            double A2Mean = 0.0;
            A2.TagIds.ForEach(tag => A2Mean += tag.weight);
            A2Mean /= A2.total_tag_amount;

            double Top = 0.0;

            foreach(var tagA1 in A1.TagIds)
            {
                if(A2.TagIds.Exists(tag => tag.Id == tagA1.Id))
                {
                    Top += (tagA1.weight - A1Mean) * (A2.TagIds.Find(tag => tag.Id == tagA1.Id).weight - A2Mean);
                }
            }
            double Buttom = Math.Sqrt(Top);



            return Top / Buttom;
        }


        public double CalculateUser(User A1, User A2) //Baseret på Tags og ikke Artists
        {

            double A1Mean = 0.0;
            A1.Tags.ForEach(tag => A1Mean += tag.weight);
            A1Mean /= A1.total_tag_amount;

            double A2Mean = 0.0;
            A2.Tags.ForEach(tag => A2Mean += tag.weight);
            A2Mean /= A2.total_tag_amount;

            double Top = 0.0;

            foreach (var tagA1 in A1.Tags)
            {
                if (A2.Tags.Exists(tag => tag.Id == tagA1.Id))
                {
                    Top += (tagA1.weight - A1Mean) * (A2.Tags.Find(tag => tag.Id == tagA1.Id).weight - A2Mean);
                }
            }
            double Buttom = Math.Sqrt(Top);



            return Top / Buttom;
        }


    }
}
