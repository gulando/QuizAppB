using Microsoft.EntityFrameworkCore;
using QuizData;


namespace QuizRepository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
        public DbSet<Quiz> Quizes { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<QuizTheme> QuizThemes { get; set; }
        
        public DbSet<QuestionType> QuestionTypes { get; set; }
        
        public DbSet<AnswerType> AnswerTypes { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<UserRight> UserRights { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Right> Rights { get; set; }
        
        public DbSet<RoleRight> RoleRights { get; set; }
                
    }
}