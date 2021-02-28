using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api.swagger.Versioning
{
    public class ApiVersionHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var version = context?.DocumentName?.ToString()?.Replace("v", "");
            if (!string.IsNullOrEmpty(version))
            {
                //operation.Parameters.Add(new OpenApiParameter()
                //{
                    
                //    Name = "api",
                //    In = ParameterLocation.Header,
                //    Required = true,
                //    AllowEmptyValue = false,
                //    Content = new Dictionary<string, OpenApiMediaType>() { "api", }
                    

                //});



            }
        }
    }

    public  class ApiVersionHeaderSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var version = context?.DocumentName?.ToString()?.Replace("v", "");
            if (!string.IsNullOrEmpty(version))
            {


            }
        }
    }

    public class ApiVersionHeaderDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var entry in swaggerDoc.Paths.Values)
            {
                entry.Parameters ??= new List<OpenApiParameter>();
                entry.Parameters.Add(new OpenApiParameter()
                {
                    Name = "api",
                    In = ParameterLocation.Header,
                    Required = true,
                    AllowEmptyValue = false,
                    
                });

            }
        }
    }

    public class GroupingByNamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split(".").Last().ToLower();
            if (!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}