using System.Web;
using System.Web.Optimization;

namespace EsquemasSecundarios
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //-- CSS --\\

            bundles.Add(new StyleBundle("~/Style/Bootstrap").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Style/Fonts").Include(
                     "~/Content/vendors/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Style/NProgress").Include(
                     "~/Content/vendors/nprogress/nprogress.css"));

            bundles.Add(new StyleBundle("~/Style/DataTables").Include(
                      "~/Content/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css"));  

            bundles.Add(new StyleBundle("~/Style/iCheck").Include(
                     "~/Content/vendors/iCheck/skins/flat/green.css"));

            bundles.Add(new StyleBundle("~/Style/Select2").Include(
                     "~/Content/vendors/select2/dist/css/select2.min.css"));

            bundles.Add(new StyleBundle("~/Style/DateRangePicker").Include(
                     "~/Content/daterangepicker.css"));

            bundles.Add(new StyleBundle("~/Style/Switchery").Include(
                     "~/Content/vendors/switchery/dist/switchery.min.css"));

            bundles.Add(new StyleBundle("~/Style/Custom").Include(
                     "~/Content/custom.min.css"));

            //-- JS --\\

            bundles.Add(new ScriptBundle("~/Script/jQuery").Include(
                        "~/Content/vendors/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Script/Bootstrap").Include(
                        "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Script/DateRangePicker").Include(
                       "~/Scripts/moment.min.js",
                       "~/Scripts/daterangepicker.js",
                       "~/Scripts/Inicializadores/daterangepicker.js"));

            bundles.Add(new ScriptBundle("~/Script/FastClick").Include(
                        "~/Content/vendors/fastclick/lib/fastclick.js"));

            bundles.Add(new ScriptBundle("~/Script/NProgress").Include(
                        "~/Content/vendors/nprogress/nprogress.js"));

            bundles.Add(new ScriptBundle("~/Script/iCheck").Include(
                        "~/Content/vendors/iCheck/icheck.min.js"));

            bundles.Add(new ScriptBundle("~/Script/Switchery").Include(
                        "~/Content/vendors/switchery/dist/switchery.min.js"));

            bundles.Add(new ScriptBundle("~/Script/Select2").Include(
                        "~/Content/vendors/select2/dist/js/select2.full.min.js",
                        "~/Scripts/Lenguaje/select2.es.js",
                        "~/Scripts/Inicializadores/select2.js"));

            bundles.Add(new ScriptBundle("~/Script/Parsley").Include(
                        "~/Content/vendors/parsleyjs/dist/parsley.min.js"));

            bundles.Add(new ScriptBundle("~/Script/DataTables").Include(
                "~/Content/vendors/datatables.net/js/jquery.dataTables.min.js",
                "~/Content/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/Content/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/Content/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/Content/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                "~/Content/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                "~/Content/vendors/datatables.net-buttons/js/buttons.print.min.js",
                "~/Scripts/Lenguaje/datatables.es.js"
                ));

            bundles.Add(new ScriptBundle("~/Script/Switchery").Include(
                "~/Content/vendors/switchery/dist/switchery.min.js"));

             bundles.Add(new ScriptBundle("~/Script/Custom").Include(
                "~/Scripts/custom.min.js"));
        }
    }
}
