using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Controls;
using System.Windows;

namespace project_p
{
    public class BarBuilder : GroupBox
    {
        Grid grid = new Grid();
        MainWindow mw;
        XmlNode bar;

        ComboBox ChordName = new ComboBox();
        ComboBox OctaveStart = new ComboBox();

        Button run_btn = new Button();
        Button view_btn = new Button();
        Button play_around_btn = new Button();

        CheckBox seventh = new CheckBox();
        CheckBox ninth = new CheckBox();
        CheckBox tenth = new CheckBox();
        CheckBox synchopate = new CheckBox();
        CheckBox together = new CheckBox();

        Slider intensity = new Slider();
        Slider complexity = new Slider();

        int number = 0;

        public BarBuilder(MainWindow mw, XmlNode bar)
        {
            this.mw = mw;
            this.bar = bar;

            grid.Height = 100;
            Width = 650;

            number = int.Parse(bar.Name.Substring(3));

            Header = bar.Name;
            Content = grid;

            GetViewBtn();
            GetRunBtn();
            GetPlayAroundBtn();

            GetChordName();
            GetOctaveStart();

            GetSeventh();
            GetNinth();
            GetTenth();
            GetSynchopate();
            GetTogether();
            GetIntensity();
            GetComplexity();
        }

        private void GetSeventh()
        {
            grid.Children.Add(seventh);

            seventh.Content = "7th";
            seventh.Margin = new Thickness(475, 5, 0, 0);
            seventh.HorizontalAlignment = HorizontalAlignment.Left;
            seventh.Width = 100;

            seventh.Click += Save;

            XmlNode xml = bar.SelectSingleNode("Seventh");

            if (xml == null)
                seventh.IsChecked = false;
            else
                seventh.IsChecked = xml.InnerText == "1";
        }

        private void GetNinth()
        {
            grid.Children.Add(ninth);

            ninth.Content = "9th";
            ninth.Margin = new Thickness(475, 25, 0, 0);
            ninth.HorizontalAlignment = HorizontalAlignment.Left;
            ninth.Width = 100;

            ninth.Click += Save;

            XmlNode xml = bar.SelectSingleNode("Ninth");

            if (xml == null)
                ninth.IsChecked = false;
            else
                ninth.IsChecked = xml.InnerText == "1";
        }

        private void GetTenth()
        {
            grid.Children.Add(tenth);

            tenth.Content = "10th";
            tenth.Margin = new Thickness(475, 45, 0, 0);
            tenth.HorizontalAlignment = HorizontalAlignment.Left;
            tenth.Width = 100;

            tenth.Click += Save;

            XmlNode xml = bar.SelectSingleNode("Tenth");

            if (xml == null)
                tenth.IsChecked = false;
            else
                tenth.IsChecked = xml.InnerText == "1";
        }

        private void GetSynchopate()
        {
            grid.Children.Add(synchopate);

            synchopate.Content = "Synchopate";
            synchopate.Margin = new Thickness(475, 65, 0, 0);
            synchopate.HorizontalAlignment = HorizontalAlignment.Left;
            synchopate.Width = 100;

            synchopate.Click += Save;

            XmlNode xml = bar.SelectSingleNode("Synchopate");

            if (xml == null)
                synchopate.IsChecked = false;
            else
                synchopate.IsChecked = xml.InnerText == "1";
        }

        private void GetTogether()
        {
            grid.Children.Add(together);

            together.Content = "Together";
            together.Margin = new Thickness(475, 85, 0, 0);
            together.HorizontalAlignment = HorizontalAlignment.Left;
            together.Width = 100;

            together.Click += Save;

            XmlNode xml = bar.SelectSingleNode("Together");

            if (xml == null)
                together.IsChecked = false;
            else
                together.IsChecked = xml.InnerText == "1";
        }

        private void GetIntensity()
        {
            //grid.Children.Add(intensity);

            intensity.Name = "Intensity";
            intensity.Minimum = 1;
            intensity.Maximum = 5;
        }

        private void GetComplexity()
        {
            grid.Children.Add(complexity);

            complexity.Name = "Complexity";
            complexity.IsSnapToTickEnabled = true;
            complexity.TickFrequency = 1;
            complexity.Ticks = new System.Windows.Media.DoubleCollection { 1.0, 2.0, 3.0, 4.0, 5.0 };
            complexity.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            complexity.Width = 200;
            complexity.Minimum = 1;
            complexity.Maximum = 5;
            complexity.Margin = new Thickness(10, 70, 0, 0);
            complexity.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void GetViewBtn()
        {
            grid.Children.Add(view_btn);

            view_btn.Content = "View";
            view_btn.Click += SwitchToBar;
            view_btn.Width = 50;
            view_btn.Height = 25;
            view_btn.Margin = new Thickness(0, 5, 10, 0);
            view_btn.VerticalAlignment = VerticalAlignment.Top;
            view_btn.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void GetChordName()
        {
            grid.Children.Add(ChordName);

            ChordName.Width = 100;
            ChordName.Height = 25;
            ChordName.Margin = new Thickness(10, 10, 0, 0);
            ChordName.VerticalAlignment = VerticalAlignment.Top;
            ChordName.HorizontalAlignment = HorizontalAlignment.Left;
            
            ChordName.Items.Add("None");
            ChordName.Items.Add("1 Major");
            ChordName.Items.Add("2 Minor");
            ChordName.Items.Add("3 Minor");
            ChordName.Items.Add("4 Major");
            ChordName.Items.Add("5 Major");
            ChordName.Items.Add("6 Minor");
            ChordName.Items.Add("7 Dim");

            XmlNode xml_chord = bar.SelectSingleNode("Chord");

            if (xml_chord == null)
                ChordName.SelectedIndex = 0;
            else
                ChordName.SelectedIndex = int.Parse(xml_chord.InnerText);


            ChordName.SelectionChanged += DefaultChord;
        }

        private void GetOctaveStart()
        {
            grid.Children.Add(OctaveStart);

            OctaveStart.Width = 50;
            OctaveStart.Height = 25;
            OctaveStart.Margin = new Thickness(120, 10, 0, 0);
            OctaveStart.VerticalAlignment = VerticalAlignment.Top;
            OctaveStart.HorizontalAlignment = HorizontalAlignment.Left;

            OctaveStart.Items.Add("0");
            OctaveStart.Items.Add("1");
            OctaveStart.Items.Add("2");
            OctaveStart.Items.Add("3");
            OctaveStart.Items.Add("4");
            OctaveStart.Items.Add("5");

            XmlNode xml_octave = bar.SelectSingleNode("OctaveStart");

            if (xml_octave == null)
                OctaveStart.SelectedIndex = 2;
            else
                OctaveStart.SelectedIndex = int.Parse(xml_octave.InnerText);


            OctaveStart.SelectionChanged += Save;
        }

        private void GetRunBtn()
        {
            grid.Children.Add(run_btn);

            run_btn.Content = "Run";
            run_btn.Click += Run_Btn_Click;
            run_btn.Width = 50;
            run_btn.Height = 25;
            run_btn.Margin = new Thickness(0, 40, 10, 0);
            run_btn.VerticalAlignment = VerticalAlignment.Top;
            run_btn.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void GetPlayAroundBtn()
        {
            grid.Children.Add(play_around_btn);

            play_around_btn.Content = "Play";
            play_around_btn.Click += Play_Around_Click;
            play_around_btn.Width = 50;
            play_around_btn.Height = 25;
            play_around_btn.Margin = new Thickness(0, 75, 10, 0);
            play_around_btn.VerticalAlignment = VerticalAlignment.Top;
            play_around_btn.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void Play_Around_Click(object o = null, RoutedEventArgs e = null)
        {
            int start = number - 1;
            int end = number + 1;

            if (start < 1)
                start = 1;

            if (end > mw.BarBuilder.Children.Count)
                end = number;

            mw.PlayRange(start, end);
        }

        private void Run_Btn_Click(object o = null, RoutedEventArgs e = null)
        {
            SwitchToBar();
            ResetBass();

            List<int> valid_notes = new List<int> { 1, 5, 8, 12 };

            if (seventh.IsChecked == true)
                valid_notes.Add(7);
            if (ninth.IsChecked == true)
                valid_notes.Add(9);
            if (tenth.IsChecked == true)
                valid_notes.Add(10);

            valid_notes.Sort();

            int note_index = 0;

            for(int i = 1; i < mw.player.time_signature_top * 4; i += 2)
            {
                if (note_index == valid_notes.Count)
                    break;

                if (i == 1 || (synchopate.IsChecked == true && !mw.TickHasMelody(i)))
                {
                    mw.GetTick(i).SetPianoKey(GetNoteValue(valid_notes[note_index]), PianoKey.KeyState.Bass);
                    note_index++;
                }
            }

            mw.Save();
        }

        public void SetPlaying(bool play)
        {
            if (play)
            {
                grid.Background = System.Windows.Media.Brushes.Yellow;
                SwitchToBar();
            }
            else
                grid.Background = System.Windows.Media.Brushes.White;
        }

        private void DefaultChord(object o = null, RoutedEventArgs e = null)
        {
            SwitchToBar();

            ResetBass();

            if(ChordName.SelectedIndex != 0)
            {
                mw.GetTick(1).SetPianoKey(GetNoteValue(1), PianoKey.KeyState.Bass);
                mw.GetTick(1).SetPianoKey(GetNoteValue(5), PianoKey.KeyState.Bass);
            }

            Save();
            mw.Save();
        }

        private void SwitchToBar(object o = null, RoutedEventArgs e = null)
        {
            mw.BarNumber.Text = number.ToString();
            mw.LoadBar();
        }

        private void Save(object o = null, RoutedEventArgs e = null)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(mw.filepath);

            XmlNode bar = doc.SelectSingleNode("//Bars/Bar" + number);

            if (bar.SelectSingleNode("Chord") == null)
                bar.AppendChild(doc.CreateElement("Chord"));
            if (bar.SelectSingleNode("OctaveStart") == null)
                bar.AppendChild(doc.CreateElement("OctaveStart"));
            if (bar.SelectSingleNode("Synchopate") == null)
                bar.AppendChild(doc.CreateElement("Synchopate"));
            if (bar.SelectSingleNode("Together") == null)
                bar.AppendChild(doc.CreateElement("Together"));
            if (bar.SelectSingleNode("Seventh") == null)
                bar.AppendChild(doc.CreateElement("Seventh"));
            if (bar.SelectSingleNode("Ninth") == null)
                bar.AppendChild(doc.CreateElement("Ninth"));

            XmlNode chord = doc.SelectSingleNode("//Bars/Bar" + number + "/Chord");
            chord.RemoveAll();
            chord.AppendChild(doc.CreateTextNode(ChordName.SelectedIndex.ToString()));

            XmlNode octave_start = doc.SelectSingleNode("//Bars/Bar" + number + "/OctaveStart");
            octave_start.RemoveAll();
            octave_start.AppendChild(doc.CreateTextNode(OctaveStart.SelectedIndex.ToString()));

            XmlNode synch = doc.SelectSingleNode("//Bars/Bar" + number + "/Synchopate");
            synch.RemoveAll();
            synch.AppendChild(doc.CreateTextNode(synchopate.IsChecked == true ? "1" : "0"));

            XmlNode tog = doc.SelectSingleNode("//Bars/Bar" + number + "/Together");
            tog.RemoveAll();
            tog.AppendChild(doc.CreateTextNode(together.IsChecked == true ? "1" : "0"));

            XmlNode sev = doc.SelectSingleNode("//Bars/Bar" + number + "/Seventh");
            sev.RemoveAll();
            sev.AppendChild(doc.CreateTextNode(seventh.IsChecked == true ? "1" : "0"));

            XmlNode nin = doc.SelectSingleNode("//Bars/Bar" + number + "/Ninth");
            nin.RemoveAll();
            nin.AppendChild(doc.CreateTextNode(ninth.IsChecked == true ? "1" : "0"));

            doc.Save(mw.filepath);
        }

        private void ResetBass()
        {
            List<PianoControl> ticks_for_removal = new List<PianoControl>();

            foreach (PianoControl p in mw.Timeline.Children.OfType<PianoControl>())
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
                mw.Timeline.Children.Remove(p);
        }

        /*
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

            if (player.NumberOfTicksPerBar() >= 13)
                GetTick(13).SetPianoKey(fifth, PianoKey.KeyState.Bass);
        }

        private void EighthNoteAlternating()
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
            GetTick(3).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            GetTick(5).SetPianoKey(first, PianoKey.KeyState.Bass);
            GetTick(7).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            GetTick(9).SetPianoKey(first, PianoKey.KeyState.Bass);

            GetTick(11).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            if (player.NumberOfTicksPerBar() >= 13)
            {
                GetTick(13).SetPianoKey(first, PianoKey.KeyState.Bass);
                GetTick(15).SetPianoKey(fifth, PianoKey.KeyState.Bass);
            }
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

        private void CantHelpFallingInLove()
        {
            List<int> pattern = InvertPattern(new List<int> { 1, 5, 12, 8, 10, 12 });

            Activate(pattern);
        }

        private void Random()
        {
            ResetBass();


            List<int> valid_notes = new List<int> { 1, 2, 3, 5, 7, 8, 9, 10, 12 };

            List<int> pattern = new List<int>();

            //Add root note based on inversion
            pattern.Add(1 + 2 * Inversion.SelectedIndex);

            Random r = new Random();

            for (int i = 0; i < Activations.SelectedIndex; i++)
            {
                int next_note = r.Next(0, valid_notes.Count - 1);

                pattern.Add(valid_notes[next_note]);
            }

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
        }*/

        private List<int> FindMelodyNotes()
        {
            List<int> melody_notes = new List<int>();

            foreach (PianoControl p in mw.Timeline.Children.OfType<PianoControl>())
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

            for (int i = 0; i < mw.player.time_signature_top; i++)
            {
                if (!mw.HasTick(i * 4 + 1) && activations > 0)
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

            for (int i = 0; i < mw.player.time_signature_top; i++)
            {
                if (!mw.HasTick(i * 4 + 3) && activations > 0)
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
            int base_note = mw.key_signature_offset + 12 * (int)OctaveStart.SelectedIndex + 12;

            int[] major_scale = { 2, 2, 1, 2, 2, 2, 1 };

            int final_note = base_note;
            int scale_position = 0;

            for (int i = 1; i < scale_number + ChordName.SelectedIndex - 1; i++)
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
