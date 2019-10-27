using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Data.Models
{
    public class Enums
    {
        public enum Gender
        {
            Female = 0,
            Male = 1
        }

        public enum TelephoneType
        {
            Mobile,
            Home,
            Office
        }

        public enum RelationType
        {
            Colleague,
            Acquaintance,
            Relative,
            Other
        }
    }
}
