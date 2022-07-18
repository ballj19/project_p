using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Controls;
using System.IO;

namespace project_p
{
    public partial class MainWindow
    {
        public void LoadBar()
        {   
            Timeline.Children.Clear();

            if (!File.Exists(filepath))
                CreateSaveFile();

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNode bar = doc.DocumentElement.SelectSingleNode("//Bars/Bar" + int.Parse(BarNumber.Text) + "/Ticks");

            if(bar != null)
            {
                foreach (XmlNode tick in bar.ChildNodes)
                {
                    LoadTick(tick);
                }
            }
        }

        private void LoadTick(XmlNode tick)
        {
            PianoControl p = new PianoControl();
            p.Load(tick);
            Timeline.Children.Add(p);
        }
    }
}
