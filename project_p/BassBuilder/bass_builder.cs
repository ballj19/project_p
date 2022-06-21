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

namespace project_p
{
    /// <summary>
    /// Interaction logic for BassBuilder.xaml
    /// </summary>
    public partial class MainWindow
    {
        public void BassBuilder()
        {
            InitializeComponent();

            FillChords();
            FillInversions();
            FillOctaves();
            FillActivations();
            FillPatterns();
        }

        private void FillChords()
        {
            ChordName.Items.Add("1 Major");
            ChordName.Items.Add("2 Minor");
            ChordName.Items.Add("3 Minor");
            ChordName.Items.Add("4 Major");
            ChordName.Items.Add("5 Major");
            ChordName.Items.Add("6 Minor");
            ChordName.Items.Add("7 Dim");

            ChordName.SelectedIndex = 0;
        }

        private void FillInversions()
        {
            Inversion.Items.Add("None");
            Inversion.Items.Add(1);
            Inversion.Items.Add(2);

            Inversion.SelectedIndex = 0;
        }

        private void FillOctaves()
        {
            OctaveStart.Items.Add(-1);
            OctaveStart.Items.Add(0);
            OctaveStart.Items.Add(1);
            OctaveStart.Items.Add(2);
            OctaveStart.Items.Add(3);
            OctaveStart.Items.Add(4);
            OctaveStart.Items.Add(5);
            OctaveStart.Items.Add(6);

            OctaveStart.SelectedIndex = 3;
        }

        private void FillActivations()
        {

            Activations.Items.Add(0);
            Activations.Items.Add(1);
            Activations.Items.Add(2);
            Activations.Items.Add(3);
            Activations.Items.Add(4);
            Activations.Items.Add(5);
            Activations.Items.Add(6);

            Activations.SelectedIndex = 0;
        }

        private void FillPatterns()
        {
            Pattern.Items.Add("Quarter Note");
            Pattern.Items.Add("Pyramid");
            Pattern.Items.Add("Climb9");
            Pattern.Items.Add("First Fifth Octave");
            Pattern.Items.Add("Random");

            Pattern.SelectedIndex = 0;
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            ResetBass();

            switch (Pattern.SelectedItem.ToString())
            {
                case "Quarter Note":
                    QuarterNote();
                    break;
                case "Pyramid":
                    Pyramid();
                    break;
                case "Climb9":
                    Climb9();
                    break;
                case "First Fifth Octave":
                    FirstFifthOctave();
                    break;
                case "Random":
                    Random();
                    break;
            }


            SortTicks();
        }



        private void ResetBass()
        {
            List<PianoControl> ticks_for_removal = new List<PianoControl>();

            foreach (PianoControl p in Timeline.Children.OfType<PianoControl>())
            {
                foreach (PianoKey k in p.piano.Children.OfType<PianoKey>())
                {
                    if (k.state == PianoKey.KeyState.Bass)
                    {
                        k.state = PianoKey.KeyState.Off;
                        k.StateChanged();
                    }
                }

                if (!p.HasMelody())
                    ticks_for_removal.Add(p);
            }

            foreach (PianoControl p in ticks_for_removal)
                Timeline.Children.Remove(p);
        }

        private void QuarterNote()
        {
            byte first;
            byte fifth;

            if (Inversion.SelectedIndex == 0)
            {
                first = GetNoteValue(1);
                fifth = GetNoteValue(5);
            }
            else if (Inversion.SelectedIndex == 1)
            {
                first = GetNoteValue(3);
                fifth = GetNoteValue(8);
            }
            else
            {
                first = GetNoteValue(5);
                fifth = GetNoteValue(10);
            }


            GetTick(1).SetPianoKey(first, PianoKey.KeyState.Bass);
            GetTick(1).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            GetTick(5).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            GetTick(9).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            GetTick(13).SetPianoKey(fifth, PianoKey.KeyState.Bass);
        }

        private void Pyramid()
        {
            List<int> pattern = InvertPattern(new List<int> { 1, 5, 10, 8, 12, 17, 15, 19, 24 });

            Activate(pattern);
        }

        private void Climb9()
        {
            List<int> pattern = InvertPattern(new List<int> { 1, 5, 8, 9, 10, 12, 15, 16, 17 });

            Activate(pattern);
        }

        private void FirstFifthOctave()
        {
            List<int> pattern = InvertPattern(new List<int> { 1, 5, 8, 8, 8, 8, 8, 8 });

            Activate(pattern);
        }

        private void Random()
        {
            ResetBass();


            List<int> valid_notes = new List<int> { 1, 2, 3, 5, 7, 8, 9, 10, 12 };

            List<int> pattern = new List<int> { 1 };

            Random r = new Random();

            for (int i = 0; i < Activations.SelectedIndex; i++)
            {
                int next_note = r.Next(0, valid_notes.Count - 1);

                pattern.Add(valid_notes[next_note]);
            }


            pattern = InvertPattern(pattern);

            Activate(pattern);

            SortTicks();

            PlayButton_Click(null, null);
        }

        private List<int> InvertPattern(List<int> pattern)
        {
            for (int i = 0; i < Inversion.SelectedIndex; i++)
                pattern.RemoveAt(0);

            return pattern;
        }

        private void Activate(List<int> pattern)
        {
            int number_of_activations = Activations.SelectedIndex;

            GetTick(1).SetPianoKey(GetNoteValue(pattern[0]), PianoKey.KeyState.Bass);

            List<int> activations = new List<int>();
            activations.AddRange(FillQuarterNotes(ref number_of_activations));
            activations.AddRange(FillEighthNotes(ref number_of_activations));
            activations.Sort();

            for (int i = 0; i < activations.Count; i++)
            {
                GetTick(activations[i]).SetPianoKey(GetNoteValue(pattern[i + 1]), PianoKey.KeyState.Bass);
            }
        }

        private List<int> FindMelodyNotes()
        {
            List<int> melody_notes = new List<int>();

            foreach (PianoControl p in Timeline.Children.OfType<PianoControl>())
            {
                foreach (PianoKey k in p.piano.Children.OfType<PianoKey>())
                {
                    if (k.state == PianoKey.KeyState.Melody)
                    {
                        melody_notes.Add(p.Tick());
                        break;
                    }
                }
            }

            return melody_notes;
        }

        private List<int> FillQuarterNotes(ref int activations)
        {
            List<int> place_activations = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if (!HasTick(i * 4 + 1) && activations > 0)
                {
                    place_activations.Add(i * 4 + 1);
                    activations--;
                }
            }

            return place_activations;
        }

        private List<int> FillEighthNotes(ref int activations)
        {
            List<int> place_activations = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if (!HasTick(i * 4 + 3) && activations > 0)
                {
                    place_activations.Add(i * 4 + 3);
                    activations--;
                }
            }

            return place_activations;
        }

        private int BiggestHole(List<int> notes)
        {
            int biggest = 0;
            int tick_at_biggest_gap = 1;

            for (int i = 0; i < notes.Count; i++)
            {
                int distance = 0;

                if (i == 0)
                    distance = notes[i] - 1;
                else
                    distance = notes[i] - notes[i - 1];

                if (distance > biggest)
                {
                    biggest = distance;

                    tick_at_biggest_gap = notes[i] - distance / 2;  //Find halfway point between two notes
                }
            }

            return tick_at_biggest_gap;
        }

        private byte GetNoteValue(int scale_number)
        {
            int base_note = key_signature_offset + 12 * (int)OctaveStart.SelectedValue + 12;

            int[] major_scale = { 2, 2, 1, 2, 2, 2, 1 };

            int final_note = base_note;
            int scale_position = 0;

            for (int i = 1; i < scale_number + ChordName.SelectedIndex; i++)
            {
                final_note += major_scale[scale_position];

                scale_position++;

                if (scale_position == major_scale.Length)
                    scale_position = 0;
            }

            return (byte)final_note;
        }
    }
}
