using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryService.Handlers
{
    public class GetValueHandler: BaseHandler
    {
        public GetValueHandler(HttpContext context, IStorageService service) : base(context, service) { }

        public override async Task HandleAsync()
        {
            var key = _context.GetRouteData().Values["key"].ToString();
            _context.Response.ContentType = "text/html; charset=utf-8";
            try
            {
                var value = _service.Get(key);
                await _context.Response.WriteAsync($"{value}");
            }
            catch (KeyNotFoundException)
            {
                _context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
