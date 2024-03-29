using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaintStockStatusAPI.Data;
using PaintStockStatusAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintStockStatusAPI
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

            services.AddDbContext<DataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPaintInventoryService, PaintInventoryService>();
            //services.AddTransient<IOrderService, OrderService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                           .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins(
                      "https://paintcompanyui.onrender.com"
                    , "https://www.paintcompanyui.onrender.com"
                    , "https://gracewaychurch.xyz"
                    , "https://www.gracewaychurch.xyz"
                    , "http://localhost:3000"
                    , "https://localhost:3000"
                    , "http://localhost:3001"
                    , "https://localhost:3001"
                    )
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                //.AllowCredentials();
            }));
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"c:\PATH TO COMMON KEY RING FOLDER"))
                .SetApplicationName("SharedCookieApp");
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Domain = ".gracewaychurch.xyz";
                options.Cookie.Name = "..SharedCookie";
                options.Cookie.Path = "/";
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaintStockStatusAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaintStockStatusAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
