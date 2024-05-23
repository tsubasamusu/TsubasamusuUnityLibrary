#pragma warning disable CS8602
#pragma warning disable CS8603
#pragma warning disable CS8600
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TSUBASAMUSU.UnityWebRequestAwaiter;
using UnityEngine;
using UnityEngine.Networking;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    public static class SpreadsheetManager
    {
        public static async Task<Response_SetCellValue> SetCellValueAsync(string googleCloudJwt, string spreadsheetId, string sheetName, int row, int column, string setValue)
        {
            string targetCell = SpreadsheetUtility.GetStringValueFromCellValues(row, column);

            Request_SetCellValue request = new Request_SetCellValue()
            {
                valueInputOption = ValueInputOption.RAW,
                data = new List<ValueRange>()
                    {
                        new ValueRange()
                        {
                            range = sheetName+"!"+targetCell+":"+targetCell,
                            majorDimension = Dimension.ROWS,
                            values = new List<List<string>>()
                            {
                                new List<string>()
                                {
                                    setValue
                                }
                            }
                        }
                    },
                includeValuesInResponse = false,
                responseValueRenderOption = ValueRenderOption.FORMULA,
                responseDateTimeRenderOption = DateTimeRenderOption.FORMATTED_STRING
            };

            string jsonRequest = JsonConvert.SerializeObject(request);

            string apiUrl = "https://sheets.googleapis.com/v4/spreadsheets/" + spreadsheetId + "/values:batchUpdate";

            using UnityWebRequest unityWebRequest = new UnityWebRequest(apiUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonRequest)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Authorization", "Bearer " + googleCloudJwt},
                {"Content-type", "application/json"}
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

            return JsonConvert.DeserializeObject<Response_SetCellValue>(unityWebRequest.downloadHandler.text);
        }

        public static async Task<List<List<string>>> GetCellValuesAsync(string googleCloudJwt, string spreadsheetId, string sheetName, (int row, int column) firstCell, (int row, int column) lastCell)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            nameValueCollection.Add("ranges", sheetName + "!" + SpreadsheetUtility.GetStringValueFromCellValues(firstCell.row, firstCell.column) + ":" + SpreadsheetUtility.GetStringValueFromCellValues(lastCell.row, lastCell.column));
            nameValueCollection.Add("majorDimension", Dimension.ROWS.ToString());
            nameValueCollection.Add("valueRenderOption", ValueRenderOption.FORMULA.ToString());
            nameValueCollection.Add("dateTimeRenderOption", DateTimeRenderOption.FORMATTED_STRING.ToString());

            string apiUrl = "https://sheets.googleapis.com/v4/spreadsheets/" + spreadsheetId + "/values:batchGet?" + nameValueCollection.ToString();

            using UnityWebRequest unityWebRequest = new UnityWebRequest(apiUrl, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
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

            Response_GetCellValues response = JsonConvert.DeserializeObject<Response_GetCellValues>(unityWebRequest.downloadHandler.text);

            return response.valueRanges[0].values;
        }

        public static async Task<int> GetLastRowAsync(string googleCloudJwt, string spreadsheetId, string sheetName, int column)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            nameValueCollection.Add("ranges", sheetName + "!" + SpreadsheetUtility.ConvertIntegerToAlphabets(column) + ":" + SpreadsheetUtility.ConvertIntegerToAlphabets(column));
            nameValueCollection.Add("majorDimension", Dimension.ROWS.ToString());
            nameValueCollection.Add("valueRenderOption", ValueRenderOption.FORMULA.ToString());
            nameValueCollection.Add("dateTimeRenderOption", DateTimeRenderOption.FORMATTED_STRING.ToString());

            string apiUrl = "https://sheets.googleapis.com/v4/spreadsheets/" + spreadsheetId + "/values:batchGet?" + nameValueCollection.ToString();

            using UnityWebRequest unityWebRequest = new UnityWebRequest(apiUrl, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
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

                return -1;
            }

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(unityWebRequest.result.ToString());

                Debug.LogError(unityWebRequest.downloadHandler.text);

                return -1;
            }

            Response_GetCellValues response = JsonConvert.DeserializeObject<Response_GetCellValues>(unityWebRequest.downloadHandler.text);

            return response.valueRanges[0].values.Count;
        }
    }
}