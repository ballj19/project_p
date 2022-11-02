using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;

namespace project_e
{
    internal class Bar
    {
        public enum Type
        {
            Lead,
            Standard,
            Empty
        }

        Generator generator;

        public int first_melody = 0;
        List<int> notes_in_key = new List<int>();
        int note_location = 0;
        int starting_note = 60;
        Type type;

        public List<int> notes = new List<int>();

        public Bar(Generator g, Type t = Type.Standard)
        {
            generator = g;
            type = t;

            GetNotesInKey();

            if (t == Type.Standard)
                GetStandardBar();
            else if (t == Type.Empty)
                GetEmptyBar();
        }

        private void GetStandardBar()
        {
            note_location = notes_in_key.IndexOf(starting_note);


            List<int> ticks = GetTicks(generator.number_of_ticks_per_bar);

            first_melody = generator.r.Next(0, 7);

            note_location += first_melody;

            for (int i = 0; i < generator.number_of_ticks_per_bar; i++)
            {
                int midi = notes_in_key[note_location];

                if (ticks.Contains(i))
                {
                    notes.Add(midi);

                    bool next_note_found = false;

                    while(!next_note_found)
                    {
                        int hop_index = generator.r.Next(0, generator.intervals.Count);
                        ListBoxItem item = generator.intervals[hop_index] as ListBoxItem;

                        int reverse = generator.r.Next(0, 2);

                        int next_note;

                        if (reverse > 0)
                            next_note = midi + (int)item.Tag;
                        else
                            next_note = midi - (int)item.Tag;


                        if (notes_in_key.Contains(next_note))
                        {
                            next_note_found = true;
                            note_location = notes_in_key.IndexOf(next_note);
                        }
                    }
                }
                else
                {
                    notes.Add(0);
                }
            }

        }

        private void GetEmptyBar()
        {
            for (int i = 0; i < generator.number_of_ticks_per_bar; i++)
            {
                notes.Add(0);
            }
        }

        public List<int> GetTicks(int number_of_ticks_per_bar)
        {
            int number_of_notes;

            if (generator.exact_number_of_notes)
                number_of_notes = generator.max_notes;
            else
               number_of_notes = generator.r.Next(2, generator.max_notes + 1);

            List<int> ticks = new List<int>();

            while (ticks.Count < number_of_notes)
            {
                int tick = generator.r.Next(0, number_of_ticks_per_bar);

                if (!ticks.Contains(tick))
                    ticks.Add(tick);
            }

            return ticks;
        }



        private void GetNotesInKey()
        {
            int[] major_scale = { 2, 2, 1, 2, 2, 2, 1 };

            int lower = note_location;
            int higher = note_location;

            int scale_position = 0;

            int midi = starting_note;

            notes_in_key.Add(starting_note);

            while (midi < 108)
            {
                midi += major_scale[scale_position];
                notes_in_key.Add(midi);

                scale_position++;

                if (scale_position == major_scale.Length)
                    scale_position = 0;
            }

            scale_position = major_scale.Length - 1;
            midi = starting_note;

            while (midi > 21)
            {
                midi -= major_scale[scale_position];
                notes_in_key.Insert(0, midi);

                scale_position--;

                if (scale_position < 0)
                    scale_position = major_scale.Length - 1;
            }
        }
    }
}
