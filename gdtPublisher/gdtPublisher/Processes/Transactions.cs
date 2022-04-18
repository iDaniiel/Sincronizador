using gdtPublisher.Utils;
using System;

namespace gdtPublisher.Processes
{
    public static class Transactions
    {
        public static void Create(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                //Bitacora mensaje de error
                return;
            }

            string[] dataInfo  = data.Split('#');
            if (dataInfo.Length < 2)
            {
                Console.WriteLine("no viene completo el mensaje");
                return;
            }

            string nameProcess = dataInfo[0];
            string properties = dataInfo[1];

            if (nameProcess.Equals("CentralizarEmpleado"))
            {
                //Verificar de SonarQube
                nameProcess = PublisherProcess.GetNameProcessById(properties);
            }

            //Validamos que la ultima transaccion no sea igual a la actual ya que se trata de repetida
            if (PublisherProcess.lastTransaction == nameProcess)
            {
                return;
            }

            PublisherProcess.lastTransaction = nameProcess;

            Execute(nameProcess, properties);            
        }

        private static void Execute(string nameProcess, string properties)
        {
            TransacBase transac = null;
            TransacBase.Error = false;

            switch (nameProcess)
            {
                case "Login":
                case "Logout":
                case "Screensaver":
                    transac = new Session(nameProcess, properties);
                    break;
                case "Empleado":
                    transac = new Empleado(properties);
                    break;
                case "Rol":
                    transac = new Rol(properties);
                    break;
                case "Estacion":
                    transac = new Estacion(properties);
                    break;
                case "f12opcions":
                    transac = new SynchronizerF12(properties);
                    break;
                default:
                    TransacBase.Error = true;
                    break;
            }

            if (TransacBase.Error)
            {
                return;
            }

            transac.SaveTransaction();
        }
    }
}
