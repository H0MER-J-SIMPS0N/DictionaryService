using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryService.Handlers
{
    public class DeleteValueHandler: BaseHandler
    {
        public DeleteValueHandler(HttpContext context, IStorageService service) : base(context, service) { }

        public override async Task HandleAsync()
        {
            var key = _context.GetRouteData().Values["key"].ToString();
            _context.Response.ContentType = "text/html; charset=utf-8";
            try
            {
                _service.Delete(key);
                _context.Response.StatusCode = StatusCodes.Status410Gone;
                await _context.Response.WriteAsync($"{key}");
            }
            catch (KeyNotFoundException)
            {
                _context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
