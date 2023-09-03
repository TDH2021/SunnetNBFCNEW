using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sunnet_NBFC.Models
{
    public class Enums
    {
        public enum ProductType 
        {
            [Description("Individual Loan")]
            P = 1,
            [Description("Vehicle Loan")]
            V = 2,
            [Description("Bussiness Loan")]
            B = 3,
        }
    }
}