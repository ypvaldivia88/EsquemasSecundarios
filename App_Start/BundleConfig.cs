using System.Web;
using System.Web.Optimization;

namespace EsquemasSecundarios
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS !!!!!!!!!!!!!!!!!!
            /* Bootstrap */
            bundles.Add(new StyleBundle("~/Style/Bootstrap").Include(
                      "~/Content/libs/bootstrap/dist/css/bootstrap.min.css"));
            /* Fonts */
            bundles.Add(new StyleBundle("~/Style/Fonts").Include(
                     "~/Content/libs/font-awesome/css/font-awesome.min.css"));
            /* Datatables */
            bundles.Add(new StyleBundle("~/Style/DataTables").Include(
                      "~/Content/libs/datatables.net-bs/css/dataTables.bootstrap.min.css"));
            /* DataTablesButtons */
            bundles.Add(new StyleBundle("~/Style/DT-Buttons").Include(
                    "~/Content/libs/datatables.net-buttons-bs/css/buttons.bootstrap.min.css"
                ));       
            /* iCheck */
            bundles.Add(new StyleBundle("~/Style/iCheck").Include(
                     "~/Content/libs/iCheck/skins/flat/green.css"));
            /* Select2 */
            bundles.Add(new StyleBundle("~/Style/Select2").Include(
                     "~/Content/libs/select2/dist/css/select2.min.css"));
            /* Custom Theme Style */
            bundles.Add(new StyleBundle("~/Style/Custom").Include(
                     "~/Content/css/custom.css"));

            // JS !!!!!!!!!!!!!!!!!!!
            /* jQuery */
            bundles.Add(new ScriptBundle("~/Script/jQuery").Include(
                        "~/Content/libs/jquery/dist/jquery.min.js"));
            /* Bootstrap */
            bundles.Add(new ScriptBundle("~/Script/Bootstrap").Include(
                        "~/Content/libs/bootstrap/dist/js/bootstrap.min.js"));
            /* bootstrap - daterangepicker */
            bundles.Add(new ScriptBundle("~/Script/DateRangePicker").Include(
                       "~/Content/js/moment/moment.min.js",
                       "~/Content/js/datepicker/daterangepicker.js"));
            /* FastClick */
            bundles.Add(new ScriptBundle("~/Script/FastClick").Include(
                        "~/Content/libs/fastclick/lib/fastclick.js"));
            /* NProgress */
            bundles.Add(new ScriptBundle("~/Script/NProgress").Include(
                        "~/Content/libs/nprogress/nprogress.js"));
            /* iCheck */
            bundles.Add(new ScriptBundle("~/Script/iCheck").Include(
                        "~/Content/libs/iCheck/icheck.min.js"));
            /* Switchery */
            bundles.Add(new ScriptBundle("~/Script/Switchery").Include(
                        "~/Content/libs/switchery/dist/switchery.min.js"));
            /* Select2 */
            bundles.Add(new ScriptBundle("~/Script/Select2").Include(
                        "~/Content/libs/select2/dist/js/select2.full.min.js",
                        "~/Content/js/select2/select2.spanish.js"));
            /* Parsley */
            bundles.Add(new ScriptBundle("~/Script/Parsley").Include(
                        "~/Content/libs/parsleyjs/dist/parsley.min.js"));
            /* DataTables*/
            bundles.Add(new ScriptBundle("~/Script/DataTables").Include(
                "~/Content/libs/datatables.net/js/jquery.dataTables.min.js",
                "~/Content/libs/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/Content/js/datatables/es.js"
                ));            
            /* DataTablesButtons*/
            bundles.Add(new ScriptBundle("~/Script/DT-Buttons").Include(
                "~/Content/libs/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/Content/libs/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/Content/libs/datatables.net-buttons/js/buttons.flash.min.js",
                "~/Content/libs/datatables.net-buttons/js/buttons.html5.min.js",
                "~/Content/libs/datatables.net-buttons/js/buttons.print.min.js"                
                ));
            /* DataTablesInit*/
            bundles.Add(new ScriptBundle("~/Script/DT-Init").Include(
                "~/Content/js/datatables/init.js"
                ));
            /* Bootstrap DateRangePicker */
            bundles.Add(new ScriptBundle("~/Script/DateRangePicker").Include(
                "~/Content/js/datepicker/daterangepicker.js"));
            /* Custom Theme Scripts */
            bundles.Add(new ScriptBundle("~/Script/Custom").Include(
                "~/Content/js/custom.js"));
        }
    }
}
