using System.Collections.Generic;
using System.Linq;

namespace MangAnimeList
{
    public class Media
    {
        private List<string> _title;
        private string _status;
        private string _thumbnail;
        private int _id;
        private int _releaseYear;
        private List<string> _tags;
        private string _cover;

        public Media(List<string> title ,string status, int releaseYear, List<string> tags, string cover, int id, string thumbnail)
        {
            _title = title.ToList();
            _tags = tags.ToList();
            _status = status;
            _releaseYear = releaseYear;
            _cover = cover;
            _id = id;
            _thumbnail = thumbnail;
        }

        public List<string> Title
        {
            get { return _title; }
        }

        public string Status
        {
            get { return _status; }
        }

        public int StartYear
        {
            get { return _releaseYear; }
        }

        public List<string> Tags
        {
            get { return _tags; }
        }

        public string Cover
        {
            get { return _cover; }
        }

        public int id
        {
            get { return _id; }
        }

        public string thumbnail
        {
            get { return _thumbnail; }
        }
    }
}
