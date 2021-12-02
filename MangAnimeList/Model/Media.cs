using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAnimeList.Model
{
    class Media
    {
        private string _title;
        private string _status;
        private int _startYear;
        private List<string> _tags;
        private string _cover;

        public Media(string title, string status, int startYear, string cover)
        {
            _title = title;
            _status = status;
            _startYear = startYear;
            _tags = new List<string>();
            _cover = cover;
        }

        public string Title
        {
            get { return _title; }
        }

        public string Status
        {
            get { return _status; }
        }

        public int StartYear
        {
            get { return _startYear; }
        }

        public List<string> Tags
        {
            get { return _tags; }
        }

        public string Cover
        {
            get { return _cover; }
        }
    }
}
