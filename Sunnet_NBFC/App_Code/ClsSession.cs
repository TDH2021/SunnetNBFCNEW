using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunnet_NBFC.App_Code
{
    public static class ClsSession
    {
        public static int CompanyID { get; set; } = 0;
        public static int UserID { get; set; } = 0;
        public static int EmpId { get; set; } = 0;
        public static int RoleID { get; set; } = 0;
        public static int BranchId { get; set; } = 0;
        public static string UserType { get; set; } = "";


    }
}
