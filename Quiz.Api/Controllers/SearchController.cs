using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace QuizApi.Controllers
{
    public class SearchController : Controller
    {
        #region properties
        
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        
        public SearchController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        #endregion
        
        public JsonResult Index()
        {
            return null;
        }
    }
}