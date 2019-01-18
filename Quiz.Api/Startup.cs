using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizData.Models;

namespace QuizApi
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;

        private IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var conString = Configuration["ConnectionStrings:DefaultConnection"];
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conString));
            
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IAnswerTypeRepository, AnswerTypeRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IQuizThemeRepository, QuizThemeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}