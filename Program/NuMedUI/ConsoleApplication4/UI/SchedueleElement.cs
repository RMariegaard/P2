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
        public RecommendedArtist Artist;
        public DateTime EndTime;
        public string AddedFrom;

        public SchedueleElement(RecommendedArtist artist, DateTime startTime, double minutesOfTime, string AddedFrom)
        {
            this.AddedFrom = AddedFrom;
            Artist = artist;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(minutesOfTime);
        }
    }
}
