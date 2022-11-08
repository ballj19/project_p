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

namespace project_m
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<int> midi_naturals = new List<int>() { 0, 2, 4, 5, 7, 9, 11 };
        List<int> major_scale = new List<int>() { 0, 2, 2, 1, 2, 2, 2 };

        public struct KeySignature
        {
            public char letter;
            public Accidental accidental;
        }

        public struct TimeSignature
        {
            public int bpm;
            public int top;
            public int bottom;
            public int tick_divisions;
        }

        KeySignature key_signature = new KeySignature
        {
            letter = 'C',
            accidental = Accidental.Natural
        };

        TimeSignature time_signature = new TimeSignature
        {
            top = 4,
            bottom = 4,
            bpm = 100,
            tick_divisions = 2
        };

        string filepath = @"C:\Users\jakeb\Documents\test.musicxml";

        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
