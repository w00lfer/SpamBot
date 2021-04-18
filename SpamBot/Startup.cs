using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using SpamBotApi.Mappings;
using SpamBotApi.Models;
using SpamBotApi.Repositories;
using SpamBotApi.Repositories.Interfaces;
using SpamBotApi.Services;
using SpamBotApi.Services.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace SpamBot
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IReceiverRepository, ReceiverRepository>();

            services.AddHttpClient<IEmailService, EmailService>(opts =>
            {
                opts.BaseAddress = new Uri(Configuration.GetValue<string>("MailgunAddress"));
                var apiKey = Encoding.ASCII.GetBytes($"api:{Configuration.GetValue<string>("MailgunApiKey")}");
                opts.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(apiKey));
            });

            services.AddScoped<IReceiverService, ReceiverService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpamBot", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpamBot v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
