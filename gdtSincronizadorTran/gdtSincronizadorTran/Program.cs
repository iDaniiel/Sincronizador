using System;
using System.Windows.Forms;

namespace gdtSincronizadorTran
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new gdtFormTran());

            if (gdtFormTran.Error)
            {
                Console.WriteLine("Se termino el proceso con errores" );
            }
            Application.Exit();
        }
    }
}
