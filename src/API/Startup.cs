using System.Data.Common;
using AutoMapper;
using Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Domain;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            }));

            services.AddAutoMapper(typeof(APIMappingProfile), typeof(DomainMappingProfile));

            services.AddSingleton<DbConnectionStringBuilder>(new NpgsqlConnectionStringBuilder
                {
                    Host = "postgres",
                    Username = Configuration["POSTGRES_USER"],
                    Password = Configuration["POSTGRES_PASSWORD"],
                    Database = Configuration["POSTGRES_DB"]
                })
                .AddDbContext<AppDataContext>((serviceProvider, contextOptionsBuilder) => contextOptionsBuilder
                    .UseNpgsql(serviceProvider.GetRequiredService<DbConnectionStringBuilder>()
                        .ConnectionString));

            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<ICourseEntitiesRepository, CourseEntitiesRepository>();
            services.AddScoped<IStudentEntitiesRepository, StudentEntitiesRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger().UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
    }
}