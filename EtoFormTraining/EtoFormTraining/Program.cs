using Eto.Forms;
using System;

namespace EtoFormTraining
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            new Application(Eto.Platform.Detect).Run(new MainForm());
        }
    }
}