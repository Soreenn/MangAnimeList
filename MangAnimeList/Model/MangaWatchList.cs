namespace MangAnimeList
{
    public class MangaWatchList
    {
        Manga _manga;
        int _chapter;
        int _volume;
        string _state;

        public MangaWatchList(Manga manga, int volume, int chapter, string state)
        {
            _manga = manga;
            _chapter = chapter;
            _volume = volume;
            _state = state;
        }

        public int Volume
        {
            get { return _volume; }
        }

        public string State
        {
            get { return _state; }
        }

        public int Chapter
        {
            get { return _chapter; }
        }

        public Manga Manga
        {
            get { return _manga; }
        }
    }
}
