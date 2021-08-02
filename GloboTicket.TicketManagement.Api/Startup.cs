using GloboTicket.TicketManagement.Application;
using GloboTicket.TicketManagement.Instrastructure;
using GloboTicket.TicketManagement.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloboTicket.TicketManagement.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddPersistenceServices(Configuration);
            services.AddControllers();

            services.AddCors(option =>
            {
                option.AddPolicy("Open",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            //services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Open");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
