using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using M;
using System.IO;
using System.Diagnostics;

namespace project_e
{
    class Generator
    {
        public int middle_c = 60;
        public int number_of_ticks_per_bar = 8;
        public int time_signature_top = 4;
        public int time_signature_bottom = 4;
        public int max_notes;
        public int number_of_bars;
        public IList intervals;
        public int tempo;
        public Random r = new Random();

        List<Bar> bars;
        MainWindow mw;


        private int handle = 0;

        protected delegate void MidiCallback(int handle, int msg, int instance, int param1, int param2);

        [DllImport("winmm.dll")]
        private static extern int midiOutOpen(ref int handle, int deviceID, MidiCallback proc, int instance, int flags);

        [DllImport("winmm.dll")]
        protected static extern int midiOutShortMsg(int handle, int message);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Generator(MainWindow mw)
        {
            this.mw = mw;
            int result = midiOutOpen(ref handle, 0, null, 0, 0);
        }

        public void SetParams(int b, IList i, int t, int n)
        {
            max_notes = n;
            number_of_bars = b;
            intervals = i;
            tempo = t;
        }

        private void Play(byte note, byte velocity = 0x7F)
        {
            int message = (velocity << 16) + (note << 8) + 0x90;
            int r = midiOutShortMsg(handle, message);
        }

        public void PlayBars()
        {
            mw.BassNotes.Items.Clear();

            foreach (Bar bar in bars)
            {
                string list_text = (bar.first_melody + 1).ToString() + "  ";

                foreach (int tick in bar.notes)
                {
                    if (tick > 0)
                        list_text += "O";
                    else
                        list_text += "-";
                }

                mw.BassNotes.Items.Add(list_text);
            }

            MidiFile mf;

            using (Stream stm = File.OpenRead(@"C:\Users\ballj\Downloads\Landslide.mid"))
                mf = M.MidiFile.ReadFrom(stm);


            var result = mf.Clone();

            //Begin to modify
            result = RemoveEvents(result);
            result = AddEvents(result);
            result = result.AdjustTempo((double)tempo);


            using (var stm = File.OpenWrite(@"C:\Users\ballj\Downloads\song.mid"))
            {
                stm.SetLength(0);
                result.WriteTo(stm);
            }

            System.Diagnostics.Process.Start( @"C:\Users\ballj\Downloads\song.mid");

            SetForegroundWindow(WinGetHandle("Pianoteq"));
            System.Windows.Forms.SendKeys.SendWait(" ");
            Thread.Sleep(1000);
            SetForegroundWindow(WinGetHandle("MainWindow"));
        }

        public static IntPtr WinGetHandle(string wName)
        {
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains(wName))
                    return pList.MainWindowHandle;

            return IntPtr.Zero;
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
            List<byte> waiting_to_turn_off = new List<byte>();
            int prev_note_tick = 1;
            IList<M.MidiEvent> track_events = result.Tracks[0].Events;

            for(int bar_number = 0; bar_number < bars.Count; bar_number++)
            {
                for (int tick = 0; tick < number_of_ticks_per_bar; tick++)
                {
                    int absolute_tick = number_of_ticks_per_bar * bar_number + tick;
                    int delay = (int)((absolute_tick - prev_note_tick) / (double)number_of_ticks_per_bar * time_signature_top * result.TimeBase); //How long to wait until next event

                    List<M.MidiEvent> turn_off_events = TurnOffNotesInTrack(waiting_to_turn_off, delay);
                    waiting_to_turn_off.Clear();

                    if (turn_off_events.Count > 0)
                        delay = 0;

                    foreach (M.MidiEvent e in turn_off_events)
                        track_events.Insert(track_events.Count - 1, e); //Insert right before end of file

                    int midi = bars[bar_number].notes[tick];
                    byte velocity = 100;


                    M.MidiMessage message = new M.MidiMessageNoteOn((byte)midi, velocity, 0);
                    M.MidiEvent ev = new M.MidiEvent(delay, message);

                    track_events.Insert(track_events.Count - 1, ev); //Insert right before end of file

                    waiting_to_turn_off.Add((byte)midi);

                    prev_note_tick = absolute_tick;

                    delay = 0;
                }
            }

            return result;
        }

        private List<M.MidiEvent> TurnOffNotesInTrack(List<byte> notes, int delay)
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

        /*
        public async void PlayBars()
        {
            double sleep = (double)time_signature_top / (double)tempo * 60.0 * 1000.0 / (double)number_of_ticks_per_bar;

            mw.BassNotes.Items.Clear();

            foreach (Bar bar in bars)
            {
                string list_text = (bar.bass + 1).ToString() + "  ";

                foreach (int tick in bar.notes)
                {
                    if (tick > 0)
                        list_text += "O";
                    else
                        list_text += "-";
                }

                mw.BassNotes.Items.Add(list_text);
            }
            
            await Task.Run(() =>
            {
                for (int i = 0; i < number_of_ticks_per_bar / 2; i++)
                {
                        Play((byte)(middle_c - 12), 60);
                    Thread.Sleep((int)(sleep * 2.0));
                }
            });

            foreach (Bar bar in bars)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < number_of_ticks_per_bar; i++)
                    {
                        if (bar.notes[i] != 0)
                            Play((byte)bar.notes[i], 100);

                        Thread.Sleep((int)sleep);
                    }


                });

            }
        }*/

        public void NextBar()
        {
            bars = new List<Bar>();


            for(int i = 0; i < number_of_bars; i++)
            {
                bars.Add(new Bar(this));
            }

            bars.Add(new Bar(this, Bar.Type.Empty));

            PlayBars();
        }

    }
}
