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
    /// Logique d'interaction pour AnimeDetails.xaml
    /// </summary>
    public partial class AnimeDetails : Window
    {
        Controller _controller;
        string _animeTitleRomaji;
        string _animeTitleNative;
        public AnimeDetails(int animeIndex, Controller controller)
        {
            InitializeComponent();

            _controller = controller;
            List<Anime> animes = controller.GetAnimeHomeList;

            var animeCoverURL = animes[animeIndex].Cover;
            var mangaBannerURL = "https://s4.anilist.co/file/anilistcdn/media/anime/banner/131565-JBlm0IItFlUV.jpg";
            _animeTitleRomaji = animes[animeIndex].Title[0];
            if(animes[animeIndex].Title.ElementAtOrDefault(2) != null)
            {
                _animeTitleNative = animes[animeIndex].Title[1];
            }
            else
            {
                NativeButton.Visibility = Visibility.Hidden;
            }
            
            var animeEpisodes = animes[animeIndex].episodes;
            var animeType = animes[animeIndex].type;
            var animeStatus = animes[animeIndex].Status;
            var animeNote = "100";

            BitmapImage mangaCoverSet = new BitmapImage();
            mangaCoverSet.BeginInit();
            mangaCoverSet.UriSource = new Uri(animeCoverURL, UriKind.Absolute);
            mangaCoverSet.EndInit();

            BitmapImage MangaBannerSet = new BitmapImage();
            MangaBannerSet.BeginInit();
            MangaBannerSet.UriSource = new Uri(mangaBannerURL, UriKind.Absolute);
            MangaBannerSet.EndInit();

            foreach (string tag in animes[animeIndex].Tags)
            {
                tagsBox.Items.Add(tag);
            }

            cover.ImageSource = mangaCoverSet;
            Banner.ImageSource = MangaBannerSet;
            title.Content = _animeTitleRomaji;
            episodes.Content = animeEpisodes;
            type.Content = animeType;
            status.Content = animeStatus;
            note.Content = animeNote;
        }
        private void DisplayRomajiTitle(object sender, MouseEventArgs e)
        {
            title.Content = _animeTitleRomaji;
        }
        private void DisplayNativeTitle(object sender, MouseEventArgs e)
        {
            title.Content = _animeTitleNative;
        }

        private void AddToList(object sender, MouseEventArgs e)
        {
            _controller.AddMedia(_animeTitleRomaji, "animes");
        }
    }
}
