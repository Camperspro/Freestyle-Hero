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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Freestyle_Hero
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Width = 588;
            InitializeComponent();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            gameWindow newGame = new gameWindow();
            newGame.Width = 587;
            newGame.Height = 943;
            newGame.Show();
            this.Close();
        }
    }
}
