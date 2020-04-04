using System.Web;
using System.Web.Optimization;

namespace AppProj.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/utopia").Include(
                        "~/Scripts/js/utopia.js",
                //"~/Scripts/js/chosen.jquery.js",
                //"~/Scripts/js/jquery.cleditor.js",
                //"~/Scripts/js/custom.js",
                //"~/Scripts/js/alerts.js",
                //"~/color/javascripts/SCF.ui.js",
                //"~/Scripts/js/utopia-ui.js",
                //"~/Scripts/js/mouse.js",
                //"~/Scripts/js/slider.js",
                //"~/Scripts/js/bootstrap-datepicker.js",
                //"~/Scripts/js/tables.js",
                //"~/Scripts/js/pagescroller.js",
                //"~/color/javascripts/SCF.ui/commutator.js",
                //"~/color/javascripts/SCF.ui/starbar.js",
                //"~/color/javascripts/SCF.ui/checkbox.js",
                //"~/color/javascripts/SCF.ui/radio.js",
                //"~/Scripts/js/utopia-growl.js",
                //"~/Scripts/js/bootstrap-colorpicker.js",
                //"~/Scripts/js/smooth-page-scrol.js",
                //"~/Scripts/js/smooth-page-scrol.js",
                //"~/Scripts/js/jquery.knob.js",                                        
                //"~/Scripts/js/jquery.hoverIntent.js",
                //"~/Scripts/js/jquery.easing.1.3.js",
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/bootstrap-datatable.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/custom.main.js",
                        "~/Scripts/custom.imagegrid.js",
                        "~/Scripts/custom.datatable.js",
                        "~/Scripts/typeahead.bundle.js",
                // "~/Scripts/js/jquery.validationEngine.js",
                //"~/Scripts/js/jquery.validationEngine-en.js",
                //"~/Scripts/js/jquery.wookmark.js",
                "~/Scripts/js/jquery.cookie.js",                       
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/jquery.jqprint.0.3.js",
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/js/header.js",
                        "~/Scripts/js/sidebar.js",
                        "~/Scripts/jquery-barcode.js",
                        "~/Scripts/printThis.js",
                        "~/Scripts/exportExcell.js"
                        ));

            /*bundles.Add(new ScriptBundle("~/bundles/jquery-validation").Include(                        
                        "~/Scripts/jquery.validate*"
                        ));            */

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                        "~/Content/css/utopia-white.css",
                        "~/Content/css/utopia-responsive.css",
                //"~/Content/css/ie.css",
                //"~/Content/css/jquery.cleditor.css",
                //"~/Content/css/alerts.css",
                //"~/Content/css/colorpicker.css",
                //"~/Content/color/bundle.css",
                //"~/Content/css/chosen.css",
                //"~/Content/css/ui-lightness/jquery-ui.css",
                //"~/Content/css/datepicker.css",
                //"~/Content/css/utopia-growl.css",
                //"~/Content/css/page-scrller.css",
                //"~/Content/css/validationEngine.jquery.css",
                        "~/Content/custom.css",
                        "~/Content/typeahead.css",
                //"~/Content/css/gallery/wookmark.css",
                //"~/Content/css/gallery/modal.css",
                //"~/Content/bootstrap-datatable.css",
                        "~/Content/bootstrap-datepicker.css"
                        ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}