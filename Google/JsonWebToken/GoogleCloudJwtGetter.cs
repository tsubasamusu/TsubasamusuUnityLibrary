using System;
using System.Text;
using System.Threading.Tasks;
using TSUBASAMUSU.UnityWebRequestAwaiter;
using UnityEngine;
using UnityEngine.Networking;

namespace TSUBASAMUSU.Google.JsonWebToken
{
    public static class GoogleCloudJwtGetter
    {
        public static async Task<(string message, long currentUnixTime)> GetGoogleCloudJwtAsync(string googleCloudRunUrl, string privateKey, string serviceAccountEmailAddress, string[] scopes)
        {
            Request request = new Request
            {
                privateKey = privateKey,
                serviceAccountEmailAddress = serviceAccountEmailAddress,
                scopes = string.Join(" ", scopes)
            };

            using UnityWebRequest unityWebRequest = new UnityWebRequest(googleCloudRunUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(request))),
                downloadHandler = new DownloadHandlerBuffer()
            };

            unityWebRequest.SetRequestHeader("Content-type", "application/json");

            try
            {
                await unityWebRequest.SendWebRequest();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return (string.Empty, -1);
            }

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(unityWebRequest.result.ToString());

                Debug.LogError(unityWebRequest.downloadHandler.text);

                return (string.Empty, -1);
            }

            return (unityWebRequest.downloadHandler.text, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
        }
    }
}