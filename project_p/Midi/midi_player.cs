using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace project_p
{
    public class MidiPlayer
    {
        public enum Hand{
            Right,
            Left
        }

        private Dictionary<string, int> note_values = new Dictionary<string, int>
        {
            {"Cb", -1 },
            {"C", 0 },
            {"C#", 1 },
            {"Db", 1 },
            {"D", 2 },
            {"D#", 3 },
            {"Eb", 3 },
            {"E", 4 },
            {"E#", 5 },
            {"Fb", 4 },
            {"F", 5 },
            {"F#", 6 },
            {"Gb", 6 },
            {"G", 7 },
            {"G#", 8 },
            {"Ab", 8 },
            {"A", 9 },
            {"A#", 10 },
            {"Bb", 10 },
            {"B", 11 },
            {"B#", 12 },
        };

        private int handle = 0;
        MidiFile file;

        public int bpm = 100;
        public int time_signature_top = 4;
        public int time_signature_bottom = 4;

        int rightIterator = 0;
        List<List<byte>> rightNotes = new List<List<byte>>();

        int leftIterator = 0;
        List<List<byte>> leftNotes = new List<List<byte>>();

        protected delegate void MidiCallback(int handle, int msg, int instance, int param1, int param2);

        [DllImport("winmm.dll")]
        private static extern int midiOutOpen(ref int handle, int deviceID, MidiCallback proc, int instance, int flags);

        [DllImport("winmm.dll")]
        protected static extern int midiOutShortMsg(int handle, int message);

        public MidiPlayer()
        {
            int result = midiOutOpen(ref handle, 0, null, 0, 0);
        }

        public MidiPlayer(MidiFile file)
        {
            this.file = file;
            int result = midiOutOpen(ref handle, 0, null, 0, 0);
            ParseMidi();
        }

        private void ParseMidi()
        {
            ParseTrack(file.Tracks[0], Hand.Right);
            ParseTrack(file.Tracks[1], Hand.Left);
        }

        private void ParseTrack(MidiTrack track, Hand hand)
        {
            Dictionary<int, List<byte>> notes = new Dictionary<int, List<byte>>();

            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    if (!notes.Keys.Contains(midiEvent.Time))
                    {
                        notes.Add(midiEvent.Time, new List<byte>());
                    }

                    notes[midiEvent.Time].Add(Convert.ToByte(midiEvent.Note));
                }
            }

            foreach (List<byte> list in notes.Values)
            {
                if (hand == Hand.Right)
                    rightNotes.Add(list);
                else
                    leftNotes.Add(list);
            }
        }

        public void Play(byte note, byte velocity = 0x7F)
        {
            int message = (velocity << 16) + (note << 8) + 0x90;
            int r = midiOutShortMsg(handle, message);
        }

        public void Play(string note, byte velocity = 0x7F)
        {
            if (note == "")
                return;

            Play(NoteStringToInt(note), velocity);
        }

        private byte NoteStringToInt(string note)
        {
            int octave = Int32.Parse(note.Substring(note.Length - 1, 1));
            string base_note = note.Substring(0, note.Length - 1);

            return Convert.ToByte(12 + (12 * octave) + note_values[base_note]);
        }

        public void Stop(byte note)
        {
            int message = (0x0 << 16) + (note << 8) + 0x90;
            int r = midiOutShortMsg(handle, message);
        }

        public void Stop(string note)
        {
            if (note == "")
                return;

            Stop(NoteStringToInt(note));
        }

        public void PlayNext(Hand hand = Hand.Right)
        {
            if(hand == Hand.Right && rightIterator < rightNotes.Count)
            {
                foreach(byte note in rightNotes[rightIterator])
                {
                    Play(note);
                }
                rightIterator++;
            }
            else if(hand == Hand.Left && leftIterator < leftNotes.Count)
            {
                foreach (byte note in leftNotes[leftIterator])
                {
                    Play(note);
                }
                leftIterator++;
            }

        }

        public void Restart()
        {
            rightIterator = 0;
            leftIterator = 0;
        }

        public int NumberOfTicksPerBar()
        {
            return 16 / time_signature_bottom * time_signature_top;
        }


    }
}
