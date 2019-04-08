using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizRepository;
using QuizService;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using QuizApi.Helpers;


namespace QuizApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration config)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = config;
        }
            
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
            services.AddScoped<IDbContext>(provider => provider.GetService<QuizDBContext>())
                .AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));

            var identityConString = Configuration["ConnectionStrings:IdentityConnection"];

            services.AddScoped<IIdentityDBContext>(provider => provider.GetService<QuizIdentityDBContext>())
                .AddDbContext<QuizIdentityDBContext>(options =>
                    options.UseSqlServer(identityConString, b => b.MigrationsAssembly("Quiz.Api")));

            //services.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));
            //.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));

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
            
            #region generic repository and service
            
            //add generic repository and service
            services.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
            services.AddScoped(typeof(IElasticSearchRepository<>), typeof(ElasticSearchRepository<>));
            
            services.AddScoped(typeof(IService<>),typeof(Service<>));
            services.AddScoped(typeof(ISearchService<>),typeof(SearchService<>));
            
            #endregion
            
            #region services
            
            //add services
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAnswerTypeService, AnswerTypeService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionTypeService, QuestionTypeService>();
            services.AddScoped<IQuizService, QuizService.QuizService>();
            services.AddScoped<IQuizThemeService, QuizThemeService>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRightService, RightService>();
            services.AddScoped<IRoleService, RoleService>();
            
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRightService, UserRightService>();
            services.AddScoped<IRoleRightService, RoleRightService>();
            
            
            services.AddSingleton<ILogService, LogService>();
            
            #endregion

            #region elasticsearch
            
            services.AddElasticSearch(Configuration);
            
            #endregion
            
        }

        public void Configure(IApplicationBuilder app, ILogService logger)
        {    
            //Custom Exception Handling.
            app.ConfigureExceptionHandler(logger);
            
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
        
    }
}