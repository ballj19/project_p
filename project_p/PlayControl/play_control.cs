using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace project_p
{
    public partial class MainWindow
    {
        MidiPlayer player;
        bool stop_playing = false;

        private void PlayControl()
        {
            player = new MidiPlayer();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
            stop_playing = false;
            PlayRange(int.Parse(BarStart.Text), int.Parse(BarEnd.Text));
        }


        private async void PlayBarButton_Click(object sender, RoutedEventArgs e)
        {
            Save();

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNode b = doc.SelectSingleNode("//Bars/Bar" + BarNumber.Text);

            await Task.Run(() =>
            {
                Bar bar = new Bar(b, player);
                bar.Play();
            });
        }

        private async void PlayRange(int start, int end)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNodeList bars = doc.SelectSingleNode("//Bars").ChildNodes;

            foreach (XmlNode b in bars)
            {
                if (stop_playing)
                    break;

                int bar_number = int.Parse(b.Name.Substring(3));

                DisplayBar(bar_number);

                if (bar_number >= start && bar_number <= end)
                {
                    await Task.Run(() =>
                    {
                        Bar bar = new Bar(b, player);
                        bar.Play();
                    });
                }
            }
        }

        private void DisplayBar(int bar)
        {
            //BarNumber.Text = bar.ToString();
            //LoadBar();
        }


        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            stop_playing = true;
        }

    }
}
