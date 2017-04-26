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

        public bool Exclamation => OverlappingArtist != null;
        public List<SchedueleElement> OverlappingArtist;
        
        public void OverlappingAdd(SchedueleElement Concert)
        {
            if (OverlappingArtist == null)
                OverlappingArtist = new List<SchedueleElement>();

            OverlappingArtist.Add(Concert);
        }

        public SchedueleElement(RecommendedArtist artist, DateTime startTime, double minutesOfTime, string AddedFrom)
        {
            this.AddedFrom = AddedFrom;
            Artist = artist;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(minutesOfTime);
        }
    }
}
