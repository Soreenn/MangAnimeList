﻿using System;
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
using System.Windows.Threading;
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
            loadingBar.Visibility = Visibility.Visible;
            _controller.Login(usernameLabel.Text, passwordLabel.Text);
            _controller.IsConnected = true;
            MainWindow window = new MainWindow(_controller);
            Close();
            window.ShowDialog();
        }
    }
}
