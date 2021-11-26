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

            var rndAnimeNumber = new Random();
            var rndAnimeImage1 = rndAnimeNumber.Next(32836);
            var rndAnimeImage2 = rndAnimeNumber.Next(32836);
            var rndAnimeImage3 = rndAnimeNumber.Next(32836);
            var rndAnimeImage4 = rndAnimeNumber.Next(32836);
            var AnimeImage1 = "";
            var AnimeImage2 = "";
            var AnimeImage3 = "";
            var AnimeImage4 = "";

            var path = @"..\..\..\..\Data\anime-offline-database.json";
            string jsonFile = File.ReadAllText(path);

            var obj = JsonConvert.DeserializeObject<AnimeData>(jsonFile);
            for (int i = 0 ; i < obj.data.Count(); i++)
            {
                if(i == rndAnimeImage1)
                {
                    AnimeImage1 = obj.data[i].picture;
                }
                if (i == rndAnimeImage2)
                {
                    AnimeImage2 = obj.data[i].picture;
                }
                if (i == rndAnimeImage3)
                {
                    AnimeImage3 = obj.data[i].picture;
                }
                if (i == rndAnimeImage4)
                {
                    AnimeImage4 = obj.data[i].picture;
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
        }
    }
}
