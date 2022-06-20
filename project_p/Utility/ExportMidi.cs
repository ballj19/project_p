using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M;
using System.IO;
using System.Windows;

namespace project_p
{
    public partial class MainWindow
    {
        private void ExportMidi_Click(object sender, RoutedEventArgs e)
        {
            M.MidiFile mf;

            using (Stream stm = File.OpenRead(@"C:\Users\Jake\Downloads\A_Whole_New_World.mid"))
                mf = M.MidiFile.ReadFrom(stm);

            mf = mf.AdjustTempo(60);

            int test = 0;
        }
    }
}
