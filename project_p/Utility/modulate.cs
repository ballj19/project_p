using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml;

namespace project_p
{
    public partial class MainWindow
    {

        private void ModulateButton_Click(object sender, RoutedEventArgs e)
        {
            int amount = int.Parse(ModulateAmount.Text);

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNodeList bars = doc.DocumentElement.SelectNodes("//Bars/*");

            
            foreach(XmlNode bar in bars)
            {
                foreach (XmlNode tick in bar.ChildNodes)
                {
                    foreach (XmlNode note in tick.ChildNodes)
                    {
                        foreach (XmlNode number in note.SelectNodes("Number"))
                        {
                            number.InnerText = (int.Parse(number.InnerText) + amount).ToString();
                        }
                    }
                }
            }

            doc.Save(filepath);

            LoadBar();
        }
    }
}
