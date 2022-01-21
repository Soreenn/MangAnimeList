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
    /// Logique d'interaction pour TrackList.xaml
    /// </summary>
    public partial class TrackList : Window, INotifyPropertyChanged
    {
        private Visibility _mangaVisibility;
        private Visibility _animeVisibility;
        private Controller _controller;
        private List<Anime> _animes;
        private List<AnimeWatchList> _animesWatchlist;
        private List<MangaWatchList> _mangasWatchlist;
        private List<Manga> _mangas;
        private List<string> _mediaType = new List<string>();

        public TrackList(Controller controller)
        {
            _controller = controller;
            _animes = _controller.InitializeAnimeList().ToList();
            _mangas = _controller.InitializeMangaList().ToList();
            _animesWatchlist = controller.GetAnimeWatchlist().ToList();
            _mangasWatchlist = controller.GetMangaWatchlist().ToList();
            string animesType = "Animes";
            string mangasType = "Mangas";
            _mediaType.Add(animesType);
            _mediaType.Add(mangasType);
            MangaVisibility = Visibility.Collapsed;
            AnimeVisibility = Visibility.Visible;
            InitializeComponent();
        }
        public Visibility AnimeVisibility
        {
            get => _animeVisibility;
            set
            {
                _animeVisibility = value;
                NotifyPropertyChanged(nameof(AnimeVisibility));
            }
        }
        public Visibility MangaVisibility
        {
            get => _mangaVisibility;
            set
            {
                _mangaVisibility = value;
                NotifyPropertyChanged(nameof(MangaVisibility));
            }
        }

        private void ShowMedia(object sender, MouseEventArgs e)
        {
            if (sender is Grid ctrl)
            {
                if (ctrl.DataContext is Anime anime)
                {
                    int animeIndex = anime.id;
                    AnimeDetails window = new AnimeDetails(animeIndex, _controller);
                    window.ShowDialog();
                }
                else if (ctrl.DataContext is Manga manga)
                {
                    int mangaIndex = _controller.GetMangaIndex(manga.id);
                    MangaDetails window = new MangaDetails(mangaIndex, _controller);
                    window.ShowDialog();
                }
            }
        }

        private void GoBack(object sender, MouseEventArgs e)
        {
            MainWindow window = new MainWindow(_controller);
            Close();
            window.ShowDialog();
        }

        public List<string> MediaType
        {
            get { return _mediaType; }
        }

        public List<AnimeWatchList> AnimesUnfinished
        {
            get { return _animesWatchlist.Where(watchlist => watchlist.State.Equals("UNFINISHED")).ToList(); }
        }
        public List<AnimeWatchList> AnimesFinished
        {
            get { return _animesWatchlist.Where(watchlist => watchlist.State.Equals("FINISHED")).ToList(); }
        }

        public List<MangaWatchList> MangasUnfinished
        {
            get { return _mangasWatchlist.Where(watchlist => watchlist.State.Equals("UNFINISHED")).ToList(); }
        }
        public List<MangaWatchList> MangasFinished
        {
            get { return _mangasWatchlist.Where(watchlist => watchlist.State.Equals("FINISHED")).ToList(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void listType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listType.SelectedItem.ToString() == "Animes")
            {
                MangaVisibility = Visibility.Collapsed;
                AnimeVisibility = Visibility.Visible;
                NotifyPropertyChanged(nameof(AnimesUnfinished));
                NotifyPropertyChanged(nameof(AnimesFinished));
                _animesWatchlist = _controller.GetAnimeWatchlist().ToList();
                _mangasWatchlist = _controller.GetMangaWatchlist().ToList();
            }
            else
            {
                AnimeVisibility = Visibility.Collapsed;
                MangaVisibility = Visibility.Visible;
                NotifyPropertyChanged(nameof(MangasUnfinished));
                NotifyPropertyChanged(nameof(MangasFinished));
                _animesWatchlist = _controller.GetAnimeWatchlist().ToList();
                _mangasWatchlist = _controller.GetMangaWatchlist().ToList();
            }

        }
    }
}
