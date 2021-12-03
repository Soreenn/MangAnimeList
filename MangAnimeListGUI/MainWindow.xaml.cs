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

namespace MangAnimeListGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Controller controller = new Controller();
            List<Manga> mangas =  controller.InitializeMangaList();
            List<Anime> animes = controller.InitializeAnimeList();

            var rndMangaNumber = new Random();
            var rndMangaImage1 = rndMangaNumber.Next(mangas.Count());
            var rndMangaImage2 = rndMangaNumber.Next(mangas.Count());
            var rndMangaImage3 = rndMangaNumber.Next(mangas.Count());
            var rndMangaImage4 = rndMangaNumber.Next(mangas.Count());
            var MangaImage1 = mangas[rndMangaImage1].Cover;
            var MangaImage2 = mangas[rndMangaImage2].Cover;
            var MangaImage3 = mangas[rndMangaImage3].Cover;
            var MangaImage4 = mangas[rndMangaImage4].Cover;

            var rndAnimeNumber = new Random();
            var rndAnimeImage1 = rndAnimeNumber.Next(mangas.Count());
            var rndAnimeImage2 = rndAnimeNumber.Next(mangas.Count());
            var rndAnimeImage3 = rndAnimeNumber.Next(mangas.Count());
            var rndAnimeImage4 = rndAnimeNumber.Next(mangas.Count());
            var AnimeImage1 = animes[rndAnimeImage1].Cover;
            var AnimeImage2 = animes[rndAnimeImage2].Cover;
            var AnimeImage3 = animes[rndAnimeImage3].Cover;
            var AnimeImage4 = animes[rndAnimeImage4].Cover;




            BitmapImage MangaImage1Set = new BitmapImage();
            MangaImage1Set.BeginInit();
            MangaImage1Set.UriSource = new Uri(MangaImage1, UriKind.Absolute);
            MangaImage1Set.EndInit();
            rndManga1.ImageSource = MangaImage1Set;

            BitmapImage MangaImage2Set = new BitmapImage();
            MangaImage2Set.BeginInit();
            MangaImage2Set.UriSource = new Uri(MangaImage2, UriKind.Absolute);
            MangaImage2Set.EndInit();
            rndManga2.ImageSource = MangaImage2Set;

            BitmapImage MangaImage3Set = new BitmapImage();
            MangaImage3Set.BeginInit();
            MangaImage3Set.UriSource = new Uri(MangaImage3, UriKind.Absolute);
            MangaImage3Set.EndInit();
            rndManga3.ImageSource = MangaImage3Set;

            BitmapImage MangaImage4Set = new BitmapImage();
            MangaImage4Set.BeginInit();
            MangaImage4Set.UriSource = new Uri(MangaImage4, UriKind.Absolute);
            MangaImage4Set.EndInit();
            rndManga4.ImageSource = MangaImage4Set;

        }
    }
}
