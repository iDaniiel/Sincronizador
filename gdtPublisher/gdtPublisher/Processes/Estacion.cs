using gdtPublisher.Utils;
using System;
using System.Web.Script.Serialization;

namespace gdtPublisher.Processes
{
    public class Estacion : TransacBase
    {
        public Estacion(string properties)
        {
            var serialize = new JavaScriptSerializer();
            var data = (EstacionModdel)serialize.Deserialize(properties, typeof(EstacionModdel));
            string fecha = DateTime.Now.ToString(Properties.Settings.Default.dateFormat);

            piTipoOperacion = data.OperacionId;
            fcHash = Hash.Get(properties + fecha);
            piTiendaId = data.Tienda;
            piPuestoBaseAsigno = data.PuestoBaseAutorizo;
            pcEstacionAsigna = data.EstacionAsignado;
            pcPuestos = data.PuestosAutorizados;
            pdFechaMovimiento = data.FechaMovimiento;
            pcEstacionOperacion = data.EstacionOperacion;
            puestosNoAutorizados = data.PuestosNoAutorizados;
            empleadoAutorizo = data.EmpleadoAutorizo;
            puestoRolAutorizo = data.PuestoRolAutorizo;
        }

        public int piTiendaId { get; set; }
        public int piPuestoBaseAsigno { get; set; }
        public string pcEstacionAsigna { get; set; }
        public string pcPuestos { get; set; }
        public string pdFechaMovimiento { get; set; }
        public string pcEstacionOperacion { get; set; }
        public string puestosNoAutorizados { get; set; }
        public int empleadoAutorizo { get; set; }
        public int puestoRolAutorizo { get; set; }
        public string pcBitacoraDetalle { get; set; }
    }

    #region EstacionModdel
    public class EstacionModdel
    {
        public int OperacionId { get; set; }
        public int Tienda { get; set; }
        public string PuestosAutorizados { get; set; }
        public string PuestosNoAutorizados { get; set; }
        public string EstacionAsignado { get; set; }
        public int EmpleadoAutorizo { get; set; }
        public int PuestoBaseAutorizo { get; set; }
        public int PuestoRolAutorizo { get; set; }
        public string FechaMovimiento { get; set; }
        public string EstacionOperacion { get; set; }
    }
    #endregion
}
