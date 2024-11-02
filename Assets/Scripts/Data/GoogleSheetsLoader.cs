using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetsLoader : MonoBehaviour
{
    // 0 -> docId
    // 1 -> gId
    const string BASE_URL = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";

    public static IEnumerator LoadData(string docId, string gId, System.Action<string> callback)
    {
        string url = string.Format(BASE_URL, docId, gId);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading data: " + request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                callback?.Invoke(request.downloadHandler.text);
            }
        }
    }
}
