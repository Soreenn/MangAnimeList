using System;
using System.Windows;
using System.Windows.Threading;
using MangAnimeList;

namespace MangAnimeListGUI
{
    /// <summary>
    /// Logique d'interaction pour SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer dT = new DispatcherTimer();
        DispatcherTimer dL = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();

            dT.Tick += new EventHandler(dt_Tick);
            dL.Tick += new EventHandler(dL_Tick);
            dT.Interval = new TimeSpan(0, 0, 3);
            dL.Interval = TimeSpan.FromMilliseconds(72.5);
            dL.Start();
            dT.Start();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            Login MangAnimeList = new Login(controller);
            MangAnimeList.Show();

            dT.Stop();
            this.Close();
        }

        private void dL_Tick(object sender, EventArgs e)
        {
            if(loadingBar.Value >= 100)
            {
                dL.Stop();
            }
            else
            {
                loadingBar.Value += 5;
            }
        }
    }
}
