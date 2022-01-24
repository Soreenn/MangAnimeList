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

            this._animeIndex = animeIndex;
            this._controller = controller;
            List<Anime> _animes = controller.InitializeAnimeList();
            _isAnimeInList = this._controller.IsMediaInList("animes", _animes[animeIndex].id);
            _isAnimeFinished = this._controller.IsMediaFinished("animes", _animes[animeIndex].id);

            string _animeCoverURL = _animes[animeIndex].Cover;
            string _animeBannerURL = "https://s4.anilist.co/file/anilistcdn/media/anime/banner/131565-JBlm0IItFlUV.jpg";
            _animeTitleRomaji = _animes[animeIndex].Title[0];
            if(_animes[animeIndex].Title.ElementAtOrDefault(2) != null)
            {
                _animeTitleNative = _animes[animeIndex].Title[1];
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

            var _animeEpisodes = _animes[animeIndex].episodes;
            var _animeType = _animes[animeIndex].type;
            var _animeStatus = _animes[animeIndex].Status;
            var _animeNote = "100";

            BitmapImage _animeCoverSet = new BitmapImage();
            _animeCoverSet.BeginInit();
            _animeCoverSet.UriSource = new Uri(_animeCoverURL, UriKind.Absolute);
            _animeCoverSet.EndInit();

            BitmapImage _animeBannerSet = new BitmapImage();
            _animeBannerSet.BeginInit();
            _animeBannerSet.UriSource = new Uri(_animeBannerURL, UriKind.Absolute);
            _animeBannerSet.EndInit();

            foreach (string _tag in _animes[animeIndex].Tags)
            {
                tagsBox.Items.Add(_tag);
            }

            cover.ImageSource = _animeCoverSet;
            Banner.ImageSource = _animeBannerSet;
            title.Content = _animeTitleRomaji;
            episodes.Content = _animeEpisodes;
            type.Content = _animeType;
            status.Content = _animeStatus;
            note.Content = _animeNote;
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
            List<Anime> _animes = _controller.InitializeAnimeList();
            _controller.AddMedia("animes", _animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", _animes[_animeIndex].id);

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
            List<Anime> _animes = _controller.InitializeAnimeList();
            _controller.FinishMedia("animes", _animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", _animes[_animeIndex].id);
            _isAnimeFinished = _controller.IsMediaFinished("animes", _animes[_animeIndex].id);

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
            List<Anime> _animes = _controller.InitializeAnimeList();
            _controller.RemoveMedia("animes", _animes[_animeIndex].id);
            _isAnimeInList = _controller.IsMediaInList("animes", _animes[_animeIndex].id);

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
        private void GoBack(object sender, MouseEventArgs e)
        {
            Close();
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
