using gdtPublisher.Utils;
using System;
using System.Web.Script.Serialization;

namespace gdtPublisher.Processes
{
    public class Rol : TransacBase
    {
        public Rol(string properties)
        {
            var serializer = new JavaScriptSerializer();
            var data = (RolModel)serializer.Deserialize(properties, typeof(RolModel));
            string fecha = DateTime.Now.ToString(Properties.Settings.Default.dateFormat);

            piTipoOperacion = data.OperacionId;
            fcHash = Hash.Get(properties + fecha);
            piNoEmpAfect = data.EmpleadoAfectado;
            piNoTienda = data.Tienda;
            piPuestoBaseEmpAfec = data.PuestoRolEmpAfec;
            piPuestoRolEmpAfec = data.PuestoRolEmpAfec;
            piNoEmpAsigno = data.EmpleadoAsigno;
            piPuestoBaseEmpAsigno = data.PuestoBaseAsigno;
            piRolEmpAsigno = data.PuestoRolAsigno;
            pdFechaInicioVig = data.FechaInicioVigencia.Replace("/","-");
            pdFechaFinVig = data.FechaFinVigencia.Replace("/","-");
            pdFechaMovimiento = data.FechaMovimiento;
            pcWS = data.EstacionOperacion;
            piRolCompleto = data.RolCompleto ? 1 : 0;
        }
        
        public int piNoEmpAfect { get; set; }
        public int piNoTienda { get; set; }
        public int piPuestoRolEmpAfec { get; set; }
        public int piPuestoBaseEmpAfec { get; set; }
        public int piNoEmpAsigno { get; set; }
        public string pdFechaMovimiento { get; set; }
        public int piPuestoBaseEmpAsigno { get; set; }
        public int piRolEmpAsigno { get; set; }
        public string pdFechaInicioVig { get; set; }
        public string pdFechaFinVig { get; set; }
        public string pcWS { get; set; }
        public int piRolCompleto { get; set; }
        public string pcBitacoraDetalle { get; set; }
    }

    public class RolModel
    {
        public int OperacionId { get; set; }
        public int EmpleadoAfectado { get; set; }
        public int Tienda { get; set; }
        public int PuestoBaseEmpAfec { get; set; }
        public int PuestoRolEmpAfec { get; set; }
        public int EmpleadoAsigno { get; set; }
        public int PuestoBaseAsigno { get; set; }
        public int PuestoRolAsigno { get; set; }
        public string FechaInicioVigencia { get; set; }
        public string FechaFinVigencia { get; set; }
        public string FechaMovimiento { get; set; }
        public string EstacionOperacion { get; set; }
        public bool RolCompleto { get; set; }
    }
}
