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

namespace project_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Generator g;

        bool repeat = false;

        public MainWindow()
        {
            InitializeComponent();

            g = new Generator();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            g.Generate(1, 4, 1);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                g.Generate(1, 4, 1);
            if (e.Key == Key.R)
            {
                if (g.repeat)
                    g.repeat = false;
                else
                    g.Repeat(1, 4, 1);
            }
        }
    }
}
