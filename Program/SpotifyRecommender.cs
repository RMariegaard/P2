using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses




namespace ConsoleApplication5
{
    class Program
    {


        private static SpotifyWebAPI _spotify;


        public static void Main(String[] args)
        {
            Hej hej = new Hej();

            hej.Auuth(args);
            _spotify = new SpotifyWebAPI()
            {
                UseAuth = true //This will disable Authentication.
            };

            hej.Test();

            Console.Read();
        }
    }

    class Hej {

        private static SpotifyWebAPI _spotify;

        public async void Auuth(String[] args)
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
        "http://localhost",
        8000,
        "eeb342b6409d4dee886276446210988c",
        Scope.UserReadPrivate,
        TimeSpan.FromSeconds(60) );

            try
            {
                //This will open the user's browser and returns once
                //the user is authorized.
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }

            if (_spotify == null)
                return;

        }
        public async void Test()
        {
            var profile = await _spotify.GetPrivateProfileAsync();
            Console.WriteLine("Name: " + profile.DisplayName);
            Console.WriteLine("ID: " + profile.Id);

            //Get Roskilde Artist
            List<string> Roskilde_k = new List<string>();
            FullPlaylist playlist = _spotify.GetPlaylist("spotify_denmark", "2Sr8Lp6XEqcOSlojpTwgXX");
            Paging<PlaylistTrack> roskilde = playlist.Tracks;
            List<PlaylistTrack> roskilde_p = roskilde.Items;
            List<SimpleArtist> artist = new List<SimpleArtist>();
            foreach (PlaylistTrack track in roskilde_p)
            {
                foreach (SimpleArtist item in track.Track.Artists)
                    artist.Add(item);

            }
            foreach (SimpleArtist itemm in artist)
            {
                Roskilde_k.Add(itemm.Name);
            }
            //Roskilde_k now contains all artist in spotify playlist of Roskilde 2017


            //Get User Top Artists
            List<string> User_k = new List<string>();
            Paging<FullArtist> listTemp = new Paging<FullArtist>();

            try
            {
                listTemp = await _spotify.GetUsersTopArtistsAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            if (listTemp == null) Console.WriteLine("error");
                foreach (FullArtist track in listTemp.Items)
                {
                    User_k.Add(track.Name);
                }
           

            //Get User Genres
            List<string> User_g = new List<string>();
            foreach (FullArtist _artist in listTemp.Items)
            {
                foreach(string genre in _artist.Genres)
                {
                    if (!User_g.Contains(genre)) User_g.Add(genre);
                }
            }

            //Get Roskilde Artists' Genre
            List<string> Roskilde_g = new List<string>();
            foreach (SimpleArtist itemm in artist)
            {
                FullArtist temp = _spotify.GetArtist(itemm.Id);
                if(temp.Genres != null)
                foreach(string _genre in temp.Genres)
                {
                    if (!Roskilde_g.Contains(_genre)) Roskilde_g.Add(_genre);
                }
            }




            //Prints Artist matches
            Console.WriteLine("Concerts based on your top artists:");
            foreach (string kunstner in User_k)
            {
                if (Roskilde_k.Contains(kunstner)) Console.WriteLine(kunstner);
            }
            Console.WriteLine("---------------DONE!---------------\n");
            //Prints Artist with Genre matches
            Console.WriteLine("Concerts based on your top artists' genres:");
            List<string> genreMatch = new List<string>();
            foreach (string kunstner in User_g)
            {
                if (Roskilde_g.Contains(kunstner)) genreMatch.Add(kunstner);
            }
            List<string> artistMatch = new List<string>();
            foreach (string genre in genreMatch)
            {
                FullArtist temp = new FullArtist();
                foreach(SimpleArtist _artist in artist)
                {
                    temp = _spotify.GetArtist(_artist.Id);
                    if (temp.Id != null && !artistMatch.Contains(temp.Name) && temp.Genres.Contains(genre))
                    {

                        artistMatch.Add(temp.Name);
                        Console.WriteLine(temp.Name);
                    }
                }
            }
            Console.WriteLine("---------------DONE!---------------\n");

            //Related Artist
            Console.WriteLine("Based on Roskilde's Realted Artist");
            foreach (string kunstner in User_k)
            {
                foreach(SimpleArtist _artist in artist)
                {
                    List<FullArtist> _rA = GetRelated(_artist.Id);
                    foreach(FullArtist navn in _rA)
                    {
                        if (kunstner == navn.Name) Console.WriteLine(_artist.Name);
                    }
                }
            }
            Console.WriteLine("---------------DONE!---------------\n");

            Console.WriteLine("Random Concert:");
            Console.WriteLine(GetRandomConcert(Roskilde_k));
            Console.WriteLine("---------------DONE!---------------\n");



        }
        public List<FullArtist> GetRelated(string artistId)
        {
            //Related Artists
            List<FullArtist> relatedArtists = new List<FullArtist>();
            SeveralArtists list = _spotify.GetRelatedArtists(artistId);
            if (list.Artists != null)
            {
                foreach (FullArtist item in list.Artists)
                {
                    relatedArtists.Add(item);
                }
            }
            return relatedArtists;

        }
        public string GetRandomConcert(List<string> roskilde)
        {
            string random;

            Random _random = new Random();
            random = roskilde.ElementAt(_random.Next(0, roskilde.Count-1));

            return random;
        }
    }
}
