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
        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            Generate_Beat();
            Generate_Chord_Progression();

            Generate_XML();
        }

        private void Generate_Beat()
        {
            beat = new List<Beat>();

            beat.Add(new Beat { interval = 1, duration = 2 });
            beat.Add(new Beat { interval = 5, duration = 2 });
            beat.Add(new Beat { interval = 8, duration = 2 });
        }

        private List<Note>  Generate_Bass(int measure)
        {
            int progression_index = progression[measure - 1];

            List<Note> notes = new List<Note>();

            Note base_note = new Note { mw = this, letter = GetLetterFromScaleNumber(progression_index), duration = 2, octave = 3, stave = 2 };
            
            foreach(Beat b in beat)
            {
                Note n = Note_Plus_Interval(base_note, b.interval);
                n.duration = b.duration;
                notes.Add(n);
            }

            if(measure != 1)
            {
                int octave_shift = Find_Best_Octave_Shift(notes);

                if (octave_shift != 0)
                {
                    List<Note> octave_shifted = new List<Note>();
                    foreach (Note n in notes)
                    {
                        Note new_note = n;
                        new_note.octave += octave_shift;
                        octave_shifted.Add(new_note);
                    }

                    bass_notes.AddRange(octave_shifted);

                    return octave_shifted;
                }
            }

            bass_notes.AddRange(notes);

            return notes;
        }

        private List<Note> Generate_Melody()
        {
            List<Note> notes = new List<Note>();

            return notes;
        }

        private void Generate_Chord_Progression()
        {
            progression = new List<int>{ 1, 3, 6, 3};
        }        
    }
}
