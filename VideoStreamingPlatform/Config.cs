using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VideoStreamingPlatform
{
    public static class Config
    {
        // In-memory API Scopes
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API #1")
            };

        // In-memory Clients
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1" }
                }
            };
    }
}
