using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Controller _controller;

        public Login(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
        }
        private void LoginUser(object sender, MouseEventArgs e)
        {
            _controller.Login(usernameLabel.Text, passwordLabel.Text);
            this.Close();
        }
    }
}
