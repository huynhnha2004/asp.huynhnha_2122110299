using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASP_HuynhNha_2122110299
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Cấu hình các service (DI container)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Bật Swagger
            services.AddSwaggerGen();
        }

        // Cấu hình pipeline xử lý HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Bật Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger"; // Swagger UI sẽ hiển thị tại /swagger
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Redirect từ / về /swagger
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return System.Threading.Tasks.Task.CompletedTask;
                });
            });
        }
    }
}
