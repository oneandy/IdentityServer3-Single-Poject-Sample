using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityServerOnSameApp3.Auth
{
    static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "api1"
                },
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read data"
                },
            };
        }
    }

    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "bob",
                    Subject = "1"
                },
                new InMemoryUser
                {
                    Username = "alice",
                    Password = "alice",
                    Subject = "2"
                }
            };
        }
    }

    static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                // no human involved
                new Client
                {
                    ClientName = "Silicon-only Client",
                    ClientId = "silicon",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,

                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "api1"
                    }
                },

                // human is involved
                new Client
                {
                    ClientName = "Silicon on behalf of Carbon Client",
                    ClientId = "carbon",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,

                    Flow = Flows.AuthorizationCode,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "api1", "read"
                    },

                    RedirectUris = new List<string>
                    {
                        "https://www.getpostman.com/oauth2/callback"
                    }
                }
            };
        }
    }
}