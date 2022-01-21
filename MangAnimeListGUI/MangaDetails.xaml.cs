using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            List<Manga> mangas = _controller.InitializeMangaList();
            _isMangaInList = _controller.IsMediaInList("mangas", mangas[mangaIndex].id);
            _isMangaFinished = _controller.IsMediaFinished("mangas", mangas[mangaIndex].id);

            var mangaCoverURL = mangas[mangaIndex].Cover;
            var rndMangaBannerURL = mangas[mangaIndex].bannerImage;
            _mangaTitleRomaji = mangas[mangaIndex].Title[0];

            if (mangas[mangaIndex].Title[1] != "")
            {
                _mangaTitleNative = mangas[mangaIndex].Title[1];
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
            List<Manga> mangas = _controller.InitializeMangaList();
            _controller.AddMedia("mangas", mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", mangas[_mangaIndex].id);

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
            List<Manga> mangas = _controller.InitializeMangaList();
            _controller.FinishMedia("mangas", mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", mangas[_mangaIndex].id);
            _isMangaFinished = _controller.IsMediaFinished("mangas", mangas[_mangaIndex].id);

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
            List<Manga> mangas = _controller.InitializeMangaList();
            _controller.RemoveMedia("mangas", mangas[_mangaIndex].id);
            _isMangaInList = _controller.IsMediaInList("mangas", mangas[_mangaIndex].id);

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
