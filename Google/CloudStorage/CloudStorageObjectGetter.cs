#pragma warning disable CS8603
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using TSUBASAMUSU.UnityWebRequestAwaiter;
using UnityEngine;
using UnityEngine.Networking;

namespace TSUBASAMUSU.Google.CloudStorage
{
    public static class CloudStorageObjectGetter
    {
        /// <summary>
        /// Get the texture in Google Cloud Storage.
        /// </summary>
        public static async Task<Texture2D> GetTextureFromCloudStorageAsync(string googleCloudJwt, string bucketName, string objectName, int textureWidth = 512, int textureHeight = 512)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            nameValueCollection.Add("alt", "media");

            string apiUrl = "https://www.googleapis.com/storage/v1/b/" + bucketName + "/o/" + objectName + "?" + nameValueCollection.ToString();

            using UnityWebRequest unityWebRequest = new UnityWebRequest(apiUrl, "GET")
            {
                downloadHandler = new DownloadHandlerTexture()
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Authorization", "Bearer " + googleCloudJwt},
                {"Content-type", "application/x-www-form-urlencoded"}
            };

            foreach (KeyValuePair<string, string> header in headers) unityWebRequest.SetRequestHeader(header.Key, header.Value);

            try
            {
                await unityWebRequest.SendWebRequest();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return null;
            }

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(unityWebRequest.result.ToString());

                Debug.LogError(unityWebRequest.downloadHandler.text);

                return null;
            }

            return DownloadHandlerTexture.GetContent(unityWebRequest);
        }
    }
}