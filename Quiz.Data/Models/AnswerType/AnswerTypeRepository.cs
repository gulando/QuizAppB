using System.Linq;
using QuizData.Models;


namespace QuizData.Models
{
    public class AnswerTypeRepository : IAnswerTypeRepository
    {
        private readonly ApplicationDbContext _context;
        
        public AnswerTypeRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<AnswerType> AnswerTypes => _context.AnswerTypes;

        public void SaveAnswerType(AnswerType answerType)
        {
            if (answerType.AnswerTypeID == 0)
            {
                _context.AnswerTypes.Add(answerType);
            }
            else
            {
                AnswerType dbEntry = _context.AnswerTypes.FirstOrDefault(p => p.AnswerTypeID == answerType.AnswerTypeID);

                if (dbEntry != null)
                {
                    dbEntry.AnswerTypeName = answerType.AnswerTypeName;
                    dbEntry.QuizTD = answerType.QuizTD;
                }
            }
            _context.SaveChanges();
        }

        public AnswerType DeleteAnswerType(int answerTypeID)
        {
            var dbEntry = _context.AnswerTypes.FirstOrDefault(p => p.AnswerTypeID == answerTypeID);

            if (dbEntry != null)
            {
                _context.AnswerTypes.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }

        public AnswerType GetAnswerTypeByID(int answerTypeID)
        {
            return _context.AnswerTypes.Find(answerTypeID);
        }
    }
}