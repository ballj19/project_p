using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace project_p
{
    public partial class MainWindow
    {
        public MidiPlayer player;
        bool stop_playing = false;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        const int WM_SYSKEYDOWN = 0x0104;
        const int VK_SPACE = 0x20; //http://www.kbdedit.com/manual/low_level_vk_list.html
        const int VK_RETURN = 0x0D;

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


        private void PlayBarButton_Click(object sender, RoutedEventArgs e)
        {
            Save();

            ExportStart.Text = BarNumber.Text;
            ExportEnd.Text = BarNumber.Text;

            Play();

            /*XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlNode b = doc.SelectSingleNode("//Bars/Bar" + BarNumber.Text);

            await Task.Run(() =>
            {
                Bar bar = new Bar(b, player);
                bar.Play();
            });*/
        }

        public void PlayRange(int start, int end)
        {
            ExportStart.Text = start.ToString();
            ExportEnd.Text = end.ToString();

            Play();

            /*XmlDocument doc = new XmlDocument();
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
            }*/
        }

        private async void Play()
        {
            ExportMidi_Click(null, null);

            PostMessage(FindWindow(null, "Pianoteq STAGE v7.5.4"), WM_SYSKEYDOWN, VK_SPACE, 0);

            double sleep = (double)(60 * 1000 * player.time_signature_top) / (double)player.bpm;

            for (int i = int.Parse(ExportStart.Text); i <= int.Parse(ExportEnd.Text); i++)
            {
                if (i > BarBuilder.Children.Count)
                    break;

                BarBuilder b = BarBuilder.Children[i - 1] as BarBuilder;

                b.SetPlaying(true);

                await Task.Run(() =>
                {
                    Thread.Sleep((int)sleep);
                });

                b.SetPlaying(false);
            }

            //One extra sleep cycle
            await Task.Run(() =>
            {
                Thread.Sleep((int)sleep);
            });

            PostMessage(FindWindow(null, "Pianoteq STAGE v7.5.4"), WM_SYSKEYDOWN, VK_RETURN, 0);
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
