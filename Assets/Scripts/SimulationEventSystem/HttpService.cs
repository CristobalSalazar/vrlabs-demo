using System.Net.Http;
using System.Text;
using UnityEngine;

public class HttpService
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async void SendPostRequest(object postData, string url)
    {
        string json = JsonUtility.ToJson(postData);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            await httpClient.PostAsync(url, data);
        }
        catch (HttpRequestException error)
        {
            Debug.Log(error);
        }
    }
}