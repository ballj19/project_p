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
using System.Xml;
using System.IO;

namespace project_m
{
    public partial class MainWindow : Window
    {
        List<Beat> beat;
        List<int> progression;
        List<Note> bass_notes = new List<Note>();


        public enum Accidental
        {
            Natural = 0,
            Flat = -1,
            Sharp = 1
        }

        public struct Note
        {
            public MainWindow mw;
            public char letter;
            public int duration;
            public int octave;
            public int stave;

            public Accidental accidental
            {
                get
                {


                    return Accidental.Natural;
                }
            }

            public int scale_number
            {
                get
                {
                    int scale_number = letter - mw.key_signature.letter + 1;

                    if (scale_number <= 0)
                        scale_number += 7;

                    return scale_number;
                }
            }

            private int midi_base
            {
                get
                {
                    int major_scale_index = letter - 'C' + 1;
                    if (major_scale_index <= 0)
                        major_scale_index += 7;

                    int midi_base = 0;
                    for (int i = 0; i < major_scale_index; i++)
                    {
                        midi_base += mw.major_scale[i];
                    }

                    return midi_base;
                }
            }

            public int midi_value
            {
                get
                {
                    //Middle C = 60
                    return 12 * (octave + 1) + midi_base + (int)accidental;
                }
            }
        }

        public struct Beat
        {
            public int interval;
            public int duration;
        }

        private Note Note_Plus_Interval(Note note, int interval)
        {
            char new_letter = note.letter;
            
            for (int i = 0; i < interval - 1; i++)
            {
                new_letter++;

                if (new_letter == 'C')
                    note.octave++;

                if (new_letter > 'G')
                    new_letter = (char)(new_letter - 7);
            }

            note.letter = new_letter;

            return note;
        }

        private char GetLetterFromScaleNumber(int scale_number)
        {
            char letter = key_signature.letter;

            for (int i = 0; i < scale_number - 1; i++)
            {
                letter++;

                if (letter > 'G')
                    letter = (char)(letter - 7);
            }

            return letter;
        }

        private int Average_Bass_Note()
        {
            int sum = 0;

            foreach(Note n in bass_notes)
            {
                sum += n.midi_value;
            }

            return sum / bass_notes.Count;
        }

        private int Average_Bass_Value(List<Note> bass, int octave_shift = 0)
        {
            int sum = 0;

            foreach(Note n in bass)
            {
                Note octave_shifted = n;
                octave_shifted.octave += octave_shift;
                sum += octave_shifted.midi_value;
            }

            return sum / bass.Count;
        }

        private int Find_Best_Octave_Shift(List<Note> bass)
        {
            int average_bass = Average_Bass_Note();

            int closest_octave = 0;
            int closest_average = Average_Bass_Value(bass);

            if(Math.Abs(Average_Bass_Value(bass, -1) - average_bass) < Math.Abs(closest_average - average_bass))
            {
                closest_octave = -1;
                closest_average = Average_Bass_Value(bass, -1);
            }

            if (Math.Abs(Average_Bass_Value(bass, 1) - average_bass) < Math.Abs(closest_average - average_bass))
            {
                closest_octave = 1;
                closest_average = Average_Bass_Value(bass, 1);
            }

            return closest_octave;
        }
    }
}
