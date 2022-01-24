using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour Search.xaml
    /// </summary>
    public partial class Search : Window, INotifyPropertyChanged
    {
        private string _query;
        private Controller _controller;
        private List<Anime> _animes;
        private List<Manga> _mangas;
        private List<string> _tags;

        public Search(Controller controller)
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

        private void SearchMedia()
        {
            _query = SearchQuery.Text;
            NotifyPropertyChanged(nameof(Animes));
            NotifyPropertyChanged(nameof(Mangas));
        }

        private void ShowMedia(object sender, MouseEventArgs e)
        {
            if (sender is Grid ctrl)
            {
                if (ctrl.DataContext is Anime _anime)
                {
                    int _animeIndex = _anime.id -1;
                    AnimeDetails _window = new AnimeDetails(_animeIndex, _controller);
                    _window.ShowDialog();
                }
                else if (ctrl.DataContext is Manga _manga)
                {
                    int _mangaIndex = _controller.GetMangaIndex(_manga.id);
                    MangaDetails _window = new MangaDetails(_mangaIndex, _controller);
                    _window.ShowDialog();
                }
            }
        }

        private void GoBack(object sender, MouseEventArgs e)
        {
            Close();
        }

        public List<string> Tags
        {
            get
            {
                return _tags;
            }
        }

        public List<Anime> Animes
        {
            get
            {
                if(_query == "")
                {
                    if (tag.SelectedItem.Equals(tag.Items[0]))
                    {
                        return _controller.InitializeAnimeList();
                    }
                    else {
                        return _animes.Where(anime => anime.Tags.Contains(tag.SelectedItem)).ToList();
                    }     
                }
                else
                {
                    if(tag.SelectedItem.Equals(tag.Items[0]))
                    {
                        return _animes.Where(anime => anime.Title[0].ToLower().Contains(_query.ToLower())).ToList();
                    }
                    else
                    {
                        List<Anime> _animesFiltered = _animes.Where(anime => anime.Title[0].ToLower().Contains(_query.ToLower())).ToList();
                        return _animesFiltered.Where(anime => anime.Tags.Contains(tag.SelectedItem)).ToList();
                    }
                }               
            }
        }

        public List<Manga> Mangas
        {
            get
            {
                if (_query == "")
                {
                    if (tag.SelectedItem.Equals(tag.Items[0]))
                    {
                        return _controller.InitializeMangaList();
                    }
                    else
                    {
                        return _mangas.Where(manga => manga.Tags.Contains(tag.SelectedItem)).ToList();
                    }
                }
                else
                {
                    if (tag.SelectedItem.Equals(tag.Items[0]))
                    {
                        return _mangas.Where(manga => manga.Title[0].ToLower().Contains(_query.ToLower())).ToList();
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

        private void UpdateTag(object sender, SelectionChangedEventArgs e)
        {
            SearchMedia();
        }

        private void UpdateText(object sender, TextChangedEventArgs e)
        {
            SearchMedia();
        }
    }
}
