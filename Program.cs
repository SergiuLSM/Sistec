using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistech
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login loginForm = new login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Obține utilizatorul autentificat din formularul de login
                string utilizatorConectat = loginForm.UtilizatorAutentificat;
                // Transmite utilizatorul autentificat către Form1
                Application.Run(new Form1(utilizatorConectat));
            }
        }
    }
}

