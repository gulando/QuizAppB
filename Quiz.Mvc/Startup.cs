using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizData;
using QuizRepository;
using QuizService;


namespace QuizMvc
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
            
            #region database
            
            var conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conString));
            
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
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");});
            #endregion
            
            #region authorization part
            
            #endregion
            
            #region repositories
            
            //add repositories
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IAnswerTypeRepository, AnswerTypeRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddTransient<IQuizRepository, QuizRepository.QuizRepository>();
            services.AddTransient<IQuizThemeRepository, QuizThemeRepository>();
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRightRepository, RightRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IUserRightRepository, UserRightRepository>();
            services.AddTransient<IRoleRightRepository, RoleRightRepository>();
            
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
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigureExceptionHandler(logger);
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }*/
            
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

        }
        
    }
}