using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAnimeList
{
    public class Datum
    {
        public string[] sources { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int episodes { get; set; }
        public string status { get; set; }
        public AnimeSeason animeSeason { get; set; }
        public string picture { get; set; }
        public string thumbnail { get; set; }
        public string[] synonyms { get; set; }
        public string[] relations { get; set; }
        public string[] tags { get; set; }
    }
}
