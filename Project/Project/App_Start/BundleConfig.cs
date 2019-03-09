using System.Web;
using System.Web.Optimization;

namespace Project
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //phan dinh kiên - Update 26/02/2019
            //Get Css cho layout 
            bundles.Add(new StyleBundle("~/Content/Addmin").Include(
                      "~/Assets/Admin/css/bootstrap.css",
                      "~/Assets/Admin/font-awesome/css/font-awesome.min.css",
                      "~/Assets/Admin/css/style.css",
                      "~/Content/PagedList.css"

            ));

            //phan dinh kiên - Update 26/02/2019
            //Get js Cho layout 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/Admin/js/modernizr.min.js",
                      "~/Assets/Admin/js/jquery-3.3.1.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                      "~/Assets/Admin/js/moment.min.js",
                      "~/Assets/Admin/js/popper.min.js",
                      "~/Assets/Admin/js/detect.js",
                      "~/Assets/Admin/js/fastclick.js",
                      "~/Assets/Admin/js/jquery.blockUI.js",
                      "~/Assets/Admin/js/jquery.nicescroll.js",
                      "~/Assets/Admin/js/pikeadmin.js",
                      "~/Assets/Admin/js/modernizr.min.js",
                      "~/Assets/Admin/plugins/waypoints/lib/jquery.waypoints.min.js",
                      "~/Assets/Admin/plugins/counterup/jquery.counterup.min.js"
                   

            ));

            bundles.Add(new ScriptBundle("~/bundles/Script").Include(
                      "~/Assets/Admin/plugins/waypoints/lib/jquery.waypoints.min.js",
                      "~/Assets/Admin/plugins/counterup/jquery.counterup.min.js",
                      "~/Assets/Admin/js/Project/Script.js"

            ));


            bundles.Add(new StyleBundle("~/Content/css/Login").Include(
                   "~/Assets/Backend/vendor/bootstrap/css/bootstrap.min.css",
                   "~/Assets/Backend/vendor/fontawesome-free/css/all.min.css",
                   "~/Assets/Backend/css/Login.css"
            ));


        }
    }
}
