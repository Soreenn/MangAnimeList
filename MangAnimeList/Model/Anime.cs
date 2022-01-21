using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAnimeList
{
    public class Anime : Media
    {
        public Anime(List<string> title, string status, int releaseYear, List<string> tags, string cover, int id, string thumbnail) : base(title, status, releaseYear, tags, cover, id, thumbnail)
        {

        }

        public string type
        {
            get;
            set;
        }

        public int episodes
        {
            get;
            set;
        }

    }

}

