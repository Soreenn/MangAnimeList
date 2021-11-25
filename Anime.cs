using System;

namespace MangAnimeList
{
    public class Anime
    {
        public class AnimeData
        {
            public License license { get; set; }
            public string repo { get; set; }
            public Datum[] data { get; set; }
        }

        public class License
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Datum
        {
            public string[] sources { get; set; }
            public string title { get; set; }
            public string type { get; set; }
            public int episodes { get; set; }
            public string status { get; set; }
            public Animeseason animeSeason { get; set; }
            public string picture { get; set; }
            public string thumbnail { get; set; }
            public string[] synonyms { get; set; }
            public string[] relations { get; set; }
            public string[] tags { get; set; }
        }

        public class Animeseason
        {
            public string season { get; set; }
            public int? year { get; set; }
        }
    }
}
