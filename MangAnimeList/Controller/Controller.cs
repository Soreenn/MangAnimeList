using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangAnimeList;
using System.Runtime.InteropServices;
using System.Collections;

namespace MangAnimeList
{
    public class Controller
    {
        bool _loginState = false;
        List<Manga> _mangaHomeList;
        List<Anime> _animeHomeList;

        static Random random = new Random();

        public Controller()
        {

        }

        public List<Anime> InitializeAnimeList()
        {

            IEnumerable animesQuery = DBManager.DBManager.Select($"SELECT * FROM media WHERE mediaType = 'anime'");
            List<Anime> animes = new List<Anime>();
            foreach (dynamic media in animesQuery)
            {
                List<string> titles = new List<string>();
                titles.Add(media.title_romaji);
                titles.Add(media.title_native);

                List<string> tags = new List<string>(media.tags.Split(','));

                Anime anime = new Anime(titles, media.status, (int)media.release_year, tags, media.cover, media.id, media.thumbnail);
                anime.episodes = media.episodes;
                anime.type = media.type;
                animes.Add(anime);
            }
            return animes;
        }

        public List<Manga> InitializeMangaList()
        {
            IEnumerable mangasQuery = DBManager.DBManager.Select($"SELECT * FROM media WHERE mediaType = 'manga'");
            List<Manga> mangas = new List<Manga>();
            foreach (dynamic media in mangasQuery)
            {
                List<string> tags = new List<string>(media.tags.Split(','));

                List<string> titles = new List<string>();
                titles.Add(media.title_romaji);
                titles.Add(media.title_native);

                Manga manga = new Manga(titles, media.status, (int)media.release_year, tags, media.cover, media.id, media.thumbnail);

                manga.averageScore = media.averageScore;
                manga.volumes = media.volumes;
                manga.chapters = media.chapters;
                manga.bannerImage = media.bannerImage;
                mangas.Add(manga);
            }
            return mangas;
        }

        public void GenerateRandomHomeList()
        {
            List<Anime> randomAnimeList = new List<Anime>();
            List<Manga> randomMangaList = new List<Manga>();

            Anime animeShown1;
            Anime animeShown2;
            Anime animeShown3;
            Anime animeShown4;

            Manga mangaShown1;
            Manga mangaShown2;
            Manga mangaShown3;
            Manga mangaShown4;

            Controller controller = new Controller();
            List<Manga> mangas = controller.InitializeMangaList();
            List<Anime> animes = controller.InitializeAnimeList();

            animeShown1 = animes[random.Next(animes.Count)];
            animeShown2 = animes[random.Next(animes.Count)];
            animeShown3 = animes[random.Next(animes.Count)];
            animeShown4 = animes[random.Next(animes.Count)];

            randomAnimeList.Add(animeShown1);
            randomAnimeList.Add(animeShown2);
            randomAnimeList.Add(animeShown3);
            randomAnimeList.Add(animeShown4);

            mangaShown1 = mangas[random.Next(mangas.Count)];
            mangaShown2 = mangas[random.Next(mangas.Count)];
            mangaShown3 = mangas[random.Next(mangas.Count)];
            mangaShown4 = mangas[random.Next(mangas.Count)];


            randomMangaList.Add(mangaShown1);
            randomMangaList.Add(mangaShown2); //gros consultant
            randomMangaList.Add(mangaShown3);
            randomMangaList.Add(mangaShown4);


            GetAnimeHomeList = randomAnimeList;
            GetMangaHomeList = randomMangaList;
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

        public bool IsConnected
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
            string errorMessage = null;

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
                            IsConnected = true;
                        }
                        else
                        {
                            IsConnected = false;
                            errorMessage = "Error while registering the user. Please retry !";
                        }
                    }
                    else
                    {
                        IsConnected = false;
                        errorMessage = "The username is already taken please change !";
                    }
                }
                else
                {
                    IsConnected = false;
                    errorMessage = "The passwords are not matching !";
                }
            }
            else
            {
                IsConnected = false;
                errorMessage = "Please fill all the textboxes !";
            }
            return errorMessage;
        }

        public string Login(string username, string password)
        {
            string errorMessage = null;

            if ((username != string.Empty) && (password != string.Empty))
            {
                if (DBManager.DBManager.IsLoginCorrect($"SELECT * FROM users WHERE username = '{username}'", password) == true)
                {
                    int userType = DBManager.DBManager.GetUserType($"SELECT userType FROM users WHERE username = '{username}' AND password = '{password}'");
                    int userId = DBManager.DBManager.GetUserId($"SELECT * FROM users WHERE username = '{username}'");

                    DBManager.DBManager.Session.SetValue(username, 0);
                    DBManager.DBManager.Session.SetValue(userType.ToString(), 1);
                    DBManager.DBManager.Session.SetValue(userId.ToString(), 2);
                    IsConnected = true;
                }
                else
                {
                    IsConnected = false;
                    errorMessage = "The username and / or the password are not correct. Please retry !";
                }
            }
            else
            {
                IsConnected = false;
                errorMessage = "Please fill all the textboxes !";
            }
            return errorMessage;
        }


        public void AddMedia(string mediaType, int mediaId)
        {
            int result = DBManager.DBManager.AddMediaToList($"INSERT INTO {mediaType} (user_id, {mediaType}_id, {(mediaType == "mangas" ? "current_chapter" : "current_episode" )}{(mediaType == "mangas" ? ", current_volume" : "")}, state) VALUES ('{GetUserId}', '{mediaId}', {(mediaType == "mangas" ? "1" : "1")}{(mediaType == "mangas" ? ", 1" : "")}, 'UNFINISHED')");
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
            IEnumerable animeIds = DBManager.DBManager.Select($"SELECT * FROM animes WHERE user_id LIKE '{GetUserId}'");
            List<AnimeWatchList> _watchlist = new List<AnimeWatchList>();
            foreach(dynamic media in animeIds)
            {
                AnimeWatchList _animeWatchlist = new AnimeWatchList(_animes[media.animes_id - 1], media.current_episode, media.state);
                _watchlist.Add(_animeWatchlist);
            }
            return _watchlist;
        }

        public List<MangaWatchList> GetMangaWatchlist()
        {
            List<Manga> _mangas = InitializeMangaList();
            IEnumerable mangaIds = DBManager.DBManager.Select($"SELECT * FROM mangas WHERE user_id LIKE '{GetUserId}'");
            List<MangaWatchList> _watchlist = new List<MangaWatchList>();
            foreach (dynamic media in mangaIds)
            {
                MangaWatchList _mangaWatchlist = new MangaWatchList(_mangas[GetMangaIndex(media.mangas_id)], media.current_volume, media.current_chapter, media.state);
                _watchlist.Add(_mangaWatchlist);
            }
            return _watchlist;
        }

        public bool IsMangaInList(int mangaId)
        {
            List<Manga> _mangas = InitializeMangaList();
            IEnumerable result = DBManager.DBManager.Select($"SELECT * FROM mangas WHERE user_id LIKE '{GetUserId}' AND mangas_id LIKE '{mangaId}'");
            bool isMangaInList;
            if (result.ToString() == null)
            {
                isMangaInList = false;
            } else
            {
                isMangaInList = true;
            }
            return isMangaInList;
        }
    }
}
