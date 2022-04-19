using gdtSincronizadorTran.Utils;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Windows.Forms;

namespace gdtSincronizadorTran
{
    public partial class gdtFormTran : Form
    {
        public static bool Error { get; set; }
        
        public gdtFormTran()
        {
            InitializeComponent();
        }

        private void gdtFormTran_Load(object sender, EventArgs e)
        {
            ConcurrentQueue<QueueModel> queue = new ConcurrentQueue<QueueModel>();
            int numberTransactions = Properties.Settings.Default.numberOfTransactions;
            var moveFiles = new MoveFiles();
            
            try
            {
                var tmpFiles = Directory.GetFiles(Properties.Settings.Default.tmpDirectory, "*.tran");
                bool notExistFiles = tmpFiles.Length <= 0;

                if (notExistFiles)
                {
                    var resultMove = moveFiles.PendingToTMP();
                    if (!resultMove)
                    {
                        Error = true;
                        this.Close();
                    }
                }

                string[] filesDirectoryTMP = Directory.GetFiles(Properties.Settings.Default.tmpDirectory, "*.tran");
                foreach (string file in filesDirectoryTMP)
                {
                    var content = File.ReadAllLines(file);
                    foreach (string tran in content)
                    {
                        if (string.IsNullOrEmpty(tran.Trim()))
                        {
                            File.Delete(file);
                            continue;
                        }

                        queue.Enqueue(new QueueModel() { Transaction = tran, Path = file });
                    }
                }

                int countTransactions = 0;
                string query = "";
                
                foreach (var content in queue)
                {
                    Console.WriteLine(content.Transaction);
                    Console.WriteLine(content.Path);

                    query += $"{content.Transaction} ";
                    countTransactions++;

                    if (countTransactions == numberTransactions || countTransactions == queue.Count)
                    {
                        var send = true;
                        if (!send)
                        {
                            break;
                        }

                        while (countTransactions > 0)
                        {
                            QueueModel result;
                            if (queue.TryDequeue(out result))
                            {
                                Console.WriteLine("Se elimino de la cola " + result.Path);
                            }

                            moveFiles.TMPToProcessed(result.Path);
                            countTransactions--;
                        }   
                    }
                    Console.WriteLine("Transacciones en la cola: " + queue.Count);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Error = true;
                this.Close();
            }
        }
        
        public class QueueModel
        {
            public string Transaction { get; set; }
            public string Path { get; set; }
        }
    }
}
