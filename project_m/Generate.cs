using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;

namespace project_m
{
    public partial class MainWindow : Window
    {
        XmlDocument doc;
        int bpm = 100;
        char key_signature = 'C'; //C = 0
        int time_signature_top = 4;
        int time_signature_bottom = 4;
        int tick_divisions = 2;

        List<Beat> beat;
        List<int> progression;

        string filepath = @"C:\Users\jakeb\Documents\test.musicxml";

        public struct Note
        {
            public char letter;
            public int duration;
            public int octave;
            public int stave;
        }

        public struct Beat
        {
            public int interval;
            public int duration;
        }
        
        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            Generate_Beat();
            Generate_Chord_Progression();

            Generate_XML();
        }

        private Note Note_Plus_Interval(Note note,  int interval)
        {
            char new_letter = note.letter;

            for (int i = 0; i < interval - 1; i++)
            {
                new_letter++;

                if (new_letter == 'C')
                    note.octave++;

                if (new_letter > 'G')
                    new_letter = (char)(new_letter - 7);
            }

            note.letter = new_letter;

            return note;
        }

        private char GetLetterFromScaleNumber(int scale_number)
        {
            char letter = key_signature;

            for (int i = 0; i < scale_number - 1; i++)
            {
                letter++;

                if (letter > 'G')
                    letter = (char)(letter - 7);
            }

            return letter;
        }

        private void Generate_Beat()
        {
            beat = new List<Beat>();

            beat.Add(new Beat { interval = 1, duration = 2 });
            beat.Add(new Beat { interval = 5, duration = 2 });
            beat.Add(new Beat { interval = 8, duration = 2 });
        }

        private List<Note>  Generate_Bass(int progression_index)
        {
            List<Note> notes = new List<Note>();

            Note base_note = new Note { letter = GetLetterFromScaleNumber(progression_index), duration = 2, octave = 3, stave = 2 };
            
            foreach(Beat b in beat)
            {
                Note n = Note_Plus_Interval(base_note, b.interval);
                n.duration = b.duration;
                notes.Add(n);
            }

            return notes;
        }

        private List<Note> Generate_Melody()
        {
            List<Note> notes = new List<Note>();

            return notes;
        }

        private void Generate_Chord_Progression()
        {
            progression = new List<int>{ 1, 5, 6, 4};
        }

        private XmlElement Generate_Measure_New_Attributes()
        {
            XmlElement attributes = doc.CreateElement("", "attributes", "");

            XmlElement divisions = doc.CreateElement("", "divisions", "");
            XmlText divisions_text = doc.CreateTextNode(tick_divisions.ToString());
            divisions.AppendChild(divisions_text);
            attributes.AppendChild(divisions);

            XmlElement key = doc.CreateElement("", "key", "");
            XmlElement fifths = doc.CreateElement("", "fifths", "");
            XmlText fifths_text = doc.CreateTextNode(key_signature.ToString());
            fifths.AppendChild(fifths_text);
            key.AppendChild(fifths);
            attributes.AppendChild(key);

            XmlElement time = doc.CreateElement("", "time", "");
            XmlElement beats = doc.CreateElement("", "beats", "");
            XmlText beats_text = doc.CreateTextNode(time_signature_top.ToString());
            beats.AppendChild(beats_text);
            XmlElement beattype = doc.CreateElement("", "beat-type", "");
            XmlText beatstype_text = doc.CreateTextNode(time_signature_bottom.ToString());
            beattype.AppendChild(beatstype_text);
            time.AppendChild(beats);
            time.AppendChild(beattype);
            attributes.AppendChild(time);

            XmlElement staves = doc.CreateElement("", "staves", "");
            XmlText staves_text = doc.CreateTextNode("2");
            staves.AppendChild(staves_text);
            attributes.AppendChild(staves);

            XmlElement clef1 = doc.CreateElement("", "clef", "");
            clef1.SetAttribute("number", "1");
            XmlElement sign1 = doc.CreateElement("", "sign", "");
            XmlText sign1_text = doc.CreateTextNode("G");
            sign1.AppendChild(sign1_text);
            XmlElement line1 = doc.CreateElement("", "line", "");
            XmlText line1_text = doc.CreateTextNode("2");
            line1.AppendChild(line1_text);
            clef1.AppendChild(sign1);
            clef1.AppendChild(line1);
            attributes.AppendChild(clef1);

            XmlElement clef2 = doc.CreateElement("", "clef", "");
            clef2.SetAttribute("number", "2");
            XmlElement sign2 = doc.CreateElement("", "sign", "");
            XmlText sign2_text = doc.CreateTextNode("F");
            sign2.AppendChild(sign2_text);
            XmlElement line2 = doc.CreateElement("", "line", "");
            XmlText line2_text = doc.CreateTextNode("4");
            line2.AppendChild(line2_text);
            clef2.AppendChild(sign2);
            clef2.AppendChild(line2);
            attributes.AppendChild(clef2);

            return attributes;
        }

        private XmlText Get_Type_Text(Note n)
        {
            int duration = n.duration;
            string duration_text;

            if(duration < tick_divisions)
                duration_text = "eigth";
            else if(duration < tick_divisions * 2)
                duration_text = "quarter";
            else if(duration < tick_divisions * 4)
                duration_text = "half";
            else
                duration_text = "whole";

            return doc.CreateTextNode(duration_text);
        }

        private XmlElement Note_To_XML(Note n)
        {
            XmlElement note = doc.CreateElement("", "note", "");
            XmlElement pitch = doc.CreateElement("", "pitch", "");
            XmlElement step = doc.CreateElement("", "step", "");
            XmlElement octave = doc.CreateElement("", "octave", "");
            XmlElement duration = doc.CreateElement("", "duration", "");
            XmlElement voice = doc.CreateElement("", "voice", "");
            XmlElement type = doc.CreateElement("", "type", "");
            XmlElement stem = doc.CreateElement("", "stem", "");
            XmlElement staff = doc.CreateElement("", "staff", "");

            XmlText step_text = doc.CreateTextNode(n.letter.ToString());
            step.AppendChild(step_text);
            pitch.AppendChild(step);

            XmlText octave_text = doc.CreateTextNode(n.octave.ToString());
            octave.AppendChild(octave_text);
            pitch.AppendChild(octave);
            note.AppendChild(pitch);

            XmlText duration_text = doc.CreateTextNode(n.duration.ToString());
            duration.AppendChild(duration_text);
            note.AppendChild(duration);

            XmlText voice_text = doc.CreateTextNode("1");
            voice.AppendChild(voice_text);
            note.AppendChild(voice);

            XmlText type_text = Get_Type_Text(n);
            type.AppendChild(type_text);
            note.AppendChild(type);
                        
            XmlText stem_text = doc.CreateTextNode("up");
            stem.AppendChild(stem_text);
            note.AppendChild(stem);

            XmlText staff_text = doc.CreateTextNode(n.stave.ToString());
            staff.AppendChild(staff_text);
            note.AppendChild(staff);

            return note;
        }

        /*
         * <note default-x="83.49" default-y="-15.00">
        <pitch>
          <step>C</step>
          <octave>5</octave>
          </pitch>
        <duration>1</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
        <staff>1</staff>
        </note>*/

        private XmlElement Generate_Measure_XML(int number)
        {
            XmlElement measure = doc.CreateElement("", "measure", "");
            measure.SetAttribute("number", number.ToString());            

            if(number == 1)
                measure.AppendChild(Generate_Measure_New_Attributes());
                       
            foreach(Note n in Generate_Bass(progression[number - 1]))
            {
                measure.AppendChild(Note_To_XML(n));
            }

            return measure;

        }

        private void Generate_XML()
        {
            doc = new XmlDocument();

            //XML Headers
            XmlElement score = doc.CreateElement("", "score-partwise", "");
            score.SetAttribute("version", "4.0");
                        
            XmlElement partlist = doc.CreateElement("", "part-list", "");
            XmlElement scorepart = doc.CreateElement("", "score-part", "");
            scorepart.SetAttribute("id", "P1");
            XmlElement partname = doc.CreateElement("", "part-name", "");
            XmlText partname_text = doc.CreateTextNode("Jake Practice Music");
            partname.AppendChild(partname_text);
            scorepart.AppendChild(partname);
            partlist.AppendChild(scorepart);
            score.AppendChild(partlist);

            XmlElement part = doc.CreateElement("", "part", "");
            part.SetAttribute("id", "P1");

            //XML Measures
            for (int i = 1; i <= progression.Count; i++)
            {

                part.AppendChild(Generate_Measure_XML(i));

            }

            score.AppendChild(part);

            //Save File
            doc.AppendChild(score);
            doc.Save(filepath);
        }
    }

    /*
     <score-partwise version="4.0">
      <part-list>
        <score-part id="P1">
          <part-name>Music</part-name>
        </score-part>
      </part-list>
      <part id="P1">
        <measure number="1">
          <attributes>
            <divisions>1</divisions>
            <key>
              <fifths>0</fifths>
            </key>
            <time>
              <beats>4</beats>
              <beat-type>4</beat-type>
            </time>
            <clef>
              <sign>G</sign>
              <line>2</line>
            </clef>
          </attributes>
          <note>
            <pitch>
              <step>C</step>
              <octave>4</octave>
            </pitch>
            <duration>4</duration>
            <type>whole</type>
          </note>
        </measure>
      </part>
    </score-partwise>
     */
    }
