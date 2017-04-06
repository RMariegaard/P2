using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
  /*  class KeyWordSearch
    {
        List<Userartist> Matches = new List<Userartist>();
        List<Tag> TagsInCommon = new List<Tag>();
        bool TagIsThere;

        public List<Tag> CompareArtistAndTagList(Artist Data, List<Tag> UserTags)
        {
            TagsInCommon.RemoveRange(0, TagsInCommon.Count());

            for (int i = 0; i < UserTags.Count(); i++)
            {
                for (int j = 0; j < Data.Tags.Count(); j++)
                {
                    TagIsThere = false;
                    for (int q = 0; q < TagsInCommon.Count(); q++)
                    {
                        if (UserTags.ElementAt(i).Id == TagsInCommon.ElementAt(q).Id)
                            TagIsThere = true;
                    }
                    if (!TagIsThere)
                        if (UserTags.ElementAt(i).Id == Data.Tags.ElementAt(j).Id)
                            TagsInCommon.Add(UserTags.ElementAt(i));
                }
            }

            return TagsInCommon;
        }
        
        public List<Tag> CompareTwoArtists(Artist Data, Artist User)
        {
            TagsInCommon.RemoveRange(0, TagsInCommon.Count());
            List<Tag> UserTags = User.Tags;

            for (int i = 0; i < UserTags.Count(); i++)
            {
                for (int j = 0; j < Data.Tags.Count(); j++)
                {
                    TagIsThere = false;
                    for (int q = 0; q < TagsInCommon.Count(); q++)
                    {
                        if (UserTags.ElementAt(i).Id == TagsInCommon.ElementAt(q).Id)
                            TagIsThere = true;
                    }
                    if (!TagIsThere)
                        if (UserTags.ElementAt(i).Id == Data.Tags.ElementAt(j).Id)
                            TagsInCommon.Add(UserTags.ElementAt(i));
                }
            }

            return TagsInCommon;
        }

        public List<Userartist> DirectMatch(List<Artist> Data, List<Userartist> User)
        {
            Matches.RemoveRange(0, Matches.Count());

            for (int i = 0; i < User.Count(); i++)
            {
                for (int j = 0; j < Data.Count(); j++)
                {
                    if (User.ElementAt(i).Id == Data.ElementAt(j).Id && Matches.Contains(User.ElementAt(i)) == false)
                        Matches.Add(User.ElementAt(i));
                }
            }

            return Matches;
        }
    }*/
}
