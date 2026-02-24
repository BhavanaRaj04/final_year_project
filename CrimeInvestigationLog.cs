using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedEncryptedCrime
{
    public class CrimeInvestigationLog
    {
        public string StaffId { get; set; }
        public string StaffRole { get; set; }
        public string CrimeId { get; set; }
        public string LogDate { get; set; }
        public string Description { get; set; }
    }
}