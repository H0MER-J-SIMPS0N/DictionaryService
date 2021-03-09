using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DictionaryService.Handlers
{
    public class BaseHandler
    {
        protected readonly HttpContext _context;
        protected readonly IStorageService _service;

        public BaseHandler(HttpContext context, IStorageService service)
        {
            _context = context;
            _service = service;
        }
        public virtual Task HandleAsync()
        {
            return null;
        }
    }
}
