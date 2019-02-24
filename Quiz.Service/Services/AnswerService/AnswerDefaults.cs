namespace QuizService
{
    public static class AnswerDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string AnswerAllCacheKey => "Quiz.answer.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string AnswerByIdCacheKey => "Quiz.answer.ByID";
    }
}