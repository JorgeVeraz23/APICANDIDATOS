using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Enumerators
{
    public enum EnumAccessType
    {
        [Description("Dont Apply")]
        None = 0,
        [Description("LogIn")]
        LogIn = 1,
        [Description("LogOut")]
        LogOut = 2,
    }

    public enum EnumTypeDocument
    {
        [Description("RUC")]
        Ruc = 0,
        [Description("DNI")]
        DNI = 1,
        [Description("CEDULA")]
        CED = 2,
    }

    public class EnumParams
    {
        public static string SendMailSmtp = "SendMailSmtp";
        public static string SendMailPort = "SendMailPort";
        public static string SendMailUser = "SendMailUser";
        public static string SendMailPassword = "SendMailPassword";
        //Agregar los nuevo parametros que agreguen
        public static bool ValidateParams (string Params)
        {
            List<string> EnumParams = new()
            {
                "SendMailSmtp",
                "SendMailPort",
                "SendMailUser",
                "SendMailPassword"
            };
            return EnumParams.Contains(Params);
        }
    }

    public enum EnumTypeDate
    {
        [Description("No Text")]
        Texto = 0,
        [Description("Decimal")]
        Decimal = 1,
        [Description("Int")]
        Entero = 2,
        [Description("Date")]
        Fecha = 3,
    }


}
