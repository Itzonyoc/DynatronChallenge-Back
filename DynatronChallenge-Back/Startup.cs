using Autofac;
using DynatronChallenge.DAC;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace DynatronChallenge_Back
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                          p => p.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });

            services.AddSwaggerGen(options =>
            {
                //var security = new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", new string[] { }},
                //};
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DynatronChallenge", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddSessionStateTempDataProvider();

            services.AddSession();
            services.AddHttpContextAccessor();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //var sqlConfig = new SqlConfig();
            //Configuration.GetSection("ConnectionStrings").Bind(sqlConfig);

            //var jwtConfig = new JWTConfig();
            //Configuration.GetSection("JwtIssuerSettings").Bind(jwtConfig);

            //builder.RegisterInstance(sqlConfig).As<SqlConfig>();
            //builder.RegisterInstance(jwtConfig).As<JWTConfig>();

            builder.RegisterType<CustomerDAC>().As<ICustomerDAC>();
            //builder.RegisterType<MenuDal>().As<IMenuDal>();
            //builder.RegisterType<EmployeeDal>().As<IEmployeeDal>();
            //builder.RegisterType<SettingsDal>().As<ISettingsDal>();
            //builder.RegisterType<CatalogDal>().As<ICatalogDal>();
            //builder.RegisterType<AttendanceDal>().As<IAttendanceDal>();

            //builder.RegisterType<RoleStoreDal>().As<IRoleStore<ApplicationRoleModel>>();
            //builder.RegisterType<UserStoreDal>().As<IUserStore<ApplicationUserModel>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseAuthentication();
            //app.UseMiddleware<JwtMiddleware>();

            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "DynatronChallenge");
            });

            app.Use((context, next) =>
            {
                var connectionString = context.Session.GetString("ConnectionString");
                return next();
            });
        }
    }
}
