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
                //Does it overlap
                if (element.StartTime < newConcert.EndTime && element.StartTime >= newConcert.StartTime || element.EndTime > newConcert.StartTime && element.StartTime < newConcert.StartTime)
                {
                    //Hard Added
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
                        MessageBox.Show($"You have added two artists that overlap on {element.StartTime.DayOfWeek} - {element.StartTime.Day} \n {element.Artist.Name} and {newConcert.Artist.Name}");
                    }
                    //Overlap, but not hard added. Takes the one with the best rating
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


                        if (o < n) //LIGE NU TAGER DEN ALTID COLLABORATIVE DA DEN SOM REGL ER STØRRE END CONTENT
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
