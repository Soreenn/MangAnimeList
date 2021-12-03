using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAnimeList
{
    public class Manga : Media
    {
        public Manga(List<string> title, string status, int releaseYear, List<string> tags, string cover) : base(title, status, releaseYear, tags, cover)
        {

        }

        public int id
        {
            get;
            set;
        }

        public int? averageScore
        {
            get;
            set;
        }

        public int? volumes
        {
            get;
            set;
        }

        public int? chapters
        {
            get;
            set;
        }

        public string bannerImage
        {
            get;
            set;
        }
    }

}
