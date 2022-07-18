using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace project_p
{
    public class PianoControl : StackPanel
    {
        public Grid piano;
        TextBox tick;
        Button delete;

        public PianoControl()
        {
            Orientation = Orientation.Horizontal;

            Margin = new Thickness(0, 20, 0, 0);

            tick = new TextBox
            {
                Text = "16",
                Width = 20,
                Height = 20,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5, 20, 0, 0)
            };

            delete = new Button
            {
                Content = "X",
                Height = 20,
                Width = 20,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 20, 0, 0),                
            };

            delete.Click += Delete_Click;
            tick.LostFocus += Tick_LostFocus;

            DrawPiano();

            Children.Add(tick);
            Children.Add(piano);
            Children.Add(delete);
        }

        private void Tick_LostFocus(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.MainWindow as MainWindow;

            mw.SortTicks();
        }

        public int Tick()
        {
            return int.Parse(tick.Text);
        }

        public void SetTick(int t)
        {
            tick.Text = t.ToString();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            StackPanel p = Parent as StackPanel;
            p.Children.Remove(this);
        }

        private void DrawPiano()
        {
            piano = new Grid
            {
                Margin = new Thickness(20, 0, 0, 0),
                
            };

            int[] black_keys = { 22, 25, 27, 30, 32, 34, 37, 39, 42, 44, 46, 49, 51, 54, 56, 58, 61, 63, 66, 68, 70, 73, 75, 78, 80, 82, 85, 87, 90, 92, 94, 97, 99, 102, 104, 106 };

            int x_margin = 0;

            for (int i = 21; i <= 108; i++)
            {

                if (!black_keys.Contains(i))
                {
                    PianoKey key = new PianoKey(i, PianoKey.KeyColor.White);
                    key.Margin = new Thickness((PianoKey.white_width - 1) * x_margin, 0, 0, 0);
                    SetZIndex(key, 1);
                    piano.Children.Add(key);
                    x_margin++;
                }
                else
                {
                    PianoKey key = new PianoKey(i, PianoKey.KeyColor.Black);
                    key.Margin = new Thickness((PianoKey.white_width - 1) * x_margin - (PianoKey.black_width / 2), 0, 0, 0);
                    SetZIndex(key, 2);
                    piano.Children.Add(key);

                }
            }
                       
            Ellipse center_c = new Ellipse
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 4,
                Height = 4,
                Margin = new Thickness(22 * PianoKey.white_width - 4, 50, 0, 0),
                Fill = Brushes.Black
            };

            SetZIndex(center_c, 2);

            piano.Children.Add(center_c);
        }

        public XmlElement Save(XmlDocument doc)
        {
            XmlElement tick_xml = doc.CreateElement("", "Tick" + tick.Text, "");

            foreach(PianoKey key in piano.Children.OfType<Button>())
            {
                if (key.state == PianoKey.KeyState.Off)
                    continue;

                XmlElement note = doc.CreateElement("", "Note", "");

                XmlElement type = doc.CreateElement("", "Type", "");
                XmlText type_text = key.state == PianoKey.KeyState.Melody ? doc.CreateTextNode("Melody") : doc.CreateTextNode("Bass");
                type.AppendChild(type_text);
                note.AppendChild(type);

                XmlElement num = doc.CreateElement("", "Number", "");
                XmlText num_text = doc.CreateTextNode(key.number.ToString());
                num.AppendChild(num_text);
                note.AppendChild(num);                   

                tick_xml.AppendChild(note);
            }

            return tick_xml;
        }

        public void Load(XmlNode node)
        {
            string tick_number = node.Name.Substring(4, node.Name.Length - 4);
            tick.Text = tick_number;

            foreach(XmlNode note in node)
            {
                int number = int.Parse(note.SelectSingleNode("Number").InnerText);
                PianoKey.KeyState state = note.SelectSingleNode("Type").InnerText == "Melody" ? PianoKey.KeyState.Melody : PianoKey.KeyState.Bass;

                foreach (PianoKey key in piano.Children.OfType<Button>())
                {
                    if (key.number == number)
                    {
                        key.state = state;
                        key.StateChanged();
                    }
                }
            }
        }

        public void SetPianoKey(byte number, PianoKey.KeyState state)
        {
            foreach (PianoKey key in piano.Children.OfType<Button>())
            {
                if (key.number == number)
                {
                    key.state = state;
                    key.StateChanged();
                }
            }
        }

        public bool HasMelody()
        {
            foreach (PianoKey key in piano.Children.OfType<Button>())
            {
                if (key.state == PianoKey.KeyState.Melody)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
