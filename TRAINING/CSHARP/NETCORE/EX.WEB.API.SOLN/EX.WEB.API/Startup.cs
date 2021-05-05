using AutoMapper;
using EX.Repository;
using EX.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

[assembly: ApiConventionType(typeof(DefaultApiConventions ))]
namespace EX.WEB.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            //.AddXmlDataContractSerializerFormatters() 
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext, context.ModelState);
                    problemDetails.Detail = "See the errors field for details";
                    problemDetails.Instance = context.HttpContext.Request.Path;
                    var actionExecutingContext = context as
                        Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutingContext?.ActionArguments.Count ==
                         context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://url/";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "One or more validation errors occured";
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more errors on input occured";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            services.AddHttpClient();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("LibraryOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Library API",
                        Version = "1.0.0",
                        Description = "Library API",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "tellek.liberty@gmail.com",
                            Name = "Tellek E. Liberty",
                            Url = new Uri("https://www.linkedin.com/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License"
                        }
                    });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath, true);
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        // Log the fault.
                        await context.Response.WriteAsync("An unexpected fault happened. Try Again later.");
                    });
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "EX WEB API");
            });

            app.UseRouting();

            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
