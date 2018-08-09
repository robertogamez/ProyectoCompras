using System.Web.Optimization;

namespace Pedidos.WebApp.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* Scripts*/
            bundles.Add(new ScriptBundle("~/bundle/products")
                .Include("~/scripts/app/products/products-validation.js", "~/scripts/app/products/products.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}