using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Flow.Util
{
    public class SignalVisualizerFormBuilder
    {
        internal SignalVisualizerFormBuilder() { }
        public static SignalVisualizerFormBuilder New => new SignalVisualizerFormBuilder();
        static SignalVisualizerFormBuilder()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        
    }
}