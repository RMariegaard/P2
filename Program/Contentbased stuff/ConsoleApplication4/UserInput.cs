using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeRecommender
{
    class UserInput
    {
        int UserInputID;
        ConvertIdName Convert = new ConvertIdName();

        public Artist GetArtistFromUserInput(string UserInput, Dictionary<int, Artist> DataArtist)
        {
            //Id or Name?
            if (int.TryParse(UserInput, out UserInputID))
            {
                return Convert.ArtistFromID(UserInputID, DataArtist);
            }
            else
            {
                return Convert.ArtistFromName(UserInput, DataArtist);
            }
        }

        public bool TryGetArtistFromUserInput(string UserInput, Dictionary<int, Artist> DataArtist, out Artist BaseArtist)
        {
            //Id or Name?
            try
            {
                if (int.TryParse(UserInput, out UserInputID))
                {
                    BaseArtist = Convert.ArtistFromID(UserInputID, DataArtist);
                    return true;
                }
                else
                {
                    BaseArtist = Convert.ArtistFromName(UserInput, DataArtist);
                    return true;
                }
            } catch (Exception)
            {
                BaseArtist = default(Artist);
                return false;
            }
        }
    }
}
