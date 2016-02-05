using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServerOnSameApp3.Auth;
using Serilog;

[assembly: OwinStartup(typeof(IdentityServerOnSameApp3.Startup))]

namespace IdentityServerOnSameApp3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", inner =>
            {
                var option = new IdentityServerOptions
                {
                    RequireSsl = false,
                    SiteName = "Same Server Identity Service",
                    EnableWelcomePage = false,
                    Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get())
                };

                inner.UseIdentityServer(option);
            });

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions()
            {
                Authority = "http://localhost:4560/identity",
                ValidationMode = ValidationMode.Both,
                RequiredScopes = new[] { "read", "api1" },
                DelayLoadMetadata = true
            });


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // change with your desired log level
                .WriteTo.Trace()
                //.WriteTo.File(@"C:\myPath.txt") // remember to assign proper writing privileges on the file
                .CreateLogger();

        }
    }
}
