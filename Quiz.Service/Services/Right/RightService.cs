using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RightService : IRightService
    {
        #region properties

        private readonly IRepository<Right> _rightRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public RightService(IRepository<Right> rightRepository, IMemoryCache memoryCache)
        {
            _rightRepository = rightRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Right> GetAllRights()
        {
            if (_memoryCache.TryGetValue(RightDefaults.RightAllCacheKey, out List<Right> rights)) 
                return rights.ToList();
                
            rights = _rightRepository.Table.ToList();
            _memoryCache.Set(RightDefaults.RightAllCacheKey, rights);

            return rights.ToList();
        }

        public Right GetRightByID(int rightID)
        {
            if (_memoryCache.TryGetValue(RightDefaults.RightByIdCacheKey, out Right right)) 
                return right;
            
            right = _rightRepository.GetById(rightID);
            _memoryCache.Set(RightDefaults.RightByIdCacheKey, right);

            return right;
        }

        public void UpdateRight(Right right)
        {
            _rightRepository.Update(right);
        }

        public void AddRight(Right right)
        {
            _rightRepository.Insert(right);
        }

        public void DeleteRight(int rightID)
        {
            _rightRepository.Delete(rightID);
        }
        
        #endregion
    }
}