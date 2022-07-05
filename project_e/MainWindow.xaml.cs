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
using System.Windows.Forms;
using project_p;
using System.IO;

namespace project_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SongRandomizer r;
        Generator g;

        public MainWindow()
        {
            InitializeComponent();

            for(int i = 1; i <= 10; i++)
            {
                NumberOfNotes.Items.Add(i);
                NumberOfBars.Items.Add(i);
                BiggestNoteHop.Items.Add(i);
            }

            NumberOfBars.SelectedIndex = 0;
            BiggestNoteHop.SelectedIndex = 0;
            NumberOfNotes.SelectedIndex = 1;

            g = new Generator(this);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            //r = new SongRandomizer(int.Parse(NumberOfBars.SelectedValue.ToString()), int.Parse(BiggestNoteHop.SelectedValue.ToString()), int.Parse(Tempo.Text));
            g.SetParams(int.Parse(NumberOfBars.SelectedValue.ToString()), int.Parse(BiggestNoteHop.SelectedValue.ToString()), int.Parse(Tempo.Text), int.Parse(NumberOfNotes.Text));
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                g.NextBar();
            if (e.Key == Key.R)
                g.PlayBar();
        }


        private void ImportFileButton_Click(object sender, RoutedEventArgs e)
        {
            SongImporter si = new SongImporter();

            foreach (string filepath in Directory.GetFiles(@"C:\Users\ballj\Downloads\project_e"))
            {
                si.Import(filepath);
            }
        }
    }
}
