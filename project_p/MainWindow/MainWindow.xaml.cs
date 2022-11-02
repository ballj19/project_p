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
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace project_p
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int key_signature_offset = 14; //D

        public string filepath = @"C:\Users\ballj\source\repos\project_p\project_p\songs\iris2.xml";

        public MainWindow()
        {
            InitializeComponent();

            BassBuilder();
            PlayControl();

            LoadBar();

            FillBarBuilder();

            if (Timeline.Children.Count > 1)
                SortTicks();

            BarNumber.LostFocus += BarNumber_LostFocus;
            BarNumber.GotFocus += BarNumber_GotFocus;
        }

        private void BarNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void BarNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadBar();
        }

        private void NextBar_Click(object sender, RoutedEventArgs e)
        {
            Save();

            int new_bar = int.Parse(BarNumber.Text) + 1;

            BarNumber.Text = new_bar.ToString();
            LoadBar();
        }

        private void PrevBar_Click(object sender, RoutedEventArgs e)
        {
            Save();

            
            int new_bar = int.Parse(BarNumber.Text) - 1;

            if(new_bar > 0)
            {
                BarNumber.Text = new_bar.ToString();
                LoadBar();
            }
        }

        private void AddTickButton_Click(object sender, RoutedEventArgs e)
        {
            PianoControl p = new PianoControl();
            Timeline.Children.Add(p);
        }

        public void SortTicks()
        {
            bool redo = false;

            for(int i = 1; i < Timeline.Children.Count; i++)
            {
                PianoControl one_above = Timeline.Children[i - 1] as PianoControl;
                PianoControl p = Timeline.Children[i] as PianoControl;

                if (p.Tick() < one_above.Tick())
                {
                    Timeline.Children.Remove(p);
                    Timeline.Children.Insert(0, p);
                    redo = true;
                    break;
                }                
            }

            if (redo)
                SortTicks();
        }

        public bool HasTick(int t)
        {
            foreach (PianoControl p in Timeline.Children.OfType<PianoControl>())
            {
                if (p.Tick() == t)
                    return true;
            }

            return false;
        }

        public PianoControl GetTick(int t)
        {
            foreach (PianoControl p in Timeline.Children.OfType<PianoControl>())
            {
                if (p.Tick() == t)
                    return p;
            }

            return AddTick(t);
        }

        public bool TickHasMelody(int t)
        {
            return GetTick(t).HasMelody();
        }

        public PianoControl AddTick(int t)
        {
            PianoControl p = new PianoControl();
            p.SetTick(t);
            Timeline.Children.Add(p);

            SortTicks();

            return p;
        }

        private void RevisionButton_Click(object sender, RoutedEventArgs e)
        {
            Save();

            DateTime now = DateTime.Now;

            string prefix = now.ToShortDateString().Replace('/', '_');
            prefix += "." + now.ToLongTimeString().Replace(' ', '_').Replace(':','.') + "_";

            string dir = System.IO.Path.GetDirectoryName(filepath);
            string file = System.IO.Path.GetFileName(filepath);

            File.Copy(filepath, dir + "\\" + prefix + file);
        }

        private void DeleteBarButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNode bars = doc.DocumentElement.SelectSingleNode("//Bars");

            List<XmlNode> new_bars = new List<XmlNode>();

            foreach(XmlNode bar in bars.ChildNodes)
            {
                int bar_number = int.Parse(bar.Name.Substring(3));

                if ( bar_number > int.Parse(BarNumber.Text))
                {
                    XmlNode new_bar = doc.CreateElement("", "Bar" + (bar_number - 1), "");

                    new_bar.InnerXml = bar.InnerXml;

                    new_bars.Add(new_bar);
                }
                else if(bar_number < int.Parse(BarNumber.Text))
                {
                    new_bars.Add(bar);
                }

            }

            bars.RemoveAll();

            foreach(XmlNode bar in new_bars)
            {
                bars.AppendChild(bar);
            }                

            doc.Save(filepath);

            LoadBar();
        }

        private void InsertBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FillEighthNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < player.NumberOfTicksPerBar(); i += 2)
            {
                GetTick(i);
            }
        }

        private void FillQuarterNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 1; i < player.NumberOfTicksPerBar(); i += 4)
            {
                GetTick(i);
            }
        }

        private void FillBarBuilder()
        {
            if (!File.Exists(filepath))
                CreateSaveFile();

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNodeList bars = doc.DocumentElement.SelectNodes("//Bars/*[starts-with(name(), 'Bar')]");

            foreach (XmlNode bar in bars)
            {
                BarBuilder bb = new BarBuilder(this, bar);
                BarBuilder.Children.Add(bb);
            }
        }

    }
}
