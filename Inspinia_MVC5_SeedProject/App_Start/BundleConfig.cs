using System.Web;
using System.Web.Optimization;

namespace Inspinia_MVC5_SeedProject
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {



            // Vendor scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/dataTables/jquery.dataTables.min.js",
                      "~/Scripts/dataTables/dataTables.bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/app/inspinia.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables")
                .Include("~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Scripts/DataTables")
                      .Include(
                            "~/Scripts/DataTables/jquery.dataTables.min.js",
                            "~/Scripts/DataTables/dataTables.responsive.min.js",
                            "~/Scripts/DataTables/dataTables.bootstrap.min.js"
                ));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            //Date picker
            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                        "~/Content/themes/base/jquery-ui.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/datepicker").Include(
                "~/Scripts/jquery-ui-1.12.1.min.js"
                ));

        }
    }
}
