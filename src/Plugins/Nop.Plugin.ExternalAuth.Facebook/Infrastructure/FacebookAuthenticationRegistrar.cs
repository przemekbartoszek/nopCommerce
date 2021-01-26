using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Configuration;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Authentication.External;

namespace Nop.Plugin.ExternalAuth.Facebook.Infrastructure
{
    /// <summary>
    /// Represents registrar of Facebook authentication service
    /// </summary>
    public class FacebookAuthenticationRegistrar : IExternalAuthenticationRegistrar
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder">Authentication builder</param>
        public void Configure(AuthenticationBuilder builder)
        {
            builder.AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
            {
                //set credentials
                var settings = EngineContext.Current.Resolve<IRepository<Setting>>().GetAllAsync(x => x).Result;
                options.AppId = settings.FirstOrDefault(x => x.Name.StartsWith($"{nameof(FacebookExternalAuthSettings)}.{nameof(FacebookExternalAuthSettings.ClientKeyIdentifier)}", StringComparison.InvariantCultureIgnoreCase)).Value;
                options.AppSecret = settings.FirstOrDefault(x => x.Name.StartsWith($"{nameof(FacebookExternalAuthSettings)}.{nameof(FacebookExternalAuthSettings.ClientSecret)}", StringComparison.InvariantCultureIgnoreCase)).Value;
                
                //store access and refresh tokens for the further usage
                options.SaveTokens = true;

                //set custom events handlers
                options.Events = new OAuthEvents
                {
                    //in case of error, redirect the user to the specified URL
                    OnRemoteFailure = context =>
                    {
                        context.HandleResponse();

                        var errorUrl = context.Properties.GetString(FacebookAuthenticationDefaults.ErrorCallback);
                        context.Response.Redirect(errorUrl);

                        return Task.FromResult(0);
                    }
                };
            });
        }
    }
}