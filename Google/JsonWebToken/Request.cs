#pragma warning disable CS8618
using System;

namespace TSUBASAMUSU.Google.JsonWebToken
{
    [Serializable]
    public class Request
    {
        public string privateKey;

        public string serviceAccountEmailAddress;

        public string scopes;
    }
}