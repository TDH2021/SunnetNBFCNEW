using System.Web;
using System.Web.Optimization;

namespace Sunnet_NBFC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/bundles/Layoutcss").Include(
                 "~/dist/css/sweetalert2.css",
                      "~/dist/css/sweetalert2.min.css",
                 "~/plugins/fontawesome-free/css/all.min.css", "~/dist/css/jquery-ui.css",
                      "~/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                      "~/dist/css/adminlte.css",
                      "~/Scripts/sweetalert2.js"));

            //bundles.Add(new StyleBundle("~/bundles/SweetAlertCS").Include(
            //          "~/dist/css/sweetalert2.css",
            //          "~/dist/css/sweetalert2.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/plugins/jquery/jquery-1.10.2.js",
                "~/plugins/jquery/jquery.min.js", "~/plugins/jquery/jquery.validate.min.js", "~/plugins/jquery/jquery-ui.js",
                "~/plugins/jquery/jquery.validate.unobtrusive.min.js","~/Scripts/angular.min.js",
                "~/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/plugins/datatables/jquery.dataTables.min.js",
                "~/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                "~/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                "~/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                "~/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                "~/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                "~/Scripts/moment.min.js",
                "~/plugins/pdfmake/pdfmake.min.js",
                "~/plugins/pdfmake/vfs_fonts.js",
                "~/plugins/datatables-buttons/js/buttons.html5.min.js",
                "~/plugins/datatables-buttons/js/buttons.print.min.js",
                "~/plugins/datatables-buttons/js/buttons.colVis.min.js",
                "~/dist/js/adminlte.min.js",
                "~/dist/js/demo.js"));



            bundles.Add(new StyleBundle("~/bundles/Logincss").Include(
                "~/plugins/fontawesome-free/css/all.min.css",
                     "~/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                     "~/dist/css/adminlte.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/Loginjs").Include(
                "~/plugins/jquery/jquery.min.js", "~/plugins/jquery/jquery.validate.min.js",
                "~/plugins/jquery/jquery.validate.unobtrusive.min.js", "~/Scripts/angular.min.js",
                "~/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/dist/js/adminlte.min.js"));


        }
    }
}
