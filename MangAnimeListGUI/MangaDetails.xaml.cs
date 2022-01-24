using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour MangaDetails.xaml
    /// </summary>
    public partial class MangaDetails : Window, INotifyPropertyChanged
    {
        Controller _controller;
        int _mangaIndex;
        private string _mangaTitleRomaji;
        private string _mangaTitleNative;
        private Visibility _addToListVisibility;
        private Visibility _finishMangaVisibility;
        private Visibility _removeFromListVisibility;
        private bool _isMangaInList;
        private bool _isMangaFinished;

        public MangaDetails(int mangaIndex, Controller controller)
        {
            InitializeComponent();
            _mangaIndex = mangaIndex;
            _controller = controller;
            List<Manga> _mangas = _controller.InitializeMangaList();
            _isMangaInList = _controller.IsMediaInList("mangas", _mangas[mangaIndex].id);
            _isMangaFinished = _controller.IsMediaFinished("mangas", _mangas[mangaIndex].id);

            var _mangaCoverURL = _mangas[mangaIndex].Cover;
            var _rndMangaBannerURL = _mangas[mangaIndex].bannerImage;
            _mangaTitleRomaji = _mangas[mangaIndex].Title[0];

            if (_mangas[mangaIndex].Title[1] != "")
            {
                _mangaTitleNative = _mangas[mangaIndex].Title[1];
            }
            else
            {
                NativeButton.Visibility = Visibility.Hidden;
            }

            if(_isMangaInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
            }

            if(_isMangaInList == true)
            {
                if (_isMangaFinished == true)
                {
                    FinishMangaVisibility = Visibility.Collapsed;
                }
                else
                {
                    FinishMangaVisibility = Visibility.Visible;
                }
            }
            else
            {
                FinishMangaVisibility = Visibility.Collapsed;
            }

            if (_isMangaInList == true)
            {
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                RemoveFromListVisibility = Visibility.Collapsed;
            }

            var _mangaTags = _mangas[mangaIndex].Tags;
            var _mangaVolumes = _mangas[mangaIndex].volumes;
            var _mangaChapters = _mangas[mangaIndex].chapters;
            var _mangaStatus = _mangas[mangaIndex].Status;
            var _mangaNote = _mangas[mangaIndex].averageScore;

            BitmapImage _mangaCoverSet = new BitmapImage();
            _mangaCoverSet.BeginInit();
            _mangaCoverSet.UriSource = new Uri(_mangaCoverURL, UriKind.Absolute);
            _mangaCoverSet.EndInit();

            BitmapImage _MangaBannerSet = new BitmapImage();
            _MangaBannerSet.BeginInit();
            _MangaBannerSet.UriSource = new Uri(_rndMangaBannerURL, UriKind.Absolute);
            _MangaBannerSet.EndInit();

            foreach(string _tag in _mangas[mangaIndex].Tags)
            {
                tagsBox.Items.Add(_tag);
            }

            cover.ImageSource = _mangaCoverSet;
            Banner.ImageSource = _MangaBannerSet;
            title.Content = _mangaTitleRomaji;
            volumes.Content = _mangaVolumes;
            chapters.Content = _mangaChapters;
            status.Content = _mangaStatus;
            note.Content = _mangaNote;
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
            List<Manga> _mangas = _controller.InitializeMangaList();
            _controller.AddMedia("mangas", _mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", _mangas[_mangaIndex].id);

            if (_isMangaInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
                FinishMangaVisibility = Visibility.Visible;
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
                FinishMangaVisibility = Visibility.Collapsed;
                RemoveFromListVisibility = Visibility.Collapsed;
            }
        }

        private void FinishManga(object sender, MouseEventArgs e)
        {
            List<Manga> _mangas = _controller.InitializeMangaList();
            _controller.FinishMedia("mangas", _mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", _mangas[_mangaIndex].id);
            _isMangaFinished = _controller.IsMediaFinished("mangas", _mangas[_mangaIndex].id);

            if (_isMangaInList == true)
            {
                if (_isMangaFinished == true)
                {
                    FinishMangaVisibility = Visibility.Collapsed;
                }
                else
                {
                    FinishMangaVisibility = Visibility.Visible;
                }
            }
            else
            {
                FinishMangaVisibility = Visibility.Collapsed;
            }
        }

        private void RemoveFromList(object sender, MouseEventArgs e)
        {
            List<Manga> _mangas = _controller.InitializeMangaList();
            _controller.RemoveMedia("mangas", _mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", _mangas[_mangaIndex].id);

            if (_isMangaInList == true)
            {
                AddToListVisibility = Visibility.Collapsed;
                FinishMangaVisibility = Visibility.Visible;
                RemoveFromListVisibility = Visibility.Visible;
            }
            else
            {
                AddToListVisibility = Visibility.Visible;
                FinishMangaVisibility = Visibility.Collapsed;
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

        public Visibility FinishMangaVisibility
        {
            get => _finishMangaVisibility;
            set
            {
                _finishMangaVisibility = value;
                NotifyPropertyChanged(nameof(FinishMangaVisibility));
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

        private void GoBack(object sender, MouseEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
