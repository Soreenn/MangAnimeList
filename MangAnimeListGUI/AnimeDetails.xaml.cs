using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour AnimeDetails.xaml
    /// </summary>
    public partial class AnimeDetails : Window, INotifyPropertyChanged
    {
        Controller _controller;
        int _animeIndex;
        string _animeTitleRomaji;
        string _animeTitleNative;
        private Visibility _addToListVisibility;
        private Visibility _finishAnimeVisibility;
        private Visibility _removeFromListVisibility;
        private bool _isAnimeInList;
        private bool _isAnimeFinished;

        public AnimeDetails(int animeIndex, Controller controller)
        {
            InitializeComponent();

            _animeIndex = animeIndex;
            _controller = controller;
            List<Anime> animes = controller.InitializeAnimeList();
            _isAnimeInList = _controller.IsMediaInList("animes", animes[animeIndex].id);
            _isAnimeFinished = _controller.IsMediaFinished("animes", animes[animeIndex].id);

            string animeCoverURL = animes[animeIndex].Cover;
            string animeBannerURL = "https://s4.anilist.co/file/anilistcdn/media/anime/banner/131565-JBlm0IItFlUV.jpg";
            _animeTitleRomaji = animes[animeIndex].Title[0];
            if(animes[animeIndex].Title.ElementAtOrDefault(2) != null)
            {
                _animeTitleNative = animes[animeIndex].Title[1];
            }
            else
            {
                NativeButton.Visibility = Visibility.Hidden;
            }

            if (_isAnimeInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
            }

            if (_isAnimeInList == true)
            {
                if (_isAnimeFinished == true)
                {
                    FinishAnimeVisibility = Visibility.Collapsed;
                }
                else
                {
                    FinishAnimeVisibility = Visibility.Visible;
                }
            }
            else
            {
                FinishAnimeVisibility = Visibility.Collapsed;
            }

            if (_isAnimeInList == true)
            {
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                RemoveFromListVisibility = Visibility.Collapsed;
            }

            var animeEpisodes = animes[animeIndex].episodes;
            var animeType = animes[animeIndex].type;
            var animeStatus = animes[animeIndex].Status;
            var animeNote = "100";

            BitmapImage animeCoverSet = new BitmapImage();
            animeCoverSet.BeginInit();
            animeCoverSet.UriSource = new Uri(animeCoverURL, UriKind.Absolute);
            animeCoverSet.EndInit();

            BitmapImage animeBannerSet = new BitmapImage();
            animeBannerSet.BeginInit();
            animeBannerSet.UriSource = new Uri(animeBannerURL, UriKind.Absolute);
            animeBannerSet.EndInit();

            foreach (string tag in animes[animeIndex].Tags)
            {
                tagsBox.Items.Add(tag);
            }

            cover.ImageSource = animeCoverSet;
            Banner.ImageSource = animeBannerSet;
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
            List<Anime> animes = _controller.InitializeAnimeList();
            _controller.AddMedia("animes", animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", animes[_animeIndex].id);

            if (_isAnimeInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
                FinishAnimeVisibility = Visibility.Visible;
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
                FinishAnimeVisibility = Visibility.Collapsed;
                RemoveFromListVisibility = Visibility.Collapsed;
            }
        }

        private void FinishAnime(object sender, MouseEventArgs e)
        {
            List<Anime> animes = _controller.InitializeAnimeList();
            _controller.FinishMedia("animes", animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", animes[_animeIndex].id);
            _isAnimeFinished = _controller.IsMediaFinished("animes", animes[_animeIndex].id);

            if (_isAnimeInList == true)
            {
                if (_isAnimeFinished == true)
                {
                    FinishAnimeVisibility = Visibility.Collapsed;
                }
                else
                {
                    FinishAnimeVisibility = Visibility.Visible;
                }
            }
            else
            {
                FinishAnimeVisibility = Visibility.Collapsed;
            }
        }

        private void RemoveFromList(object sender, MouseEventArgs e)
        {
            List<Anime> animes = _controller.InitializeAnimeList();
            _controller.RemoveMedia("animes", animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", animes[_animeIndex].id);

            if (_isAnimeInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
                FinishAnimeVisibility = Visibility.Visible;
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
                FinishAnimeVisibility = Visibility.Collapsed;
                RemoveFromListVisibility = Visibility.Collapsed;
            }
        }

        public Visibility AddToListVisibility
        {
            get => _addToListVisibility;
            set
            {
                _addToListVisibility = value;
                NotifyPropertyChanged(nameof(AddToListVisibility));
            }
        }

        public Visibility FinishAnimeVisibility
        {
            get => _finishAnimeVisibility;
            set
            {
                _finishAnimeVisibility = value;
                NotifyPropertyChanged(nameof(FinishAnimeVisibility));
            }
        }

        public Visibility RemoveFromListVisibility
        {
            get => _removeFromListVisibility;
            set
            {
                _removeFromListVisibility = value;
                NotifyPropertyChanged(nameof(RemoveFromListVisibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
