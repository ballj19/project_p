using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Xml;

namespace project_p
{
    public partial class MainWindow
    {
        private void ImportFileButton_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    MidiFile midi_file = new MidiFile(filePath);

                    ExtractMelody(midi_file);
                }
            }
        }

        private void ExtractMelody(MidiFile midi_file)
        {
            MidiTrack melody = midi_file.Tracks[0];

            int length_of_bar = midi_file.TicksPerQuarterNote * 4;

            int number_of_bars = melody.MidiEvents.Last().Time / length_of_bar + 1;

            int[,] notes =  new int[number_of_bars, 16];

            foreach (MidiEvent ev in melody.MidiEvents)
            {
                if (ev.MidiEventType == MidiEventType.NoteOn)
                {
                    int bar = ev.Time / length_of_bar;
                    int tick = (16  * (ev.Time % length_of_bar) / length_of_bar);
                    int note = ev.Note;

                    if(note > notes[bar,tick])
                        notes[bar, tick] = note;
                }
            }

            CreateBaseFile(notes);
        }

        private void CreateBaseFile(int[,] notes)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement song = doc.CreateElement("", "Song", "");
            XmlElement bars = doc.CreateElement("", "Bars", "");

            for (int i = 0; i < notes.GetLength(0); i++)
            {
                XmlElement bar = doc.CreateElement("", "Bar" + (i + 1), "");
                XmlElement ticks = doc.CreateElement("", "Ticks", "");
                bar.AppendChild(ticks);

                for(int j = 0; j < notes.GetLength(1); j++)
                {
                    if(notes[i,j] != 0)
                    {
                        XmlElement tick = doc.CreateElement("", "Tick" + (j + 1), "");

                        XmlElement note_element = doc.CreateElement("", "Note", "");                       

                        XmlElement number = doc.CreateElement("", "Number", "");
                        XmlText number_text = doc.CreateTextNode(notes[i, j].ToString());
                        number.AppendChild(number_text);
                        note_element.AppendChild(number);

                        XmlElement type = doc.CreateElement("", "Type", "");
                        XmlText type_text = doc.CreateTextNode("Melody");
                        type.AppendChild(type_text);
                        note_element.AppendChild(type);

                        tick.AppendChild(note_element);

                        ticks.AppendChild(tick);
                    }
                }

                bars.AppendChild(bar);
            }

            song.AppendChild(bars);
            doc.AppendChild(song);

            doc.Save("new_base_file.xml");
        }
    }
}
