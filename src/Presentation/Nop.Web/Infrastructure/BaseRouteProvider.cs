using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Localization;

namespace Nop.Web.Infrastructure
{
    public class BaseRouteProvider
    {
        protected string GetRouterPattern(IEndpointRouteBuilder endpointRouteBuilder, string seoCode = "")
        {
            if (DataSettingsManager.IsDatabaseInstalled())
            {
                using var scope = EngineContext.Current.Resolve<IServiceProvider>().CreateScope();
                var localizationSettings = scope.ServiceProvider.GetRequiredService<LocalizationSettings>();
                if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                {
                    var langservice = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                    var languages = langservice.GetAllLanguagesAsync().Result;
                    return "{language:lang=" + languages.FirstOrDefault().UniqueSeoCode + $"}}/{seoCode}";
                }
            }
            return seoCode;
        }
    }
}
