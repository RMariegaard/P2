using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagstest
{
    /* Den eneste forskel for Roskilde Artist og Roskilde er indtilvidere spillested og tid. */
    class RoskildeArtist : Artist 
    {
        public DateTime Spilletid { get; }
        public string Scene { get; }

        public RoskildeArtist(DateTime spilletid, string scene, int[] TagID, int[] TagWeight, string Name)
        : base(TagID, TagWeight, Name)
        {
            Spilletid = spilletid;
            Scene = scene;
        }
    }
}
