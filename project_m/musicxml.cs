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

        private XmlElement Generate_Measure_New_Attributes()
        {
            XmlElement attributes = doc.CreateElement("", "attributes", "");

            XmlElement divisions = doc.CreateElement("", "divisions", "");
            XmlText divisions_text = doc.CreateTextNode(time_signature.tick_divisions.ToString());
            divisions.AppendChild(divisions_text);
            attributes.AppendChild(divisions);

            XmlElement key = doc.CreateElement("", "key", "");
            XmlElement fifths = doc.CreateElement("", "fifths", "");
            XmlText fifths_text = doc.CreateTextNode(key_signature.fifths.ToString());
            fifths.AppendChild(fifths_text);
            key.AppendChild(fifths);
            attributes.AppendChild(key);

            XmlElement time = doc.CreateElement("", "time", "");
            XmlElement beats = doc.CreateElement("", "beats", "");
            XmlText beats_text = doc.CreateTextNode(time_signature.top.ToString());
            beats.AppendChild(beats_text);
            XmlElement beattype = doc.CreateElement("", "beat-type", "");
            XmlText beatstype_text = doc.CreateTextNode(time_signature.bottom.ToString());
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

        private XmlText Get_Type_Text(Note n, ref bool dot)
        {
            int duration = n.duration;
            string duration_text;

            if (duration < time_signature.tick_divisions)
                duration_text = "eigth";
            else if (duration < time_signature.tick_divisions * 2)
                duration_text = "quarter";
            else if (duration < time_signature.tick_divisions * 4)
                duration_text = "half";
            else
                duration_text = "whole";

            //Test to check if power of 2
            if((duration & (duration - 1)) != 0)
            {
                dot = true;
            }

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

            bool dot = false;
            XmlText type_text = Get_Type_Text(n, ref dot);
            type.AppendChild(type_text);
            note.AppendChild(type);

            if(dot)
            {
                XmlElement dotelement = doc.CreateElement("", "dot", "");
                note.AppendChild(dotelement);
            }

            XmlText stem_text = doc.CreateTextNode("up");
            stem.AppendChild(stem_text);
            note.AppendChild(stem);

            XmlText staff_text = doc.CreateTextNode(n.stave.ToString());
            staff.AppendChild(staff_text);
            note.AppendChild(staff);

            return note;
        }

        private XmlElement Backup_XML()
        {
            XmlElement backup = doc.CreateElement("", "backup", "");
            XmlElement duration = doc.CreateElement("", "duration", "");
            XmlText duration_text = doc.CreateTextNode((time_signature.top * time_signature.tick_divisions).ToString());
            duration.AppendChild(duration_text);
            backup.AppendChild(duration);

            return backup;
        }

        private XmlElement Generate_Measure_XML(int number)
        {
            XmlElement measure = doc.CreateElement("", "measure", "");
            measure.SetAttribute("number", number.ToString());

            if (number == 1)
                measure.AppendChild(Generate_Measure_New_Attributes());
            
            foreach (Note n in Generate_Bass(number))
            {
                measure.AppendChild(Note_To_XML(n));
            }

            measure.AppendChild(Backup_XML());

            foreach (Note n in Generate_Melody())
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
}
