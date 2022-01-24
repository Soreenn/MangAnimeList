using System.Windows;
using System.Windows.Input;
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

        private void RegisterByLabel(object sender, MouseEventArgs e)
        {
            RegisterUser();
        }

        private void RegisterByPressingEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RegisterUser();
            }
        }

        private void RegisterUser()
        {
            string errorMessage = _controller.RegisterUser(usernameLabel.Text, passwordLabel.Password, confirmPasswordLabel.Password);
            if (_controller._IsConnected == true)
            {
                MainWindow window = new MainWindow(_controller);
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
