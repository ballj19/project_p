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
            }

            Intervals.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            
            Intervals.Items.Add(new ListBoxItem { Content = "0", Tag = 0 });
            Intervals.Items.Add(new ListBoxItem { Content = "m2", Tag = 1 });
            Intervals.Items.Add(new ListBoxItem { Content = "M2", Tag = 2 });
            Intervals.Items.Add(new ListBoxItem { Content = "m3", Tag = 3 });
            Intervals.Items.Add(new ListBoxItem { Content = "M3", Tag = 4 });
            Intervals.Items.Add(new ListBoxItem { Content = "P4", Tag = 5 });
            Intervals.Items.Add(new ListBoxItem { Content = "P5", Tag = 7 });
            Intervals.Items.Add(new ListBoxItem { Content = "m6", Tag = 8 });
            Intervals.Items.Add(new ListBoxItem { Content = "M6", Tag = 9 });
            Intervals.Items.Add(new ListBoxItem { Content = "m7", Tag = 10 });
            Intervals.Items.Add(new ListBoxItem { Content = "M7", Tag = 11 });
            Intervals.Items.Add(new ListBoxItem { Content = "P8", Tag = 12 });

            NumberOfBars.SelectedIndex = 0;
            NumberOfNotes.SelectedIndex = 1;

            g = new Generator(this);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            var list = Intervals.SelectedItems;


            //r = new SongRandomizer(int.Parse(NumberOfBars.SelectedValue.ToString()), int.Parse(BiggestNoteHop.SelectedValue.ToString()), int.Parse(Tempo.Text));
            g.SetParams(int.Parse(NumberOfBars.SelectedValue.ToString()), Intervals.SelectedItems, int.Parse(Tempo.Text), int.Parse(NumberOfNotes.Text), ExactNumberOfNotes.IsChecked == true);
            g.NextBar();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                g.NextBar();
            if (e.Key == Key.R)
                g.PlayBars();
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
