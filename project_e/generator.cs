using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

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
        public int biggest_hop;
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

        public Generator(MainWindow mw)
        {
            this.mw = mw;
            int result = midiOutOpen(ref handle, 0, null, 0, 0);
        }

        public void SetParams(int b, int h, int t, int n)
        {
            max_notes = n;
            number_of_bars = b;
            biggest_hop = h;
            tempo = t;
        }

        private void Play(byte note, byte velocity = 0x7F)
        {
            int message = (velocity << 16) + (note << 8) + 0x90;
            int r = midiOutShortMsg(handle, message);
        }

        public async void PlayBar()
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
        }

        public void NextBar()
        {
            bars = new List<Bar>();

            for(int i = 0; i < number_of_bars; i++)
            {
                bars.Add(new Bar(this));
            }

            PlayBar();
        }

    }
}
