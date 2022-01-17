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
        Controller _controller;
        private string _mangaTitleRomaji;
        private string _mangaTitleNative;

        public MangaDetails(int mangaIndex, Controller controller)
        {
            InitializeComponent();

            _controller = controller;
            List<Manga> mangas = _controller.InitializeMangaList();

            var mangaCoverURL = mangas[mangaIndex].Cover;
            var rndMangaBannerURL = mangas[mangaIndex].bannerImage;
            _mangaTitleRomaji = mangas[mangaIndex].Title[1];
            if (mangas[mangaIndex].Title[2] != null)
            {
                _mangaTitleNative = mangas[mangaIndex].Title[2];
            }
            else
            {
                NativeButton.Visibility = Visibility.Hidden;
            }
            var mangaTags = mangas[mangaIndex].Tags;
            var mangaVolumes = mangas[mangaIndex].volumes;
            var mangaChapters = mangas[mangaIndex].chapters;
            var mangaStatus = mangas[mangaIndex].Status;
            var mangaNote = mangas[mangaIndex].averageScore;

            BitmapImage mangaCoverSet = new BitmapImage();
            mangaCoverSet.BeginInit();
            mangaCoverSet.UriSource = new Uri(mangaCoverURL, UriKind.Absolute);
            mangaCoverSet.EndInit();

            BitmapImage MangaBannerSet = new BitmapImage();
            MangaBannerSet.BeginInit();
            MangaBannerSet.UriSource = new Uri(rndMangaBannerURL, UriKind.Absolute);
            MangaBannerSet.EndInit();

            foreach(string tag in mangas[mangaIndex].Tags)
            {
                tagsBox.Items.Add(tag);
            }

            cover.ImageSource = mangaCoverSet;
            Banner.ImageSource = MangaBannerSet;
            title.Content = _mangaTitleRomaji;
            volumes.Content = mangaVolumes;
            chapters.Content = mangaChapters;
            status.Content = mangaStatus;
            note.Content = mangaNote;
        }

        private void DisplayRomajiTitle(object sender, MouseEventArgs e)
        {
            title.Content = _mangaTitleRomaji;
        }
        private void DisplayNativeTitle(object sender, MouseEventArgs e)
        {
            title.Content = _mangaTitleNative;
        }
        private void AddToList(object sender, MouseEventArgs e)
        {
            _controller.AddMedia(_mangaTitleRomaji, "mangas");
        }
    }
}
