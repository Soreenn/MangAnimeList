using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MangAnimeList;
using System.Text.RegularExpressions;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _easterIndex = 0;
        Controller _controller;

        public MainWindow(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.GenerateRandomHomeList();
            List<Manga> _mangas = _controller.GetMangaHomeList;
            List<Anime> _animes = _controller.GetAnimeHomeList;

            if(_controller._IsConnected == false)
            {
                username.Content = "Guest";
            }
            else
            {
                username.Content = _controller.GetUsername;
            }

            int _loopManga = 1;
            int _loopAnime = 1;
            foreach (Manga _manga in _mangas)
            {
                if (_loopManga == 1)
                {
                    BitmapImage _MangaImage = new BitmapImage();
                    _MangaImage.BeginInit();
                    _MangaImage.UriSource = new Uri(_manga.Cover, UriKind.Absolute);
                    _MangaImage.EndInit();
                    rndManga1.ImageSource = _MangaImage;
                }
                if (_loopManga == 2)
                {
                    BitmapImage _MangaImage = new BitmapImage();
                    _MangaImage.BeginInit();
                    _MangaImage.UriSource = new Uri(_manga.Cover, UriKind.Absolute);
                    _MangaImage.EndInit();
                    rndManga2.ImageSource = _MangaImage;
                }
                if (_loopManga == 3)
                {
                    BitmapImage _MangaImage = new BitmapImage();
                    _MangaImage.BeginInit();
                    _MangaImage.UriSource = new Uri(_manga.Cover, UriKind.Absolute);
                    _MangaImage.EndInit();
                    rndManga3.ImageSource = _MangaImage;
                }
                if (_loopManga == 4)
                {
                    BitmapImage _MangaImage = new BitmapImage();
                    _MangaImage.BeginInit();
                    _MangaImage.UriSource = new Uri(_manga.Cover, UriKind.Absolute);
                    _MangaImage.EndInit();
                    rndManga4.ImageSource = _MangaImage;
                }
                _loopManga++;
            }

            foreach (Anime _anime in _animes)
            {
                if (_loopAnime == 1)
                {
                    BitmapImage _AnimeImage = new BitmapImage();
                    _AnimeImage.BeginInit();
                    _AnimeImage.UriSource = new Uri(_anime.Cover, UriKind.Absolute);
                    _AnimeImage.EndInit();
                    rndAnime1.ImageSource = _AnimeImage;
                }
                if (_loopAnime == 2)
                {
                    BitmapImage _AnimeImage = new BitmapImage();
                    _AnimeImage.BeginInit();
                    _AnimeImage.UriSource = new Uri(_anime.Cover, UriKind.Absolute);
                    _AnimeImage.EndInit();
                    rndAnime2.ImageSource = _AnimeImage;
                }
                if (_loopAnime == 3)
                {
                    BitmapImage _AnimeImage = new BitmapImage();
                    _AnimeImage.BeginInit();
                    _AnimeImage.UriSource = new Uri(_anime.Cover, UriKind.Absolute);
                    _AnimeImage.EndInit();
                    rndAnime3.ImageSource = _AnimeImage;
                }
                if (_loopAnime == 4)
                {
                    BitmapImage _AnimeImage = new BitmapImage();
                    _AnimeImage.BeginInit();
                    _AnimeImage.UriSource = new Uri(_anime.Cover, UriKind.Absolute);
                    _AnimeImage.EndInit();
                    rndAnime4.ImageSource = _AnimeImage;
                }
                _loopAnime++;
            }
        }
        private void DisplayManga(object sender, MouseEventArgs e)
        {
            string _name = (((Border)e.Source)).Name;
            string[] _numbers = Regex.Split(_name, @"(\D+)(\d{1})");
            int _homeIndex = Int32.Parse(_numbers[2]) -1;

            List<Manga> _mangas = _controller.GetMangaHomeList;

            int _index = _controller.GetMangaIndex(_mangas[_homeIndex].id);

            MangaDetails window = new MangaDetails(_controller);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void DisplayAnime(object sender, MouseEventArgs e)
        {
            string _name = (((Border)e.Source)).Name;
            string[] _numbers = Regex.Split(_name, @"(\D+)(\d{1})");
            int _homeIndex = Int32.Parse(_numbers[2]) - 5;

            List<Anime> _animes = _controller.GetAnimeHomeList;

            int _index = _animes[_homeIndex].id -1;

            AnimeDetails window = new AnimeDetails(_controller);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void EasterEgg(object sender, MouseEventArgs e)
        {
            _easterIndex += 1;
            if(_easterIndex >= 5)
            {
                EasterEgg window = new EasterEgg(_controller);
                Hide();
                window.ShowDialog();
                Show();
            }
        }
        private void Search(object sender, MouseEventArgs e)
        {
            Search window = new Search(_controller);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void List(object sender, MouseEventArgs e)
        {
            TrackList window = new TrackList(_controller);
            Hide();
            window.ShowDialog();
            Show();
        }
    }
}
