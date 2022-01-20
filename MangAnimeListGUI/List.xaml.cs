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
    /// Logique d'interaction pour List.xaml
    /// </summary>
    public partial class List : Window, INotifyPropertyChanged
    {
        private string _query;
        private Controller _controller;
        private List<Anime> _animes;
        private string[] _animesListOfUser;
        private List<Manga> _mangas;
        private List<string> _tags;


        public List(Controller controller)
        {
            _tags = new List<string>().ToList(); ;
            _tags.Add("default");
            _controller = controller;
            _animes = _controller.InitializeAnimeList().ToList();
            _mangas = _controller.InitializeMangaList().ToList();

            for (int i = _animes.Count - 1; i >= 0; i--)
            {
                for (int e = _animes[i].Tags.Count - 1; e >= 0; e--)
                {
                    if (!_tags.Contains(_animes[i].Tags[e]))
                    {
                        _tags.Add(_animes[i].Tags[e]);
                    }
                }
            }
            _query = "";

            InitializeComponent();
        }

        private void SearchMedia(object sender, MouseEventArgs e)
        {
            _query = SearchQuery.Text;
            NotifyPropertyChanged(nameof(Mangas));
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

        public List<string> Tags
        {
            get
            {
                return _tags;
            }
        }

        public List<Manga> Mangas
        {
            get
            {
                if (_query == "")
                {
                    if (tag.SelectedItem == tag.Items[0])
                    {
                        return _controller.GetMangaHomeList;
                    }
                    else
                    {
                        return _mangas.Where(manga => manga.Tags.Contains(tag.SelectedItem)).ToList();
                    }
                }
                else
                {
                    if (tag.SelectedItem == tag.Items[0])
                    {
                        return _mangas.Where(manga => manga.Title[1].ToLower().Contains(_query.ToLower())).ToList();
                    }
                    else
                    {
                        List<Manga> _mangasFiltered = _mangas.Where(manga => manga.Title[1].ToLower().Contains(_query.ToLower())).ToList();
                        return _mangasFiltered.Where(manga => manga.Tags.Contains(tag.SelectedItem)).ToList();
                    }
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}