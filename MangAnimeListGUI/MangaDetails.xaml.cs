using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour MangaDetails.xaml
    /// </summary>
    public partial class MangaDetails : Window
    {
        public MangaDetails()
        {
            InitializeComponent();
            Controller controller = new Controller();
            List<Manga> mangas = controller.InitializeMangaList();

            var rndManga = new Random();
            var rndMangaIndex = rndManga.Next(mangas.Count());
            var rndMangaCoverURL = mangas[rndMangaIndex].Cover;
            var rndMangaTitle = "";
            foreach(string title in mangas[rndMangaIndex].Title)
            {
                rndMangaTitle = title;
            }
            var rndMangaTags = mangas[rndMangaIndex].Tags;
            var rndMangaVolumes = mangas[rndMangaIndex].volumes;
            var rndMangaChapters = mangas[rndMangaIndex].chapters;
            var rndMangaStatus = mangas[rndMangaIndex].Status;
            var rndMangaNote = mangas[rndMangaIndex].averageScore;

            BitmapImage rndMangaCoverSet = new BitmapImage();
            rndMangaCoverSet.BeginInit();
            rndMangaCoverSet.UriSource = new Uri(rndMangaCoverURL, UriKind.Absolute);
            rndMangaCoverSet.EndInit();

            var rndMangaBannerURL = mangas[rndMangaIndex].bannerImage;
            BitmapImage rndMangaBannerSet = new BitmapImage();
            rndMangaBannerSet.BeginInit();
            rndMangaBannerSet.UriSource = new Uri(rndMangaBannerURL, UriKind.Absolute);
            rndMangaBannerSet.EndInit();

            cover.ImageSource = rndMangaCoverSet;
            Banner.ImageSource = rndMangaBannerSet;
            title.Content = rndMangaTitle;
            tags.Content = rndMangaTags;
            volumes.Content = rndMangaVolumes;
            chapters.Content = rndMangaChapters;
            status.Content = rndMangaStatus;
            note.Content = rndMangaNote;
        }
    }
}
