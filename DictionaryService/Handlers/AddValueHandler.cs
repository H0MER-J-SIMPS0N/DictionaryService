using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryService.Handlers
{
    public class AddValueHandler : BaseHandler
    {
        public AddValueHandler(HttpContext context, IStorageService service) : base(context, service) { }
        public override async Task HandleAsync()
        {
            var key = _context.GetRouteData().Values["key"].ToString();
            var postValue = string.Empty;
            var req = _context.Request;
            req.EnableBuffering();
            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                postValue = await reader.ReadToEndAsync();
            }
            req.Body.Position = 0;
            _context.Response.ContentType = "text/html; charset=utf-8";
            if (!string.IsNullOrEmpty(postValue))
            {
                try
                {
                    _service.Get(key);
                }
                catch
                {
                    _context.Response.StatusCode = StatusCodes.Status201Created;
                }
                _service.Add(key, postValue);
                await _context.Response.WriteAsync($"{postValue}");
            }
        }
    }
}
