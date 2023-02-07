


using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace BaseV1.Common.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _distributedCache;

        public JsonStringLocalizer(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public LocalizedString this[string name] => throw new NotImplementedException();

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}
