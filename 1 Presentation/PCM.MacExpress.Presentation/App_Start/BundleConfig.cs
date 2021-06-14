using System.Web;
using System.Web.Optimization;

namespace PCM.MacExpress.Presentation
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Master/jquery-{version}.js",
                        "~/Scripts/jquery.maskedinput.min.js",
                        "~/Scripts/jquery.filter_input.js",
                        "~/Scripts/jquery.validationEngine.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/Negocio/utilitario.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Master/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Master/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Master/bootstrap.js",
                      "~/Scripts/Master/bootstrap-table.1.10.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/site.css",
                      "~/Content/bootstrap-table.1.10.min.css",
                      "~/Content/bootstrap-datetimepicker.css"));
        }
    }
}
