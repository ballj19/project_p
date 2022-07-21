using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M;
using System.IO;
using System.Windows;
using System.Xml;

namespace project_p
{
    public partial class MainWindow
    {
        private void ExportMidi_Click(object sender, RoutedEventArgs ev)
        {
            Save();

            M.MidiFile mf;

            using (Stream stm = File.OpenRead(@"C:\Users\jakeb\Downloads\Landslide.mid"))
                mf = M.MidiFile.ReadFrom(stm);


            var result = mf.Clone();

            //Begin to modify
            result = RemoveEvents(result);
            result = AddEvents(result);
            result = result.AdjustTempo((double)player.bpm);


            using (var stm = File.OpenWrite(@"C:\Users\jakeb\Downloads\song.mid"))
            {
                stm.SetLength(0);
                result.WriteTo(stm);
            }

            System.Diagnostics.Process.Start(@"C:\Users\jakeb\Downloads\song.mid");
        }

        private M.MidiFile RemoveEvents(M.MidiFile result)
        {
            //Track 0
            List<M.MidiEvent> new_events = new List<M.MidiEvent>();
            foreach (M.MidiEvent e in result.Tracks[0].Events)
            {
                if (e.Message.Status != 144)
                    new_events.Add(e);
            }

            result.Tracks[0].Events.Clear();

            foreach (M.MidiEvent e in new_events)
            {
                result.Tracks[0].Events.Add(e);
            }

            //Track 1
            new_events.Clear();

            foreach (M.MidiEvent e in result.Tracks[1].Events)
            {
                if (e.Message.Status != 144)
                    new_events.Add(e);
            }

            result.Tracks[1].Events.Clear();

            foreach (M.MidiEvent e in new_events)
            {
                result.Tracks[1].Events.Add(e);
            }


            return result;
        }

        private M.MidiFile AddEvents(M.MidiFile result)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNodeList bars = doc.SelectSingleNode("//Bars").ChildNodes;

            result = AddEventsInTrack(result, 0, bars);
            result = AddEventsInTrack(result, 1, bars);

            return result;
        }

        private M.MidiFile AddEventsInTrack(M.MidiFile result, int track, XmlNodeList bars)
        {
            List<byte> waiting_to_turn_off = new List<byte>();
            int prev_note_tick = 1;
            IList<M.MidiEvent> track_events = result.Tracks[track].Events;
            int ticks_per_bar = 4 * player.time_signature_top;

            foreach (XmlNode b in bars)
            {
                int bar_number = int.Parse(b.Name.Substring(3));

                if (bar_number < int.Parse(ExportStart.Text) || bar_number > int.Parse(ExportEnd.Text))
                {
                    prev_note_tick += ticks_per_bar;
                    continue;
                }

                for (int tick = 1; tick <= ticks_per_bar; tick++)
                {
                    XmlNode tick_node = b.SelectSingleNode("Ticks/Tick" + tick);

                    if (tick_node != null)
                    {
                        if (TrackExistsInTick(track, tick_node))
                        {
                            int absolute_tick = ticks_per_bar * (bar_number - 1) + tick;
                            int delay = (int)((absolute_tick - prev_note_tick) / (double)ticks_per_bar * player.time_signature_top * result.TimeBase); //How long to wait until next event

                            List<M.MidiEvent> turn_off_events = TurnOffNotesInTrack(track, waiting_to_turn_off, delay);
                            waiting_to_turn_off.Clear();

                            if (turn_off_events.Count > 0)
                                delay = 0;

                            foreach(M.MidiEvent ev in turn_off_events)
                                track_events.Insert(track_events.Count - 1, ev); //Insert right before end of file

                            foreach (XmlNode note in tick_node)
                            {
                                int t = note.SelectSingleNode("Type").InnerText == "Melody" ? 0 : 1;

                                if(t == track)
                                {
                                    byte midi = Byte.Parse(note.SelectSingleNode("Number").InnerText);
                                    byte velocity = note.SelectSingleNode("Type").InnerText == "Melody" ? (byte)115 : (byte)65;


                                    M.MidiMessage message = new M.MidiMessageNoteOn(midi, velocity, 0);
                                    M.MidiEvent ev = new M.MidiEvent((int)delay, message);

                                    track_events.Insert(track_events.Count - 1, ev); //Insert right before end of file

                                    waiting_to_turn_off.Add(midi);

                                    prev_note_tick = absolute_tick;

                                    delay = 0;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private bool TrackExistsInTick(int track, XmlNode tick_node)
        {
            foreach (XmlNode note in tick_node)
            {
                int t = note.SelectSingleNode("Type").InnerText == "Melody" ? 0 : 1;

                if (t == track)
                    return true;
            }

            return false;
        }

        private List<M.MidiEvent> TurnOffNotesInTrack(int track, List<byte> notes, int delay)
        {
            List<M.MidiEvent> events = new List<M.MidiEvent>();

            for (int i = 0; i < notes.Count; i++)
            {
                byte turn_off_value = notes[i];
                M.MidiMessage turn_off_message = new M.MidiMessageNoteOn(turn_off_value, 0, 0);
                M.MidiEvent turn_off_event = new M.MidiEvent(delay, turn_off_message);
                events.Add(turn_off_event);

                delay = 0;
            }

            return events;
        }
    }
}
