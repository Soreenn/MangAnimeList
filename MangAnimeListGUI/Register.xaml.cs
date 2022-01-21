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
    /// Logique d'interaction pour Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        Controller _controller;

        public Register(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void RegisterUser(object sender, MouseEventArgs e)
        {
            string errorMessage = _controller.RegisterUser(usernameLabel.Text, passwordLabel.Password, confirmPasswordLabel.Password);
            if (_controller.IsConnected == true)
            {
                Login window = new Login(_controller);
                Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show(errorMessage, "Error !");
            }
        }

        private void LoginPage(object sender, MouseEventArgs e)
        {
            Login window = new Login(_controller);
            Close();
            window.ShowDialog();
        }

    }
}
