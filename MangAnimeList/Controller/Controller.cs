using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MangAnimeList
{
    public class Controller
    {
        public Controller()
        {

        }

        public void InitializeAnimeList()
        {

        }

        public List<Manga> InitializeMangaList()
        {
            var pathManga = @"..\..\..\..\Data\manga-offline-database.json";
            string jsonFileManga = File.ReadAllText(pathManga);
            dynamic fileManga = JsonConvert.DeserializeObject(jsonFileManga);
            List<Manga> mangas = new List<Manga>();
            foreach (dynamic media in fileManga.data)
            {
                Manga manga = new Manga();
                manga.id = media.Media.id;
                manga.title = media.Media.title.english;
                manga.romajiTitle = media.Media.title.romaji;
                manga.averageScore = media.Media.averageScore;
                manga.volumes = media.Media.volumes;
                manga.chapters = media.Media.chapters;
                manga.status = media.Media.status;
                manga.startDate = media.Media.starDate.year;
                manga.tags = media.Media.tags;
                manga.coverImage = media.Media.coverImage.extraLarge;
                manga.bannerImage = media.Media.bannerImage;
                mangas.Add(manga);
            }
            return mangas;
        }
    }
}
