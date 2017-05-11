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
            
            //We are writing ToList() becuse we are romoving elements inside the foreach. With .ToList() We are creating a new list, that we use for the forech. But still removing in the original.
            foreach (SchedueleElement element in Concerts.ToList())
            {
                //Does it overlap
                if (element.StartTime < newConcert.EndTime && element.StartTime >= newConcert.StartTime || element.EndTime > newConcert.StartTime && element.StartTime < newConcert.StartTime)
                {
                    //Hard Added
                    if (element.AddedFrom == ElementOrigin.HardSelected && newConcert.AddedFrom != ElementOrigin.HardSelected)
                    {
                        if (element.Artist.Id != newConcert.Artist.Id)
                        {
                            element.OverlappingAdd(newConcert);
                        }
                        noOverlap = false;
                    }
                    else if (element.AddedFrom != ElementOrigin.HardSelected && newConcert.AddedFrom == ElementOrigin.HardSelected)
                    {
                        if (element.Artist.Id != newConcert.Artist.Id)
                        {
                            element.OverlappingAdd(element);
                        }
                        Concerts.Remove(element);
                    }
                    else if (element.AddedFrom == ElementOrigin.HardSelected && newConcert.AddedFrom == ElementOrigin.HardSelected)
                    {
                        MessageBox.Show(
                            $"You have added two artists that overlap on {element.StartTime.DayOfWeek} - {element.StartTime.Day} \n {element.Artist.Name} and {newConcert.Artist.Name}");
                    }
                    //Heard Before
                    else if (element.AddedFrom == ElementOrigin.HeardBefore && newConcert.AddedFrom != ElementOrigin.HeardBefore)
                    {
                        if (element.Artist.Id != newConcert.Artist.Id)
                        {
                            element.OverlappingAdd(newConcert);
                        }
                        noOverlap = false;
                    }
                    else if (element.AddedFrom != ElementOrigin.HeardBefore && newConcert.AddedFrom == ElementOrigin.HeardBefore)
                    {
                        if (element.Artist.Id != newConcert.Artist.Id)
                        {
                            element.OverlappingAdd(element);
                        }
                        Concerts.Remove(element);
                    }
                    else if (element.AddedFrom == ElementOrigin.HeardBefore && newConcert.AddedFrom == ElementOrigin.HeardBefore)
                    {
                        MessageBox.Show(
                            $"You have heard two artists before that overlap on {element.StartTime.DayOfWeek} - {element.StartTime.Day} \n {element.Artist.Name} and {newConcert.Artist.Name}");
                    }
                    else
                    {
                        //Only show one of each artist
                        if (element.Artist.Stars > newConcert.Artist.Stars)
                            noOverlap = false;
                        else
                        {
                            if (element.Artist.Stars > 7 || element.Artist.Stars == newConcert.Artist.Stars)
                            {
                                newConcert.OverlappingAdd(element);
                            }
                            Concerts.Remove(element);
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
