using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddMvc();
            var conString = Configuration["ConnectionStrings:DefaultConnection"];
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conString));
            
            //add repository
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IAnswerTypeRepository, AnswerTypeRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddTransient<IQuizRepository, QuizRepository.QuizRepository>();
            services.AddTransient<IQuizThemeRepository, QuizThemeRepository>();
            
            //add services
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IAnswerTypeService, AnswerTypeService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionTypeService, QuestionTypeService>();
            services.AddTransient<IQuizService, QuizService.QuizService>();
            services.AddTransient<IQuizThemeService, QuizThemeService>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}