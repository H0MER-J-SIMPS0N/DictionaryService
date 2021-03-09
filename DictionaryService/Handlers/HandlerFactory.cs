using System;

namespace DictionaryService.Handlers
{
    public class HandlerFactory
    {
        public T CreateHandler<T>(params object[] args) where T: BaseHandler
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
