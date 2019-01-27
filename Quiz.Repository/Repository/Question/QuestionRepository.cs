using System.Collections.Generic;
using QuizData;
using System.Linq;


namespace QuizRepository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<Question> Questions => GetObjList();
        
        public Question GetQuestionByID(int questionID)
        {
            return GetObjByID(questionID);
        }

        public void AddQuestion(Question question)
        {
            AddObj(question);
        }

        public void UpdateQuestion(Question question)
        {
            UpdateObj(question);
        }

        public Question DeleteQuestion(int questionID)
        {
            return DeleteObj(questionID);
        }
        
        public List<QuestionSummary> GetQuestionSummary()
        {
            var result = (from questions in dbContext.Questions
                join quizes in dbContext.Quizes on questions.QuizID equals quizes.ID
                join quizThemes in dbContext.QuizThemes on quizes.ID equals quizThemes.QuizID
                join answerTypes in dbContext.AnswerTypes on questions.AnswerTypeID equals answerTypes.ID
                join questionTypes in dbContext.QuestionTypes on questions.QuestionTypeID equals questionTypes.ID
                select new QuestionSummary 
                {
                    ID = questions.ID,
                    QuizID = quizes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName
                }).ToList();

            return result;     
        }
    }
}