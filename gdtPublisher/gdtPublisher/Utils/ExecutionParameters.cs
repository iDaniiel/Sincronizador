using System.Threading;

namespace gdtPublisher.Utils
{
    public static class ExecutionParameters
    {
        public static void Functionality()
        {
            while (true)
            {
                //Agregar codigo de PD3
                //Se obtiene el valor del item
                bool getValue = true;
                if (getValue != PublisherProcess.activeFunctionality)
                {
                    PublisherProcess.activeFunctionality = getValue;
                    Bitacora.Write("¿La funciónalidad activa? " + PublisherProcess.activeFunctionality);
                }
                
                Thread.Sleep(60000);
            }
        }
    }
}
