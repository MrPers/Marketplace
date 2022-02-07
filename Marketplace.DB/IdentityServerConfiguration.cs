using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Marketplace.DB
{
    public static class IdentityServerConfiguration
    {
        public const string ApiName = "Order";
        public const string SmartAppClientID = "client_angular_id";
        public const string SwaggerClientID = "swaggerui";

        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = SmartAppClientID, //Идентификатор клиента, инициировавшего запрос.
                RequireClientSecret = false, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                RequireConsent = false, //Указывает, требуется ли экран согласия
                RequirePkce = true, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                AllowOfflineAccess = true,//Определяет, может ли этот клиент запрашивать токены обновления
                //AccessTokenType = AccessTokenType.Reference, //Указывает, является ли токен доступа ссылочным токеном или автономным токеном JWT
                AccessTokenLifetime = 300, //Время жизни токена доступа в секундах(по умолчанию 3600 секунд / 1 час)
                //AuthorizationCodeLifetime = 5, //Время жизни кода авторизации в секундах (по умолчанию 300 секунд / 5 минут)
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // клиент может отправить имя пользователя и пароль в службу маркеров и получить маркер доступа, который представляет этого пользователя
                //AllowedCorsOrigins = { "http://localhost:4200" },
                //RedirectUris = { "http://localhost:4200/auth-callback", "http://localhost:4200/refresh" },
                //PostLogoutRedirectUris = { "http://localhost:4200/" },
                AllowedCorsOrigins = { "http://localhost:5001" },
                RedirectUris = { "http://localhost:5001/auth-callback", "http://localhost:5001/refresh" },
                PostLogoutRedirectUris = { "http://localhost:5001/" },
                AllowedScopes =
                {
                    ApiName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowAccessTokensViaBrowser = true, //Указывает, разрешено ли этому клиенту получать токены доступа через браузер
                IdentityTokenLifetime = 3600, //через сколько секунд токен обновлен(по умолчанию 300 секунд / 5 минут)
                AlwaysIncludeUserClaimsInIdToken = true, //При запросе токена идентификатора и токена доступа утверждения пользователя всегда должны добавляться к токену идентификатора вместо того, чтобы требовать от клиента использования конечной точки userinfo
                RefreshTokenUsage = TokenUsage.OneTimeOnly, //дескриптор токена обновления будет обновляться при обновлении токенов. Это значение по умолчанию.
                UpdateAccessTokenClaimsOnRefresh = true //Получает или задает значение, указывающее, следует ли обновлять маркер доступа (и его утверждения) при запросе маркера обновления.
            },

            new Client
            {
                ClientId = SwaggerClientID,
                ClientName = "Swagger UI",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowAccessTokensViaBrowser = true,
                RequireClientSecret = false,

                AllowedScopes = {
                    ApiName
                }
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> {
                new ApiResource(ApiName)
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        //public static IEnumerable<ApiScope> GetApiScopes() =>
        //    new List<ApiScope> {
        //        new ApiScope("Order")
        //    };
    }

}
