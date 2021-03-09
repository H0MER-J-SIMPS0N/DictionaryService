using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DictionaryService.Handlers
{
    public class GetAllKeysHandler : BaseHandler
    {
        public GetAllKeysHandler(HttpContext context, IStorageService service) : base(context, service) { }

        public override async Task HandleAsync()
        {
            _context.Response.ContentType = "text/html; charset=utf-8";
            await _context.Response.WriteAsync($"{string.Join(Environment.NewLine, _service.GetAllKeys())}");
        }
    }
}
