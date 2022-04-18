using gdtPublisher.Processes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace gdtPublisher.Utils
{
    public static class PublisherProcess
    {
        public const string nameProcess = "gdtPublisher";

        #region Ids de procesos transaccion
        public const int ID_ALTA_EMPLEADO = 1;
        public const int ID_REACTIVACION_EMPLEADO = 2;
        public const int ID_BAJA_EMPLEADO = 3;
        public const int ID_CAMBIO_PUESTO_EMPLEADO = 4;
        public const int ID_ASIGNACION_ROL = 5;
        public const int ID_ACTUALIZACION_ROL = 6;
        public const int ID_INACTIVACION_ROL = 7;
        public const int ID_AUTORIZA_USO_ESTACION = 8;
        public const int ID_DESACTIVA_USO_ESTACION = 9;
        public const int ID_LOGIN = 10;
        public const int ID_SCREENSAVER = 11;
        public const int ID_LOGOUT = 12;
        public const int ID_KERNEL_REBOOT = 13;
        public const int ID_REINICIO_EQUIPO = 14;
        public const int ID_APAGADO_EQUIPO = 15;
        public const int ID_SALIDA_SOPORTE = 16;
        public const int ID_INTEGRACION = 17;
        #endregion



        public static bool activeFunctionality { get; set; }
        public static string lastTransaction { get; set; }

        //Variables de sesion activa
        public static bool InSession { get; set; }
        public static string SessionParameters { get; set; }

        public static bool Exists { get { return Process.GetProcessesByName(nameProcess).Count() > 1; } }

        public static int GetIdProcessByName(this string name)
        {
            switch (name)
            {
                case "Login":
                    return ID_LOGIN;
                case "Logout":
                    return ID_LOGOUT;
                case "Screensaver":
                    return ID_SCREENSAVER;
                default:
                    return -1;
            }
        }

        public static string GetNameProcessById(string properties)
        {
            bool notValidProperties = string.IsNullOrEmpty(properties);
            if (notValidProperties)
            {
                Bitacora.Write("Las propiedades de Centralización de empleados vacia");
                return string.Empty;
            }

            var serializer = new JavaScriptSerializer();
            var number = serializer.Deserialize<TypeOperation>(properties);

            switch (number.OperacionId)
            {
                case ID_ALTA_EMPLEADO:
                case ID_REACTIVACION_EMPLEADO:
                case ID_BAJA_EMPLEADO:
                case ID_CAMBIO_PUESTO_EMPLEADO:
                    return "Empleado";
                case ID_ASIGNACION_ROL:
                case ID_ACTUALIZACION_ROL:
                case ID_INACTIVACION_ROL:
                    return "Rol";
                case ID_AUTORIZA_USO_ESTACION:
                case ID_DESACTIVA_USO_ESTACION:
                    return "Estacion";
                default:
                    return string.Empty;
            }
        }

        public static string GetFormat(this Session transacBase)
        {
            var serialize = new JavaScriptSerializer();
            string json = serialize.Serialize(transacBase);

            var str = new StringBuilder();
            str.Append("EXEC spSTORNSesion ")
                .Append($"{transacBase.piTipoOperacion},")
                .Append($"{transacBase.piNoEmp},")
                .Append($"'{transacBase.pcNombreCorto}',")
                .Append($"{transacBase.piNoTienda},")
                .Append($"{transacBase.piPuestoBaseEmpAfec},")
                .Append($"'{transacBase.pdFechaMovimiento}',")
                .Append($"{transacBase.piRolEmpleado},")
                .Append($"'{transacBase.pcIpEstacion}',")
                .Append($"'{transacBase.pcWS}',")
                .Append($"'{json}'");

            return str.ToString();
        }

        public static string GetFormat(this Empleado transacBase)
        {
            var serialize = new JavaScriptSerializer();
            string json = serialize.Serialize(transacBase);

            var str = new StringBuilder();
            str.Append("EXEC spSRERNEmpleado ")
                .Append($"{transacBase.piTipoOperacion},")
                .Append($"{transacBase.piNoEmp},")
                .Append($"{transacBase.piNoTienda},")
                .Append($"{transacBase.piPuestoBaseEmpAfec},")
                .Append($"'{transacBase.pdFechaMovimiento}',")
                .Append($"{transacBase.piNoEmpleadoJefe},")
                .Append($"'{transacBase.piPuestoBaseJefe}',")
                .Append($"'{transacBase.pcNombreCorto}',")
                .Append($"'{transacBase.pcWS}',")
                .Append($"'{json}'");

            return str.ToString();
        }

        public static string GetFormat(this Rol transacBase)
        {
            var serialize = new JavaScriptSerializer();
            string json = serialize.Serialize(transacBase);

            var str = new StringBuilder();
            str.Append("EXEC spSRERNRol ")
                .Append($"{transacBase.piTipoOperacion},")
                .Append($"{transacBase.piNoEmpAfect},")
                .Append($"{transacBase.piNoTienda},")
                .Append($"{transacBase.piPuestoRolEmpAfec},")
                .Append($"{transacBase.piPuestoBaseEmpAfec},")
                .Append($"'{transacBase.pdFechaMovimiento}',")
                .Append($"{transacBase.piPuestoBaseEmpAsigno},")
                .Append($"{transacBase.piRolEmpAsigno},")
                .Append($"'{transacBase.pdFechaInicioVig}',")
                .Append($"'{transacBase.pdFechaFinVig}',")
                .Append($"'{transacBase.pcWS}',")
                .Append($"{transacBase.piRolCompleto},")
                .Append($"'{json}'");

            return str.ToString();
        }

        public static string GetFormat(this Estacion transacBase)
        {
            var serialize = new JavaScriptSerializer();
            string json = serialize.Serialize(transacBase);

            var str = new StringBuilder();
            str.Append("EXEC spSRERNEstacion  ")
                .Append(transacBase.piTipoOperacion).Append(",")
                .Append(transacBase.piTiendaId).Append(",")
                .Append(transacBase.piPuestoBaseAsigno).Append(",")
                .Append("'" + transacBase.pcEstacionAsigna + "'").Append(",")
                .Append("'" + transacBase.pcPuestos + "'").Append(",")
                .Append("'" + transacBase.pdFechaMovimiento + "'").Append(",")
                .Append("'" + transacBase.pcEstacionOperacion + "'").Append(",")
                .Append("'" + transacBase.pcBitacoraDetalle + "'");

            return str.ToString();
        }
    }

    public class TypeOperation
    {
        public int OperacionId { get; set; }
    }
}
