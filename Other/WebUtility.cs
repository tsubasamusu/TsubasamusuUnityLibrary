#pragma warning disable CS8603
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TSUBASAMUSU.Other
{
    public static class WebUtility
    {
        public static Dictionary<string, string> GetQueryParameters()
        {
            string url = Application.absoluteURL;

            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("Failed to get the current URL.");

                return null;
            }

            Uri uri = new Uri(url);

            string queryText = uri.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);

            if (string.IsNullOrEmpty(queryText))
            {
                Debug.LogWarning("Failed to get the query text from \"" + url + "\".");

                return null;
            }

            List<string> queryTexts = queryText.Contains("&") ? queryText.Split("&").ToList() : new List<string>() { queryText };

            Dictionary<string, string> queryParameters = new Dictionary<string, string>();

            foreach (string query in queryTexts)
            {
                string key = query.Split("=")[0];

                string value = query.Split("=")[1];

                queryParameters.Add(key, value);
            }

            return queryParameters;
        }
    }
}