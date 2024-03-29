﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Threading;

namespace project_p
{
    class Bar
    {
        MidiPlayer player;
        XmlNode xml;
        
        public Bar(XmlNode b, MidiPlayer p)
        {
            player = p;
            xml = b;

        }

        public void Play()
        {

            for (int tick = 1; tick <= player.NumberOfTicksPerBar(); tick++)
            {
                XmlNode tick_node = xml.SelectSingleNode("Tick" + tick);

                if (tick_node != null)
                {
                    foreach (XmlNode note in tick_node)
                    {

                        byte midi = Byte.Parse(note.SelectSingleNode("Number").InnerText);
                        int velocity = note.SelectSingleNode("Type").InnerText == "Melody" ? 115 : 65;

                        player.Play(midi, (byte)velocity);
                    }
                }

                double sleep = (double)player.time_signature_top / (double)player.bpm * 60.0 * 1000.0 / player.NumberOfTicksPerBar();
                Thread.Sleep((int)sleep);
            }
        }
    }
}
