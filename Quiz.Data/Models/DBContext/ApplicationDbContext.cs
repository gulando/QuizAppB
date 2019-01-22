using Microsoft.EntityFrameworkCore;


namespace QuizData.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizTheme>().HasDiscriminator<string>("QuizThemeName");
        }
        
        public DbSet<Quiz> Quizes { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<QuizTheme> QuizThemes { get; set; }
        
        public DbSet<QuestionType> QuestionTypes { get; set; }
        
        public DbSet<AnswerType> AnswerTypes { get; set; }
        
        public DbSet<QuizThemeSummary> QuizThemeSummaries {get; set; }
    }
}