﻿using System.Web;
using System.Web.Optimization;

namespace TeamProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery-3.1.1.js"
                      //"~/Scripts/simpleCart.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css/admin").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/style_admin.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery-3.1.1.js",
                      "~/Scripts/modernizr-2.8.3.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundler/details").Include(
                    "~/Scripts/imagezoom.js",
                    "~/Scripts/jquery.flexslider.js",
                    "~/Scripts/jquery.elevatezoom.js"
                ));
            bundles.Add(new StyleBundle("~/Content/Details").Include(
                "~/Content/flexslider.css"
                ));
        }
    }
}
