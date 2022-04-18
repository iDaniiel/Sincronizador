using gdtPublisher.Utils;
using System;
using System.Runtime.Serialization;

namespace gdtPublisher.Processes
{
    [DataContract]
    public class Session : TransacBase
    {
        public Session(string nameProcces, string properties)
        {
            bool notValidName = string.IsNullOrEmpty(nameProcces);
            bool notValidProperties = string.IsNullOrEmpty(properties);

            if (notValidName || notValidProperties)
            {
                Error = true;
                return;
            }

            string[] data = properties.Split(',');
            foreach (string property in data)
            {
                if (string.IsNullOrEmpty(property))
                {
                    Error = true;
                    Bitacora.Write($"La propiedad {property} es null o empty");

                    return;
                }
            }

            var date = DateTime.Now.ToString(Properties.Settings.Default.dateFormat);

            piTipoOperacion = nameProcces.GetIdProcessByName();
            fcHash = Hash.Get(nameProcces + date);
            piNoEmp = Convert.ToInt32(data[1]);
            pcNombreCorto = data[2];
            piNoTienda = Convert.ToInt32(data[3]);
            piPuestoBaseEmpAfec = Convert.ToInt32(data[4]);
            pdFechaMovimiento = date;
            piRolEmpleado = Convert.ToInt32(data[5]);
            pcIpEstacion = data[6];
            pcWS = data[7];
        }

        [DataMember(Name ="numeroEmpleado", Order = 3)]
        public int piNoEmp { get; set; }
        [DataMember(Order = 4)]
        public string pcNombreCorto { get; set; }
        [DataMember(Order = 5)]
        public int piNoTienda { get; set; }
        [DataMember(Order = 6)]
        public int piPuestoBaseEmpAfec { get; set; }
        [DataMember(Order = 7)]
        public string pdFechaMovimiento { get; set; }
        [DataMember(Order = 8)]
        public int piRolEmpleado { get; set; }
        [DataMember(Order = 9)]
        public string pcIpEstacion { get; set; }
        [DataMember(Order = 10)]
        public string pcWS { get; set; }
    }
}
