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
        int bpm = 80;
        int time_signature_top = 4;
        int time_signature_bottom = 4;
        Random r = new Random();

        List<int> notes_in_key = new List<int>();
        int note_location = 0;
        int starting_note = 60;

        private int handle = 0;

        protected delegate void MidiCallback(int handle, int msg, int instance, int param1, int param2);

        [DllImport("winmm.dll")]
        private static extern int midiOutOpen(ref int handle, int deviceID, MidiCallback proc, int instance, int flags);

        [DllImport("winmm.dll")]
        protected static extern int midiOutShortMsg(int handle, int message);

        public Generator()
        {
            int result = midiOutOpen(ref handle, 0, null, 0, 0);

            GetNotesInKey();

            int test = 0;
        }

        private void Play(byte note, byte velocity = 0x7F)
        {
            int message = (velocity << 16) + (note << 8) + 0x90;
            int r = midiOutShortMsg(handle, message);
        }

        private void GetNotesInKey()
        {
            int[] major_scale = { 2, 2, 1, 2, 2, 2, 1 };

            int lower = note_location;
            int higher = note_location;

            int scale_position = 0;

            int midi = starting_note;

            notes_in_key.Add(starting_note);

            while(midi < 108)
            {
                midi += major_scale[scale_position];
                notes_in_key.Add(midi);

                scale_position++;

                if (scale_position == major_scale.Length)
                    scale_position = 0;
            }

            scale_position = major_scale.Length - 1;
            midi = starting_note;

            while(midi > 21)
            {
                midi -= major_scale[scale_position];
                notes_in_key.Insert(0, midi);

                scale_position--;

                if (scale_position < 0)
                    scale_position = major_scale.Length - 1;
            }
        }

        public void Generate(int bars, int max_notes, int max_note_distance)
        {
            note_location = notes_in_key.IndexOf(starting_note);

            int number_of_notes = r.Next(2, max_notes + 1);
            double number_of_ticks_per_bar = 4.0;

            for(int i = 0; i < number_of_notes; i++)
            {
                int midi = notes_in_key[note_location];

                Play((byte)midi, 100);

                double sleep = (double)time_signature_top / (double)bpm * 60.0 * 1000.0 / number_of_ticks_per_bar;

                Thread.Sleep((int)sleep);

                note_location += r.Next(-1 * max_note_distance, max_note_distance + 1);
            }


        }
    }
}
