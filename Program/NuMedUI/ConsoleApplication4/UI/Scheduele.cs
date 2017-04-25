using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender
{
    class Scheduele
    {
        public List<SchedueleElement> Concerts;

        public Scheduele()
        {
            Concerts = new List<SchedueleElement>();
        }
        
        //Add concert when it doesnt overlap
        public bool AddConcert(SchedueleElement newConcert)
        {
            bool noOverlap = true;
            
            foreach (SchedueleElement element in Concerts)
            {
                //Is hard selected
                
                if (element.StartTime < newConcert.EndTime && element.StartTime > newConcert.StartTime)
                {
                    if (element.AddedFrom == "HardAdd" && newConcert.AddedFrom != "HardAdd")
                    {
                        noOverlap = false;
                    }
                    else if (element.AddedFrom != "HardAdd" && newConcert.AddedFrom == "HardAdd")
                    {
                        Concerts.Remove(element);
                    }
                    else if (element.AddedFrom == "HardAdd" && newConcert.AddedFrom == "HardAdd")
                    {
                        MessageBox.Show($"You have added two artists that overlap on {element.StartTime.DayOfWeek} - {element.StartTime.Day}");
                    }
                    else
                    {
                        double n;
                        double o;
                        if (newConcert.Artist.CollaborativeFilteringRating < newConcert.Artist.ContentBasedFilteringRating)
                            n = newConcert.Artist.ContentBasedFilteringRating;
                        else
                            n = newConcert.Artist.CollaborativeFilteringRating;

                        if (element.Artist.CollaborativeFilteringRating < element.Artist.ContentBasedFilteringRating)
                            o = element.Artist.ContentBasedFilteringRating;
                        else
                            o = element.Artist.CollaborativeFilteringRating;


                        if (o < n)
                        {
                            Concerts.Remove(element);
                        }
                        else
                        {
                            noOverlap = false;
                        }
                    }
                }
            }

            //Adds Concert if there is no Overlap;
            if (noOverlap)
            {
                Concerts.Add(newConcert);
                //Sorts by starttime so that the list is sorted!
                Concerts.OrderBy(x => x.StartTime);
            }
            return noOverlap;
        }
    }
}
