using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Infrastructure;
using Assignment.Infrastructure.Mapper;
using Assignment.Infrastructure.Options;
using Assignment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assignment.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var DbConn = DatabaseConnectionOptions.GetConnectionString(Configuration);

            services.AddControllers().AddNewtonsoftJson();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins",
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod();
                                  });
            });

            services.AddAuthentication()
            .AddCookie(cfg => cfg.SlidingExpiration = true)
            .AddJwtBearer(options =>
            {
                JwtOptions JwtOptions = Configuration.GetSection("Jwt").Get<JwtOptions>();
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidAudience = JwtOptions.Issuer,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Key)),
                };
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });


            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter());
            }).ConfigureApiBehaviorOptions(opt=>
            {
               opt.InvalidModelStateResponseFactory =(context => new Response<string>(context.ModelState,400));
            });

            services.AddAutoMapper(typeof(AutoMapping));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(DbConn, builder => builder.MigrationsAssembly("Assignment.Data").EnableRetryOnFailure(2)));

            services.AddScoped<IUnitOfWork, UnitOfWork>().Cast<IUnitOfWork>();

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API - V1", Version = "v1" });
                a.SwaggerDoc("v2", new OpenApiInfo { Title = "Products API - V2", Version = "v2" });

                a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                a.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                        },Array.Empty<string>()
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                a.IncludeXmlComments(xmlPath);

                var modelXmlPath = Path.Combine(AppContext.BaseDirectory, "Assignment.Infrastructure.xml");
                a.IncludeXmlComments(modelXmlPath);

                a.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
                    version = version.Replace("v", "");
                    return versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
                });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = Configuration.GetSection("Swagger").Get<SwaggerOptions>();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                foreach (var route in swaggerOptions.UiEndpoints)
                    options.SwaggerEndpoint(route.Route, route.Description);
            });

            //app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors("AllowedOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
