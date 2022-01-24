namespace MangAnimeList
{
    public class AnimeWatchList
    {
        Anime _anime;
        int _episode;
        string _state;

        public AnimeWatchList(Anime anime, int episode, string state)
        {
            _anime = anime;
            _episode = episode;
            _state = state;
        }

        public string State
        {
            get { return _state; }
        }

        public int Episode
        {
            get { return _episode; }
        }

        public Anime Anime
        {
            get { return _anime; }
        }
    }
}
