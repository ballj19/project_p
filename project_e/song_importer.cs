using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using project_p;

namespace project_e
{
    internal class SongImporter
    {
        public SongImporter(string file)
        {

        }


        private int[,] ExtractMelody(MidiFile midi_file)
        {
            MidiTrack melody = midi_file.Tracks[0];

            int length_of_bar = midi_file.TicksPerQuarterNote * 4;

            int number_of_bars = melody.MidiEvents.Last().Time / length_of_bar;

            int[,] notes = new int[number_of_bars, 16];

            foreach (MidiEvent ev in melody.MidiEvents)
            {
                if (ev.MidiEventType == MidiEventType.NoteOn)
                {
                    int bar = ev.Time / length_of_bar;
                    int tick = (16 * (ev.Time % length_of_bar) / length_of_bar);
                    int note = ev.Note;

                    if (note > notes[bar, tick])
                        notes[bar, tick] = note;
                }
            }

            return notes;
        }

        private int[,] ExtractBass(MidiFile midi_file)
        {

            MidiTrack melody = midi_file.Tracks[1];

            int length_of_bar = midi_file.TicksPerQuarterNote * 4;

            int number_of_bars = melody.MidiEvents.Last().Time / length_of_bar;

            int[,] notes = new int[number_of_bars, 16];

            foreach (MidiEvent ev in melody.MidiEvents)
            {
                if (ev.MidiEventType == MidiEventType.NoteOn)
                {
                    int bar = ev.Time / length_of_bar;
                    int tick = (16 * (ev.Time % length_of_bar) / length_of_bar);
                    int note = ev.Note;

                    if (note > notes[bar, tick])
                        notes[bar, tick] = note;
                }
            }

            return notes;
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

                for (int j = 0; j < notes.GetLength(1); j++)
                {
                    if (notes[i, j] != 0)
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

                        bar.AppendChild(tick);
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
