using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicShopBackend.Entities;
using MusicShopBackend.Services;
using System.Text;

namespace MusicShopBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
          
            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.DisableDataAnnotationsValidation = true;

                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicShopBackend", Version = "v1" });
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("JWT:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWT:JWTkey")))


                };
            });

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MusicShopDB")));
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddScoped<IDestinationAddressService, DestinationAddressService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderProductService, OrderProductService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicShopBackend v1"));
            }



            app.UseCors(opt =>
            {
                opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
