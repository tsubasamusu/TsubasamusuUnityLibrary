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
        /// Get the texture in  the Google Cloud Storage.
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

        /// <summary>
        /// Get the asset in  the Google Cloud Storage with AssetBundle.
        /// </summary>
        public static async Task<T> GetAssetFromCloudStorage<T>(string googleCloudJwt, string bucketName, string objectName, string assetName) where T : UnityEngine.Object
        {
            AssetBundle assetBundle = await GetAssetBundleFromCloudStorageAsync(googleCloudJwt, bucketName, objectName);

            if (assetBundle == null) return null;

            return await GetAssetFromAssetBundle<T>(assetBundle, assetName);
        }

        private static async Task<T> GetAssetFromAssetBundle<T>(AssetBundle assetBundle, string assetName) where T : UnityEngine.Object
        {
            if (assetBundle == null)
            {
                Debug.LogError("AssetBundle is null.");

                return null;
            }

            T asset;

            try
            {
                AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync<T>(assetName);

                await Task.Yield();

                while (!assetBundleRequest.isDone) await Task.Delay(100);

#pragma warning disable CS8600
                asset = assetBundleRequest.asset as T;
#pragma warning restore CS8600
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return null;
            }

            if (asset == null)
            {
                Debug.LogError("Failed to load \"" + assetName + "\" from AssetBundle.");

                return null;
            }

            return asset;
        }

        private static async Task<AssetBundle> GetAssetBundleFromCloudStorageAsync(string googleCloudJwt, string bucketName, string objectName)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            nameValueCollection.Add("alt", "media");

            string apiUrl = "https://www.googleapis.com/storage/v1/b/" + bucketName + "/o/" + objectName + "?" + nameValueCollection.ToString();

            using UnityWebRequest unityWebRequest = new UnityWebRequest(apiUrl, "GET")
            {
                downloadHandler = new DownloadHandlerAssetBundle(apiUrl, 0)
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

            AssetBundle assetBundle;

            try
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(unityWebRequest);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return null;
            }

            if (assetBundle == null)
            {
                Debug.LogError("Failed to convert AssetBundle from content of sent unity web request.");

                return null;
            }

            return assetBundle;
        }
    }
}