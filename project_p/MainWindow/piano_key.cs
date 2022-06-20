using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace project_p
{
    public class PianoKey : Button
    {
        public static int white_width = 14;
        int white_height = 60;
        public static int black_width = 8;
        int black_height = 40;

        KeyColor color;
        public int number;

        public KeyState state = KeyState.Off;

        public enum KeyColor
        {
            White,
            Black
        };

        public enum KeyState
        {
            Off,
            Melody,
            Bass
        };

        public PianoKey(int n, KeyColor c)
        {
            color = c;
            number = n;

            if (c == KeyColor.Black)
                DrawBlackKey();
            else
                DrawWhiteKey();

            Click += PianoKey_Click;
            PreviewMouseRightButtonDown += PianoKey_PreviewMouseRightButtonDown;
        }

        private void PianoKey_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            state = KeyState.Off;
            StateChanged();
        }

        private void PianoKey_Click(object sender, RoutedEventArgs e)
        {
            if (state == KeyState.Off)
            {
                state = KeyState.Melody;
            }
            else if (state == KeyState.Melody)
            {
                state = KeyState.Bass;
            }
            else if (state == KeyState.Bass)
            {
                state = KeyState.Off;
            }

            StateChanged();
        }

        public void StateChanged()
        {
            if (state == KeyState.Melody)
            {
                Background = Brushes.RoyalBlue;
            }
            else if (state == KeyState.Bass)
            {
                Background = Brushes.OrangeRed;
            }
            else if (state == KeyState.Off)
            {
                if (color == KeyColor.Black)
                    Background = Brushes.Black;
                else
                    Background = Brushes.White;
            }
        }
    

        private void DrawBlackKey()
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Width = black_width;
            Height = black_height;
            Background = Brushes.Black;
            Margin = new Thickness((white_width - 1) * number + white_width - 1 - (black_width / 2), 0, 0, 0);
        }

        private void DrawWhiteKey()
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            Width = white_width;
            Height = white_height;
            Background = Brushes.White;
        }
    }
}
