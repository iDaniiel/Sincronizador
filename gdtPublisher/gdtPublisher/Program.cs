using gdtPublisher.Utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace gdtPublisher
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Se comprueba qué no exista otro proceso del sincronizador en ejecución
            if (PublisherProcess.Exists)
            {
                return;
            }

            Bitacora.Write("");
            Bitacora.Write("***GDTPUBLISHER.EXE***");
            Bitacora.Write("-----------------------------------------------------------------------------");
            Bitacora.Write("");

            //Limpiamos el directorio de log de botacora para mantener los limites de archivos existentes
            DirectoryActions.Clean(Properties.Settings.Default.pathLogsBitacora,"gdtPublish*.*");

            //Actualizamos la funcionlidad cada 60 segundos
            new Thread(ExecutionParameters.Functionality).Start();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new gdtForm());
        }
    }
}
