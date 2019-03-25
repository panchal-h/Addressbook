using System.Web;
using System.Web.Optimization;

namespace RealmDigital.AddressBook
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/Scripts/RealmDigitalScripts").Include(
               "~/Scripts/jquery.js",
               "~/Scripts/jquery.validate.js",
               "~/Scripts/jquery.validate.unobtrusive.js",
               "~/Scripts/popper.min.js",
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/moment.js",
               "~/Scripts/bootstrap-datetimepicker.min.js",
               "~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/Content/RealmDigitalStyles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/style.css",
                "~/Content/dev-style.css"));
        }
    }
}
