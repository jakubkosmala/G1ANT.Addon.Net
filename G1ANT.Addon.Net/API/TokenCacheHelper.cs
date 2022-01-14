//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------
using G1ANT.Language;
using Microsoft.Identity.Client;
using System.IO;
using System.Security.Cryptography;

namespace G1ANT.Addon.Net.API
{
    public class TokenCacheHelper
    {

        /// <summary>
        /// Path to the token cache
        /// </summary>
        public string cacheFilePath = Path.Combine(AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName, "msalcache.txt");

        private readonly object fileLock = new object();

        public TokenCacheHelper(string cacheFolder)
        {
            cacheFilePath = Path.Combine(cacheFolder, "msalcache.txt");
        }

        public void EnableSerialization(ITokenCache tokenCache)
        {
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
        }

        protected void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (fileLock)
            {
                //args.TokenCache.Deserialize(File.Exists(CacheFilePath)
                //    ? File.ReadAllBytes(CacheFilePath)
                //    : null);

                // See: https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-net-token-cache-serialization
                args.TokenCache.DeserializeMsalV3(File.Exists(cacheFilePath)
                        ? ProtectedData.Unprotect(File.ReadAllBytes(cacheFilePath), null, DataProtectionScope.CurrentUser)
                        : null);
            }
        }

        protected void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                lock (fileLock)
                {
                    // reflect changes in the persistent store
                    //File.WriteAllBytes(CacheFilePath, args.TokenCache.Serialize());
                    // once the write operation takes place restore the HasStateChanged bit to false
                    //args.TokenCache.HasStateChanged = false;

                    // See: https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-net-token-cache-serialization
                    // reflect changes in the persistent store
                    File.WriteAllBytes(cacheFilePath, ProtectedData.Protect(args.TokenCache.SerializeMsalV3(), null, DataProtectionScope.CurrentUser));
                }
            }
        }
    }
}
