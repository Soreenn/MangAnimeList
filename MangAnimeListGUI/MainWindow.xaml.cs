using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using MangAnimeList;
using Newtonsoft.Json;
using System.Threading;
using System.Text.RegularExpressions;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controller = new Controller();
        public MainWindow()
        {
            InitializeComponent();
            controller.GenerateRandomHomeList();
            List<Manga> mangas = controller.GetMangaHomeList;
            List<Anime> animes = controller.GetAnimeHomeList;

            int loopManga = 1;
            int loopAnime = 1;
            foreach (Manga manga in mangas)
            {
                if (loopManga == 1)
                {
                    BitmapImage MangaImage = new BitmapImage();
                    MangaImage.BeginInit();
                    MangaImage.UriSource = new Uri(manga.Cover, UriKind.Absolute);
                    MangaImage.EndInit();
                    rndManga1.ImageSource = MangaImage;
                }
                if (loopManga == 2)
                {
                    BitmapImage MangaImage = new BitmapImage();
                    MangaImage.BeginInit();
                    MangaImage.UriSource = new Uri(manga.Cover, UriKind.Absolute);
                    MangaImage.EndInit();
                    rndManga2.ImageSource = MangaImage;
                }
                if (loopManga == 3)
                {
                    BitmapImage MangaImage = new BitmapImage();
                    MangaImage.BeginInit();
                    MangaImage.UriSource = new Uri(manga.Cover, UriKind.Absolute);
                    MangaImage.EndInit();
                    rndManga3.ImageSource = MangaImage;
                }
                if (loopManga == 4)
                {
                    BitmapImage MangaImage = new BitmapImage();
                    MangaImage.BeginInit();
                    MangaImage.UriSource = new Uri(manga.Cover, UriKind.Absolute);
                    MangaImage.EndInit();
                    rndManga4.ImageSource = MangaImage;
                }
                loopManga++;
            }

            foreach (Anime anime in animes)
            {
                if (loopAnime == 1)
                {
                    BitmapImage AnimeImage = new BitmapImage();
                    AnimeImage.BeginInit();
                    AnimeImage.UriSource = new Uri(anime.Cover, UriKind.Absolute);
                    AnimeImage.EndInit();
                    rndAnime1.ImageSource = AnimeImage;
                }
                if (loopAnime == 2)
                {
                    BitmapImage AnimeImage = new BitmapImage();
                    AnimeImage.BeginInit();
                    AnimeImage.UriSource = new Uri(anime.Cover, UriKind.Absolute);
                    AnimeImage.EndInit();
                    rndAnime2.ImageSource = AnimeImage;
                }
                if (loopAnime == 3)
                {
                    BitmapImage AnimeImage = new BitmapImage();
                    AnimeImage.BeginInit();
                    AnimeImage.UriSource = new Uri(anime.Cover, UriKind.Absolute);
                    AnimeImage.EndInit();
                    rndAnime3.ImageSource = AnimeImage;
                }
                if (loopAnime == 4)
                {
                    BitmapImage AnimeImage = new BitmapImage();
                    AnimeImage.BeginInit();
                    AnimeImage.UriSource = new Uri(anime.Cover, UriKind.Absolute);
                    AnimeImage.EndInit();
                    rndAnime4.ImageSource = AnimeImage;
                }
                loopAnime++;
            }
        }
        private void DisplayManga(object sender, MouseEventArgs e)
        {
            string name = (((Border)e.Source)).Name;
            string[] numbers = Regex.Split(name, @"(\D+)(\d{1})");
            int index = Int32.Parse(numbers[2]) - 1;

            MangaDetails window = new MangaDetails(index, controller);
            window.ShowDialog();
        }

        private void DisplayAnime(object sender, MouseEventArgs e)
        {
            string name = (((Border)e.Source)).Name;
            string[] numbers = Regex.Split(name, @"(\D+)(\d{1})");
            int index = Int32.Parse(numbers[2]) - 5;

            AnimeDetails window = new AnimeDetails(index, controller);
            window.ShowDialog();
        }
    }
}
