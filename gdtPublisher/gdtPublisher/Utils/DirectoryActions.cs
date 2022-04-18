using gdtPublisher.Processes;
using System;
using System.IO;
using System.Linq;

namespace gdtPublisher.Utils
{
    public static class DirectoryActions
    {
        public static void Clean(string path, string patpattern)
        {
            DateTime fileDate;
            int days = Properties.Settings.Default.days;

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!Directory.Exists(path))
            {
                Bitacora.Write($"No existe el directorio: {path}");
                return;
            }

            try
            {
                var currentDate = DateTime.Now.AddDays(-days);

                string[] files = Directory.GetFiles(path, patpattern);
                bool filesExist = files.Count() > 0;

                if (!filesExist)
                {
                    return;
                }

                foreach (string file in files)
                {
                    fileDate = File.GetLastWriteTime(file);
                    if (fileDate.Date < currentDate.Date)
                    {
                        File.Delete(file);
                    }
                }

                Bitacora.Write($"Se realizó limpieza del directorio {path} con fecha anterior a {days} días");
            }
            catch (Exception e)
            {
                Bitacora.Write($"Error al limpiar el directorio {path}: {e.Message}");
                return;
            }
        }

        public static void SaveTransaction(this TransacBase transacBase)
        {   
            try
            {
                Type t = Type.GetType(transacBase.ToString());
                string nameInstance = t.Name;

                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                string logTran = string.Format(Properties.Settings.Default.pendingDirectory + "gdtPublisher{0}_{1}.tran", Environment.MachineName, date);
                string text = string.Empty;

                switch (nameInstance)
                {
                    case "Session":
                    case "f12opcions":
                        var sesion = (Session)transacBase;
                        text = sesion.GetFormat();
                        break;
                    case "Empleado":
                        var empleado = (Empleado)transacBase;
                        text = empleado.GetFormat();
                        break;
                    case "Rol":
                        var rol = (Rol)transacBase;
                        text = rol.GetFormat();
                        break;
                    case "Estacion":
                        var estacion = (Estacion)transacBase;
                        text = estacion.GetFormat();
                        break;
                    default:
                        break;
                }

                File.AppendAllText(logTran, text);
            }
            catch (Exception e)
            {
                Bitacora.Write("Error al persistir: " + e.Message);
                return;
            }
        }
    }
}
