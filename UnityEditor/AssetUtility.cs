using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace TSUBASAMUSU.UnityEditor
{
    public static class AssetUtility
    {
        public static async Task<bool> CreateJsonFileAtRootDirectoryAsync(string jsonString, string fileName)
        {
            if (!fileName.Contains(".json")) fileName = fileName + ".json";

            string path = Path.Combine(Application.dataPath, fileName);

            try
            {
                Debug.Log("Start to create JSON file named \"" + fileName + "\".");

                await File.WriteAllTextAsync(path, jsonString);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return false;
            }

            Debug.Log("Completed to create JSON file named \"" + fileName + "\".");

            return true;
        }
    }
}