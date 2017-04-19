using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class SchedueleElement
    {
        public DateTime StartTime;
        public RoskildeArtist Artist;
        public DateTime EndTime;

        public SchedueleElement(RoskildeArtist artist, DateTime startTime, double minutesOfTime)
        {
            Artist = artist;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(minutesOfTime);
        }
    }
}
