using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizRepository;
using QuizService;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Helpers;


namespace QuizApi
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            #region mvc
            
            services.AddMvc();
            
            #endregion
            
            #region cash
            
            services.AddMemoryCache();
            
            #endregion
            
            #region database
            
            var conString = Configuration["ConnectionStrings:DefaultConnection"];            
            services.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));
            services.AddScoped<IDbContext>(provider => provider.GetService<QuizDBContext>());
            
            #endregion
            
            #region mapping

            services.AddAutoMapper();
            
            //manual add mapping
           /* var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);*/
            
            #endregion
            
            #region authentication part
            
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = userService.GetUserByID(userId);
                            if (user == null)
                            {
                                // return unauthorized if user no longer exists
                                context.Fail("Unauthorized");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            #endregion
            
            #region repositories
            
            //add repository
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryAsync<>),typeof(RepositoryAsync<>));
            
            #endregion
            
            #region services
            
            //add services
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IAnswerTypeService, AnswerTypeService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionTypeService, QuestionTypeService>();
            services.AddTransient<IQuizService, QuizService.QuizService>();
            services.AddTransient<IQuizThemeService, QuizThemeService>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRightService, RightService>();
            services.AddTransient<IRoleService, RoleService>();
            
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUserRightService, UserRightService>();
            services.AddTransient<IRoleRightService, RoleRightService>();
            
            services.AddTransient<ILogService, LogService>();
            
            #endregion
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogService logger)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                
                //Custom Exception Handling.
                app.ConfigureExceptionHandler(logger);
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
        
    }
}