using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window , INotifyPropertyChanged
    {
        Controller _controller;
        private Visibility _progressBar;
        DispatcherTimer dT = new DispatcherTimer();
        DispatcherTimer dL = new DispatcherTimer();

        public Login(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
            ProgressBarVisibility = Visibility.Collapsed;
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBar;
            set
            {
                _progressBar = value;
                NotifyPropertyChanged(nameof(ProgressBarVisibility));
            }
        }


        private void LoginByLabel(object sender, MouseEventArgs e)
        {
            LoginUser();
        }

        private void LoginByPressingEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginUser();
            }
        }

        private void LoginUser()
        {
            ProgressBarVisibility = Visibility.Visible;

            string errorMessage = _controller.Login(usernameLabel.Text, passwordLabel.Password);
            if (_controller._IsConnected == true)
            {
                dT.Tick += new EventHandler(dt_Tick);
                dL.Tick += new EventHandler(dL_Tick);
                dT.Interval = new TimeSpan(0, 0, 2);
                dL.Interval = TimeSpan.FromMilliseconds(10);
                dL.Start();
                dT.Start();
            }
            else
            {
                MessageBox.Show(errorMessage, "Error !");
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            MainWindow MangAnimeList = new MainWindow(_controller);
            MangAnimeList.Show();

            dT.Stop();
            this.Close();
        }

        private void dL_Tick(object sender, EventArgs e)
        {
            if (loadingBar.Value >= 100)
            {
                dL.Stop();
            }
            else
            {
                loadingBar.Value += 1.2;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RegisterPage(object sender, MouseEventArgs e)
        {
            Register window = new Register(_controller);
            Close();
            window.ShowDialog();
        }
    }
}
