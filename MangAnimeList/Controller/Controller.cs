using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace MangAnimeList
{
    public class Controller
    {
        bool _loginState = false;
        List<Manga> _mangaHomeList;
        List<Anime> _animeHomeList;

        static Random _random = new Random();

        public Controller()
        {

        }

        public List<Anime> InitializeAnimeList()
        {

            IEnumerable _animesQuery = DBManager.DBManager.Select($"SELECT * FROM media WHERE mediaType = 'anime'");
            List<Anime> _animes = new List<Anime>();
            foreach (dynamic _media in _animesQuery)
            {
                List<string> _titles = new List<string>();
                _titles.Add(_media.title_romaji);
                _titles.Add(_media.title_native);

                List<string> _tags = new List<string>(_media.tags.Split(','));

                Anime _anime = new Anime(_titles, _media.status, (int)_media.release_year, _tags, _media.cover, _media.id, _media.thumbnail);
                _anime.episodes = _media.episodes;
                _anime.type = _media.type;
                _animes.Add(_anime);
            }
            return _animes;
        }

        public List<Manga> InitializeMangaList()
        {
            IEnumerable _mangasQuery = DBManager.DBManager.Select($"SELECT * FROM media WHERE mediaType = 'manga'");
            List<Manga> _mangas = new List<Manga>();
            foreach (dynamic _media in _mangasQuery)
            {
                List<string> _tags = new List<string>(_media.tags.Split(','));

                List<string> _titles = new List<string>();
                _titles.Add(_media.title_romaji);
                _titles.Add(_media.title_native);

                Manga _manga = new Manga(_titles, _media.status, (int)_media.release_year, _tags, _media.cover, _media.id, _media.thumbnail);

                _manga.averageScore = _media.averageScore;
                _manga.volumes = _media.volumes;
                _manga.chapters = _media.chapters;
                _manga.bannerImage = _media.bannerImage;
                _mangas.Add(_manga);
            }
            return _mangas;
        }

        public void GenerateRandomHomeList()
        {
            List<Anime> _randomAnimeList = new List<Anime>();
            List<Manga> _randomMangaList = new List<Manga>();

            Anime _animeShown1;
            Anime _animeShown2;
            Anime _animeShown3;
            Anime _animeShown4;

            Manga _mangaShown1;
            Manga _mangaShown2;
            Manga _mangaShown3;
            Manga _mangaShown4;

            Controller _controller = new Controller();
            List<Manga> _mangas = _controller.InitializeMangaList();
            List<Anime> _animes = _controller.InitializeAnimeList();

            _animeShown1 = _animes[_random.Next(_animes.Count)];
            _animeShown2 = _animes[_random.Next(_animes.Count)];
            _animeShown3 = _animes[_random.Next(_animes.Count)];
            _animeShown4 = _animes[_random.Next(_animes.Count)];

            _randomAnimeList.Add(_animeShown1);
            _randomAnimeList.Add(_animeShown2);
            _randomAnimeList.Add(_animeShown3);
            _randomAnimeList.Add(_animeShown4);

            _mangaShown1 = _mangas[_random.Next(_mangas.Count)];
            _mangaShown2 = _mangas[_random.Next(_mangas.Count)];
            _mangaShown3 = _mangas[_random.Next(_mangas.Count)];
            _mangaShown4 = _mangas[_random.Next(_mangas.Count)];


            _randomMangaList.Add(_mangaShown1);
            _randomMangaList.Add(_mangaShown2);
            _randomMangaList.Add(_mangaShown3);
            _randomMangaList.Add(_mangaShown4);


            GetAnimeHomeList = _randomAnimeList;
            GetMangaHomeList = _randomMangaList;
        }

        public List<Manga> GetMangaHomeList
        {
            set { _mangaHomeList = value; }
            get { return _mangaHomeList; }
        }


        public List<Anime> GetAnimeHomeList
        {
            set { _animeHomeList = value; }
            get { return _animeHomeList; }
        }

        public bool _IsConnected
        {
            get { return _loginState; }
            set { _loginState = value; }
        }

        public string GetUsername
        {
            get { return (string)DBManager.DBManager.Session.GetValue(0); }
        }
        public string GetUserType
        {
            get { return (string)DBManager.DBManager.Session.GetValue(1); }
        }

        public string GetUserId
        {
            get { return (string)DBManager.DBManager.Session.GetValue(2); }
        }

        public string RegisterUser(string username, string password, string confirmPassword)
        {
            string _errorMessage = null;

            if ((username != string.Empty) && (password != string.Empty) && (password != string.Empty))
            {
                if (password == confirmPassword)
                {
                    if (DBManager.DBManager.IsUsernameUnique($"SELECT * FROM users", username) == true)
                    {
                        if (DBManager.DBManager.RegisterUserDB($"INSERT INTO users (username, password) VALUES ('{username}', '{password}')") == 1)
                        {
                            int userId = DBManager.DBManager.GetUserId($"SELECT * FROM users WHERE username = '{username}'");
                            int userType = DBManager.DBManager.GetUserType($"SELECT userType FROM users WHERE username = '{username}' AND password = '{password}'");

                            DBManager.DBManager.Session.SetValue(username, 0);
                            DBManager.DBManager.Session.SetValue(userType.ToString(), 1);
                            DBManager.DBManager.Session.SetValue(userId.ToString(), 2);
                            _IsConnected = true;
                        }
                        else
                        {
                            _IsConnected = false;
                            _errorMessage = "Error while registering the user. Please retry !";
                        }
                    }
                    else
                    {
                        _IsConnected = false;
                        _errorMessage = "The username is already taken please change !";
                    }
                }
                else
                {
                    _IsConnected = false;
                    _errorMessage = "The passwords are not matching !";
                }
            }
            else
            {
                _IsConnected = false;
                _errorMessage = "Please fill all the textboxes !";
            }
            return _errorMessage;
        }

        public string Login(string username, string password)
        {
            string _errorMessage = null;

            if ((username != string.Empty) && (password != string.Empty))
            {
                if (DBManager.DBManager.IsLoginCorrect($"SELECT * FROM users WHERE username = '{username}'", password) == true)
                {
                    int userType = DBManager.DBManager.GetUserType($"SELECT userType FROM users WHERE username = '{username}' AND password = '{password}'");
                    int userId = DBManager.DBManager.GetUserId($"SELECT * FROM users WHERE username = '{username}'");

                    DBManager.DBManager.Session.SetValue(username, 0);
                    DBManager.DBManager.Session.SetValue(userType.ToString(), 1);
                    DBManager.DBManager.Session.SetValue(userId.ToString(), 2);
                    _IsConnected = true;
                }
                else
                {
                    _IsConnected = false;
                    _errorMessage = "The username and / or the password are not correct. Please retry !";
                }
            }
            else
            {
                _IsConnected = false;
                _errorMessage = "Please fill all the textboxes !";
            }
            return _errorMessage;
        }


        public void AddMedia(string mediaType, int mediaId)
        {
            int _result = DBManager.DBManager.AddMediaToList($"INSERT INTO {mediaType} (user_id, {mediaType}_id, {(mediaType == "mangas" ? "current_chapter" : "current_episode" )}{(mediaType == "mangas" ? ", current_volume" : "")}, state) VALUES ('{GetUserId}', '{mediaId}', {(mediaType == "mangas" ? "1" : "1")}{(mediaType == "mangas" ? ", 1" : "")}, 'UNFINISHED')");
        }

        public void FinishMedia(string mediaType, int mediaId)
        {
            int _result = DBManager.DBManager.FinishMedia($"UPDATE {mediaType} SET state = 'FINISHED' WHERE {mediaType}_id LIKE '{mediaId}'");
        }

        public void RemoveMedia(string mediaType, int mediaId)
        {
            int _result = DBManager.DBManager.FinishMedia($"DELETE FROM {mediaType} WHERE {mediaType}_id LIKE '{mediaId}'");
        }

        public int GetMangaIndex(int id)
        {
            List<Manga> _mangas = InitializeMangaList();
            int _index = id - _mangas[0].id;

            return _index;
        }

        public List<AnimeWatchList> GetAnimeWatchlist()
        {
            List<Anime> _animes = InitializeAnimeList();
            IEnumerable _animeIds = DBManager.DBManager.Select($"SELECT * FROM animes WHERE user_id LIKE '{GetUserId}'");
            List<AnimeWatchList> _watchlist = new List<AnimeWatchList>();
            foreach(dynamic _media in _animeIds)
            {
                AnimeWatchList _animeWatchlist = new AnimeWatchList(_animes[_media.animes_id - 1], _media.current_episode, _media.state);
                _watchlist.Add(_animeWatchlist);
            }
            return _watchlist;
        }

        public List<MangaWatchList> GetMangaWatchlist()
        {
            List<Manga> _mangas = InitializeMangaList();
            IEnumerable _mangaIds = DBManager.DBManager.Select($"SELECT * FROM mangas WHERE user_id LIKE '{GetUserId}'");
            List<MangaWatchList> _watchlist = new List<MangaWatchList>();
            foreach (dynamic _media in _mangaIds)
            {
                MangaWatchList _mangaWatchlist = new MangaWatchList(_mangas[GetMangaIndex(_media.mangas_id)], _media.current_volume, _media.current_chapter, _media.state);
                _watchlist.Add(_mangaWatchlist);
            }
            return _watchlist;
        }

        public bool IsMediaInList(string mediaType, int mediaId)
        {
            IEnumerable _result = DBManager.DBManager.Select($"SELECT * FROM {mediaType} WHERE user_id LIKE '{GetUserId}' AND {mediaType}_id LIKE '{mediaId}'");
            bool _isMediaInList;
            if (_result.Cast<object>().Any())
            {
                _isMediaInList = true;
            } else
            {
                _isMediaInList = false;
            }
            return _isMediaInList;
        }

        public bool IsMediaFinished(string mediaType, int mediaId)
        {
            IEnumerable _result = DBManager.DBManager.Select($"SELECT * FROM {mediaType} WHERE user_id LIKE '{GetUserId}' AND {mediaType}_id LIKE '{mediaId}' AND state LIKE 'FINISHED'");
            bool _isMediaFinished;
            if (_result.Cast<object>().Any())
            {
                _isMediaFinished = true;
            }
            else
            {
                _isMediaFinished = false;
            }
            return _isMediaFinished;
        }
    }
}
