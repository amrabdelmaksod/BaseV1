﻿using Microsoft.Extensions.Localization;

namespace Hedaya.Dashboard.Models
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer();
        }
    }
}
