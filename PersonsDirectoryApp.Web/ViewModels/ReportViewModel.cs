using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.ViewModels
{
    public class ReportViewModel
    {
        [DisplayName("ID")]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Personal No")]
        public string PersonalNo { get; set; }
        [DisplayName("Colleagues")]
        public int ColleagueCount { get; set; }
        [DisplayName("Acquaintances")]
        public int AcquaintanceCount { get; set; }
        [DisplayName("Relatives")]
        public int RelativeCount { get; set; }
        [DisplayName("Others")]
        public int OtherCount { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
