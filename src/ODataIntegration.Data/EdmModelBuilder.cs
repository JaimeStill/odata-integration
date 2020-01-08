using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using ODataIntegration.Data.Entities;

namespace ODataIntegration.Data
{
    public static class EdmModelBuilder
    {
        private static IEdmModel edmModel;

        public static IEdmModel GetEdmModel()
        {
            if (edmModel == null)
            {
                var builder = new ODataConventionModelBuilder();
                builder.EnableLowerCamelCase();

                builder.EntitySet<Product>("Products");

                edmModel = builder.GetEdmModel();
            }

            return edmModel;
        }
    }
}