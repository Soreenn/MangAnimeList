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

            var rndAnimeNumber = new Random();
            var rndAnimeImage1 = rndAnimeNumber.Next(32836);
            var rndAnimeImage2 = rndAnimeNumber.Next(32836);
            var rndAnimeImage3 = rndAnimeNumber.Next(32836);
            var rndAnimeImage4 = rndAnimeNumber.Next(32836);
            var AnimeImage1 = "";
            var AnimeImage2 = "";
            var AnimeImage3 = "";
            var AnimeImage4 = "";

            var pathAnime = @"..\..\..\..\Data\anime-offline-database.json";
            string jsonFileAnime = File.ReadAllText(pathAnime);

            var anime = JsonConvert.DeserializeObject<AnimeData>(jsonFileAnime);
            for (int i = 0 ; i < anime.data.Count(); i++)
            {
                if(i == rndAnimeImage1)
                {
                    AnimeImage1 = anime.data[i].picture;
                }
                if (i == rndAnimeImage2)
                {
                    AnimeImage2 = anime.data[i].picture;
                }
                if (i == rndAnimeImage3)
                {
                    AnimeImage3 = anime.data[i].picture;
                }
                if (i == rndAnimeImage4)
                {
                    AnimeImage4 = anime.data[i].picture;
                }
            }

            BitmapImage AnimeImage1Set = new BitmapImage();
            AnimeImage1Set.BeginInit();
            AnimeImage1Set.UriSource = new Uri(AnimeImage1, UriKind.Absolute);
            AnimeImage1Set.EndInit();
            rndAnime1.ImageSource = AnimeImage1Set;

            BitmapImage AnimeImage2Set = new BitmapImage();
            AnimeImage2Set.BeginInit();
            AnimeImage2Set.UriSource = new Uri(AnimeImage2, UriKind.Absolute);
            AnimeImage2Set.EndInit();
            rndAnime2.ImageSource = AnimeImage2Set;

            BitmapImage AnimeImage3Set = new BitmapImage();
            AnimeImage3Set.BeginInit();
            AnimeImage3Set.UriSource = new Uri(AnimeImage3, UriKind.Absolute);
            AnimeImage3Set.EndInit();
            rndAnime3.ImageSource = AnimeImage3Set;

            BitmapImage AnimeImage4Set = new BitmapImage();
            AnimeImage4Set.BeginInit();
            AnimeImage4Set.UriSource = new Uri(AnimeImage4, UriKind.Absolute);
            AnimeImage4Set.EndInit();
            rndAnime4.ImageSource = AnimeImage4Set;

            var rndMangaNumber = new Random();
            var rndMangaImage1 = rndMangaNumber.Next(32836);
            var rndMangaImage2 = rndMangaNumber.Next(32836);
            var rndMangaImage3 = rndMangaNumber.Next(32836);
            var rndMangaImage4 = rndMangaNumber.Next(32836);
            var MangaImage1 = "";
            var MangaImage2 = "";
            var MangaImage3 = "";
            var MangaImage4 = "";



            Trace.WriteLine(jsonFileManga);

            /*var manga = JsonConvert.DeserializeObject<Manga>(jsonFileManga);
            for (int i = 0; i < manga.data.Count(); i++)
            {
                if (i == rndMangaImage1)
                {
                    MangaImage1 = manga.data[i].Media.coverImage.extraLarge;
                }
                if (i == rndMangaImage2)
                {
                    MangaImage2 = manga.data[i].Media.coverImage.extraLarge;
                }
                if (i == rndMangaImage3)
                {
                    MangaImage3 = manga.data[i].Media.coverImage.extraLarge;
                }
                if (i == rndMangaImage4)
                {
                    MangaImage4 = manga.data[i].Media.coverImage.extraLarge;
                }
            }

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
            rndManga4.ImageSource = MangaImage4Set; //*/
        }
    }
}
