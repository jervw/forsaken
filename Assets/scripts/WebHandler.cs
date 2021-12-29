using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebHandler : MonoBehaviour
{
    public static WebHandler Instance { get; private set; }

    [SerializeField] private string username, password;

    [Header("URLs")]
    [SerializeField] private string getUrl, postUrl;

    WWWForm auth;
    Dictionary<string, string> scoreboard = new Dictionary<string, string>();

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Auth();
        StartCoroutine(GetScores());

    }

    void Auth()
    {
        auth = new WWWForm();
        auth.AddField("user", username);
        auth.AddField("password", password);
    }

    IEnumerator GetScores()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(getUrl))
        {
            // Request and wait for the desired page.
            yield return request.SendWebRequest();

            if (UnityWebRequest.Result.Success == request.result)
            {
                string[] scores = request.downloadHandler.text.Split('\n');
                foreach (string score in scores)
                {
                    string[] scoreData = score.Split(',');
                    if (scoreData.Length == 2)
                        scoreboard[scoreData[0]] = scoreData[1];
                }
            }
        }

        foreach (KeyValuePair<string, string> entry in scoreboard)
        {
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }

    IEnumerator PostScore(string name, int score)
    {
        auth.AddField("nickname", name);
        auth.AddField("score", score);

        using (UnityWebRequest request = UnityWebRequest.Post(postUrl, auth))
        {
            yield return request.SendWebRequest();

            if (UnityWebRequest.Result.Success == request.result)
                Debug.Log(request.downloadHandler.text);
            else
                Debug.LogError("Error: " + request.error);
        }

    }
}
