using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace api.swagger.Versioning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });
            services.AddSwaggerGen(options =>
            {

          

                options.ResolveConflictingActions(o => o.FirstOrDefault());
                
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "api.swagger.Versioning", Version = "v1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "api.swagger.Versioning", Version = "v2" });
            });

        

            services.AddApiVersioning(options =>
            {

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2,0);
                options.ReportApiVersions = true;
                options.UseApiBehavior = true;

                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api"),
                    new HeaderApiVersionReader("api"),
                    new MediaTypeApiVersionReader("api")
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomMiddleware();
            if (env.IsDevelopment())
            { 

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(swaggerUiOptions =>
                {
                    swaggerUiOptions.HeadContent = "Swagger API demo";


                    var fn =  "(request) => { var swaggerSelect = document.querySelector('#swagger-ui .swagger-container .topbar select'); if(swaggerSelect) { var selected = swaggerSelect.selectedOptions; if( selected ) { var version = selected[0].text.split('|')[1]; request.headers['api']=version; }  }; return request; }";


                    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "api.swagger.Versioning v1|1.0");
                    swaggerUiOptions.SwaggerEndpoint("/swagger/v2/swagger.json", "api.swagger.Versioning v2|2.0");

                    swaggerUiOptions.InjectJavascript("/js/inject.js");
                    swaggerUiOptions.UseRequestInterceptor(fn);
                });
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
