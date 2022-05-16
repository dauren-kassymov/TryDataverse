﻿using Azure.Core;
using Azure.Identity;
using System.Net.Http.Headers;
using System.Threading;

namespace Dnka.TryDataverse.Core.Service
{
    public static class AuthProvider
    {
        public static AuthenticationHeaderValue GetAuthHeader(
            string tenantId,
            string clientId,
            string clientSecret,
            string scope)
        {
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var tokenRequestContext = new TokenRequestContext(new[] { scope + ".default" });
            var accessToken = clientSecretCredential.GetToken(tokenRequestContext, CancellationToken.None);
            return new AuthenticationHeaderValue("Bearer", accessToken.Token);
        }
    }
}
