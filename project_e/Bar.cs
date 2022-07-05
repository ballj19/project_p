using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace project_e
{
    internal class Bar
    {
        Generator generator;

        public int bass;
        List<int> notes_in_key = new List<int>();
        int note_location = 0;
        int starting_note = 60;

        public List<int> notes = new List<int>();

        public Bar(Generator g)
        {
            this.generator = g;

            GetNotesInKey();

            note_location = notes_in_key.IndexOf(starting_note);
            


            List<int> ticks = GetTicks(generator.number_of_ticks_per_bar);


            for (int i = 0; i < generator.number_of_ticks_per_bar; i++)
            {
                int midi = notes_in_key[note_location];

                if (ticks.Contains(i))
                {
                    notes.Add(midi);
                    note_location += generator.r.Next(-1 * generator.biggest_hop, generator.biggest_hop + 1);
                }
                else
                {
                    notes.Add(0);
                }
            }

            bass = generator.r.Next(0, 6); //Exclude diminished 7th
        }

        public List<int> GetTicks(int number_of_ticks_per_bar)
        {
            int number_of_notes = generator.r.Next(2, generator.max_notes + 1);

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
