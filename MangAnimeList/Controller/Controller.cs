using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MangAnimeList;
using System.Runtime.InteropServices;

namespace MangAnimeList
{
    public class Controller
    {
        List<Manga> _mangaHomeList;
        List<Anime> _animeHomeList;

        static Random random = new Random();

        public Controller()
        {

        }

        public List<Anime> InitializeAnimeList()
        {
            var pathAnime = @"..\..\..\..\Data\anime-offline-database.json";
            string jsonFileAnime = File.ReadAllText(pathAnime);
            dynamic fileAnime = JsonConvert.DeserializeObject(jsonFileAnime);
            List<Anime> animes = new List<Anime>();
            foreach (dynamic media in fileAnime.data)
            {
                List<string> titles = new List<string>();
                titles.Add(media.title.Value);

                foreach (string title in media.synonyms)
                {
                    titles.Add(title);
                }

                List<string> tags = new List<string>();
                foreach (var tag in media.tags)
                {
                    tags.Add(tag.Value);
                }


                Anime anime = new Anime(titles, media.status.Value, (int)media.animeSeason.year.Value, tags, media.picture.Value);
                anime.episodes = media.episodes;
                anime.type = media.type;
                animes.Add(anime);
            }
            return animes;
        }

        public List<Manga> InitializeMangaList()
        {
            var pathManga = @"..\..\..\..\Data\manga-offline-database.json";
            string jsonFileManga = File.ReadAllText(pathManga);
            dynamic fileManga = JsonConvert.DeserializeObject(jsonFileManga);
            List<Manga> mangas = new List<Manga>();
            foreach (dynamic media in fileManga)
            {
                List<string> titles = new List<string>();
                foreach (string title in media.data.Media.title)
                {
                    titles.Add(title);
                }

                List<string> tags = new List<string>();
                foreach (var tag in media.data.Media.tags)
                {
                    tags.Add(tag.name.Value);
                }

                
                Manga manga = new Manga(titles, media.data.Media.status.Value, (int)media.data.Media.startDate.year.Value, tags, media.data.Media.coverImage.extraLarge.Value);
                manga.id = media.data.Media.id;
                manga.averageScore = media.data.Media.averageScore;
                manga.volumes = media.data.Media.volumes;
                manga.chapters = media.data.Media.chapters;
                manga.bannerImage = media.data.Media.bannerImage;
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

        public List<Manga> GetMangaHomeList {
            set { _mangaHomeList = value; }
            get { return _mangaHomeList; }
        }

        public List<Anime> GetAnimeHomeList
        {
            set { _animeHomeList = value; }
            get { return _animeHomeList; }
        }

        public void ConnectToDatabase() { 
            
        }

        public void RegisterUser(string username, string password)
        {
            if ((username != null) && (password != null))
            {
                int result = MangAnimeList.DBManager.DBManager.RegisterUserDB($"INSERT INTO users (username, password) VALUES ({username}, {password})");
                if (result == 0)
                {
                    MangAnimeList.DBManager.DBManager.Session.SetValue(username, 0);
                    MangAnimeList.DBManager.DBManager.Session.SetValue(1, 1);
                    //open menu while connected
                }
                else
                {
                    //refresh the register page with an error message : error when registering the user, please retry!
                }
            }
            else
            {
                //refresh the register page with an error message : please fill the textboxes !
            }
        }
    }
}
