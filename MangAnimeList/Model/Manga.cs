using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAnimeList
{
    public class Manga
    {
        public int id { get; set; }
        public string title { get; set; }
        public string romajiTitle { get; set; }
        public int? averageScore { get; set; }
        public int? volumes { get; set; }
        public int? chapters { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public List<string> tags { get; set; }
        public string coverImage { get; set; }
        public string bannerImage { get; set; }
    }

}
