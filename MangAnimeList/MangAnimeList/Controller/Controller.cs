using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MangAnimeList;

namespace MangAnimeList
{
    public class Controller
    {
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
                foreach (char title in media.title.Value)
                {
                    titles.Add(title.ToString());
                }

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
            foreach (dynamic media in fileManga.data)
            {
                List<string> titles = new List<string>();
                foreach (string title in media.Media.title)
                {
                    titles.Add(title);
                }

                List<string> tags = new List<string>();
                foreach (var tag in media.Media.tags)
                {
                    tags.Add(tag.name.Value);
                }

                
                Manga manga = new Manga(titles, media.Media.status.Value, (int)media.Media.startDate.year.Value, tags, media.Media.coverImage.extraLarge.Value);
                manga.id = media.Media.id;
                manga.averageScore = media.Media.averageScore;
                manga.volumes = media.Media.volumes;
                manga.chapters = media.Media.chapters;
                manga.bannerImage = media.Media.bannerImage;
                mangas.Add(manga);
            }
            return mangas;
        }
    }
}
