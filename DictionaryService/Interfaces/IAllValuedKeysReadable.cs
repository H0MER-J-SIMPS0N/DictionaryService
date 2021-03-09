using System.Collections.Generic;

namespace DictionaryService.Interfaces
{
    public interface IAllValuedKeysReadable
    {
        IEnumerable<string> GetAllKeys();
    }
}
