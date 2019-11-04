using System.Web;
using System.Web.Optimization;

namespace LateOS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/bootstrap/animate.css",
                "~/Content/bootstrap/style.css",
                "~/Content/toastr/toastr.min.css",
                "~/Content/bootstrap/datatables.min.css",
                "~/Content/slick/slick-theme.css",
                "~/Content/slick/slick.css"));

            //font-awesome
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                "~/Content/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform()));

            //jquery.css
            bundles.Add(new StyleBundle("~/Content/gritter").Include(
                "~/Content/gritter/jquery.gritter.css"));

            //jquery
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/scripts/jquery/jquery-2.1.1.js",
                "~/scripts/jquery-ui/jquery-ui.min.js",
                "~/scripts/jquery/datatables.min.js"));

            //bootstrap
            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                "~/scripts/bootstrap/bootstrap.min.js"));

            //metisMenu
            bundles.Add(new ScriptBundle("~/scripts/metisMenu").Include(
                "~/scripts/metisMenu/jquery.metisMenu.js"));

            //slimscroll
            bundles.Add(new ScriptBundle("~/scripts/slimscroll").Include(
                "~/scripts/slimscroll/jquery.slimscroll.min.js"));

            //inspinia
            bundles.Add(new ScriptBundle("~/scripts/inspinia").Include(
                "~/scripts/inspinia.js",
                "~/scripts/pace/pace.min.js",
                "~/scripts/slick.min.js"));

            //más
            bundles.Add(new ScriptBundle("~/scripts/mas").Include(
                "~/scripts/jasny/jasny-bootstrap.min.js",
                "~/scripts/dropzone/dropzone.js",
                "~/scripts/codemirror/codemirror.js",
                "~/scripts/dropzone/xml/xml.js"));

            //adicionales
            bundles.Add(new StyleBundle("~/Content/mas").Include(
                "~/Content/codemirror/codemirror.css",
                "~/Content/dropzone/basic.css",
                "~/Content/dropzone/dropzone.css",
                "~/Content/jasny/jasny-bootstrap.min.css"));
        }
    }
}