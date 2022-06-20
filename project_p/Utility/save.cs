using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.IO;

namespace project_p
{
    public partial class MainWindow
    {
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void CreateSaveFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement song = doc.CreateElement("", "Song", "");
            XmlElement bar = doc.CreateElement("", "Bars", "");

            song.AppendChild(bar);
            doc.AppendChild(song);
            doc.Save(filepath);
        }

        public void Save()
        {
            if (!File.Exists(filepath))
                CreateSaveFile();

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNode bars = doc.SelectSingleNode("//Bars");

            XmlNode current_bar = doc.SelectSingleNode("//Bars/Bar" + BarNumber.Text);

            if (current_bar == null)
                current_bar = doc.CreateElement("", "Bar" + BarNumber.Text, "");

            current_bar.RemoveAll();

            foreach (PianoControl p in Timeline.Children)
            {
                XmlElement tick_xml = p.Save(doc);
                current_bar.AppendChild(tick_xml);
            }

            bars.InsertAt(current_bar, int.Parse(BarNumber.Text) - 1);

            doc.Save(filepath);
        }
    }
}
