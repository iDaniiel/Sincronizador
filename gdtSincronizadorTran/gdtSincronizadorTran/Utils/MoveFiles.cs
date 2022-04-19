using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gdtSincronizadorTran.Utils
{
    public class MoveFiles
    {

        public bool PendingToTMP()
        {
            string sourcePath = Properties.Settings.Default.pendingDirectory;
            string destPath = Properties.Settings.Default.tmpDirectory;

            try
            {
                string[] filesDirectoryPending = Directory.GetFiles(sourcePath, "*.tran");
                bool existFilesDirectory = filesDirectoryPending.Length > 0;
                if (!existFilesDirectory)
                {
                    return true;
                }

                foreach (string file in filesDirectoryPending)
                {
                    string fileName = Path.GetFileName(file);
                    File.Move(file, Path.Combine(destPath + fileName));
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool TMPToProcessed(string sourcePath)
        {
            string destPath = Properties.Settings.Default.processedDirectory;

            if (sourcePath == null)
            {
                Console.WriteLine("Directorio de procedencia nulos");
                return false;
            }

            try
            {
                var nameFile = Path.GetFileName(sourcePath);
                
                File.Move(sourcePath, Path.Combine(destPath,nameFile));
                return true;
            }
            catch (Exception ex)
            {
                File.Delete(sourcePath);
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
