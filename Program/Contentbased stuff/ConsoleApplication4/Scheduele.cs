using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (SchedueleElement currentConcert in Concerts)
            {
                if (currentConcert.StartTime <= newConcert.StartTime && currentConcert.EndTime > newConcert.StartTime)
                {
                    noOverlap = false;
                }
                //Start time is now before the current concert
                //therefore we check wether the end time is after the start time of the current concert
                else if (currentConcert.StartTime < newConcert.EndTime)
                {
                    noOverlap = false;
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
