using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using project_p;
using System.Net;
using System.Threading;

namespace project_e
{
    internal class SongImporter
    {
        string song_list = @"C:\Users\ballj\source\repos\project_p\project_e\songs.xml";
        string song_name;
        public SongImporter()
        {
            //if (!File.Exists(song_list))
            CreateSaveFile();
        }

        public void Import(string filepath)
        {
            MidiFile midi_file = new MidiFile(filepath);
            song_name = new FileInfo(filepath).Name;
            AddToSongList(midi_file);
        }

        private int[,] ExtractMelody(MidiFile midi_file)
        {
            MidiTrack melody = midi_file.Tracks[0];

            int length_of_bar = midi_file.TicksPerQuarterNote * 4;

            int number_of_bars = Math.Max(melody.MidiEvents.Last().Time, midi_file.Tracks[1].MidiEvents.Last().Time) / length_of_bar + 1;

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

        private string[] ExtractBass(MidiFile midi_file)
        {

            MidiTrack bass = midi_file.Tracks[1];

            int length_of_bar = midi_file.TicksPerQuarterNote * 4;

            int number_of_bars = Math.Max(bass.MidiEvents.Last().Time, midi_file.Tracks[0].MidiEvents.Last().Time) / length_of_bar + 1;

            int[,] notes = new int[number_of_bars, 16];


            foreach (MidiEvent ev in bass.MidiEvents)
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


            string[] chords = new string[number_of_bars];

            for(int bar = 0; bar < number_of_bars; bar++)
            {
                List<int> notes_in_chord = new List<int>();
                string chord = "";

                for (int tick = 0; tick < 16; tick++)
                {
                    if (notes[bar, tick] != 0)
                    {
                        if (!notes_in_chord.Contains(notes[bar, tick] % 12))
                            notes_in_chord.Add(notes[bar, tick] % 12);
                    }
                }

                foreach(int note in notes_in_chord)
                    chord += MidiToString(note) + "\t";

                if(chord.Length > 0)
                    chord = chord.Remove(chord.Length - 1);

                chords[bar] = chord;
            }



            return chords;
        }

        private string MidiToString(int midi)
        {
            Dictionary<int, string> notes = new Dictionary<int, string>
            {
                {0, "C"},
                {1, "C#" },
                {2, "D" },
                {3, "D#" },
                {4, "E"},
                {5, "F" },
                {6, "F#" },
                {7, "G"},
                {8, "G#" },
                {9, "A" },
                {10, "A#"},
                {11, "B" },
            };

            return notes[midi % 12];
        }

        private string IdentifyChord(string query)
        {
            string url = "https://www.scales-chords.com/findnotes_en.php?" + query.Replace("#", "%23");


            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }


        private void CreateSaveFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement song = doc.CreateElement("", "Songs", "");

            doc.AppendChild(song);
            doc.Save(song_list);
        }

        private void AddToSongList(MidiFile midi_file)
        {
            int[,] melody = ExtractMelody(midi_file);
            string[] bass = ExtractBass(midi_file);

            XmlDocument doc = new XmlDocument();
            doc.Load(song_list);

            XmlNode songs = doc.DocumentElement;

            XmlNode song = doc.CreateElement("", "Song", "");

            XmlElement song_name_node = doc.CreateElement("", "SongName", "");
            XmlText song_name_text = doc.CreateTextNode(song_name);
            song_name_node.AppendChild(song_name_text);
            song.AppendChild(song_name_node);
            
            XmlNode bars = doc.CreateElement("", "Bars", "");

            for (int i = 0; i < melody.GetLength(0); i++)
            {
                XmlElement bar = doc.CreateElement("", "Bar" + (i + 1), "");

                for (int j = 0; j < melody.GetLength(1); j++)
                {
                    if (melody[i, j] != 0)
                    {
                        XmlElement tick = doc.CreateElement("", "Tick" + (j + 1), "");

                        XmlElement note_element = doc.CreateElement("", "Note", "");

                        XmlElement number = doc.CreateElement("", "Number", "");
                        XmlText number_text = doc.CreateTextNode(melody[i, j].ToString());
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

                XmlElement bass_node = doc.CreateElement("", "Bass", "");
                XmlText bass_text = doc.CreateTextNode(bass[i]);
                bass_node.AppendChild(bass_text);
                bar.AppendChild(bass_node);

                bars.AppendChild(bar);
            }

            song.AppendChild(bars);
            songs.AppendChild(song);

            doc.Save(song_list);
        }
    }
}
