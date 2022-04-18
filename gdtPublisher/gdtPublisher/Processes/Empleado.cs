using gdtPublisher.Utils;
using System;
using System.Web.Script.Serialization;

namespace gdtPublisher.Processes
{
    public class Empleado : TransacBase
    {
        public Empleado(string properties)
        {
            bool notValidProperties = string.IsNullOrEmpty(properties);
            if (notValidProperties)
            {
                return;
            }

            var serializer = new JavaScriptSerializer();
            var data = (EmpleadoJsonModel)serializer.Deserialize(properties, typeof(EmpleadoJsonModel));
            string fecha = DateTime.Now.ToString(Properties.Settings.Default.dateFormat);

            piTipoOperacion = data.OperacionId;
            fcHash = Hash.Get(properties + fecha);
            piNoEmp = data.EmpleadoAfec;
            piNoTienda = data.Tienda;
            piPuestoBaseEmpAfec = data.PuestoBaseEmpAfec;
            pdFechaMovimiento = data.FechaMovimiento;
            piNoEmpleadoJefe = data.EmpleadoJefe;
            piPuestoBaseJefe = data.PuestoBaseJefe;
            pcNombreCorto = data.NombreCorto;
            pcWS = data.EstacionOperacion;
        }

        public int piNoEmp { get; set; }
        public int piNoTienda { get; set; }
        public int piPuestoBaseEmpAfec { get; set; }
        public string pdFechaMovimiento { get; set; }
        public int piNoEmpleadoJefe { get; set; }
        public int piPuestoBaseJefe { get; set; }
        public string pcNombreCorto { get; set; }
        public string pcWS { get; set; }
        public string pcBitacoraDetalle { get; set; }
    }

    #region EmpleadoJsonModel
    public class EmpleadoJsonModel
    {
        public int OperacionId { get; set; }
        public int Tienda { get; set; }
        public int EmpleadoAfec { get; set; }
        public string NombreCorto { get; set; }
        public int PuestoBaseEmpAfec { get; set; }
        public int EmpleadoJefe { get; set; }
        public int PuestoBaseJefe { get; set; }
        public int PuestoRolJefe { get; set; }
        public string FechaMovimiento { get; set; }
        public string EstacionOperacion { get; set; }
    }
    #endregion
}
