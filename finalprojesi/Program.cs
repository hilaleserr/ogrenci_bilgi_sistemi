using System;
using System.Windows.Forms;

namespace finalprojesi
{
    static class Program
    {
        public static int Duzenlenecek_ID; 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
            Application.Run(new Form1());
        }
    }
}
