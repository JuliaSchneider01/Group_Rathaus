using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace GruppeRathaus{
    public class ServerManager : MonoBehaviour
{
    public SurveyManager surveyManager;
    // Firebase URL (replace with your actual Firebase URL)
    private string firebaseURL = "https://rathaus-33b2b-default-rtdb.europe-west1.firebasedatabase.app/";

   


    public void GetOptionCountsFromFirebase()
    {
        StartCoroutine(GetOptionCountsRoutine());
    }

    public void ResetChoicesInFirebase()
    {
        StartCoroutine(ResetChoices());
    }


    public IEnumerator GetOptionCountsRoutine()
    {
        //Debug.Log("OptioncountRoutine wird aufgerufen");
        string url = firebaseURL + ".json";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            OptionCounts optionCounts = JsonUtility.FromJson<OptionCounts>(jsonResponse);

            // Ergebnisse an den SurveyManager senden
            //Debug.Log("Optioncounts"+optionCounts);
            surveyManager.UpdateBarChart(optionCounts);
        }
    }

    [System.Serializable]
    public class OptionCounts
    {
        public int A;
        public int B;
        public int C;
    }

    IEnumerator ResetChoices()
    {
        string[] choices = { "A", "B", "C" };
        foreach (string choice in choices)
        {
            string url = firebaseURL + choice + ".json";
            UnityWebRequest request = new UnityWebRequest(url, "PUT");
            byte[] bodyRaw = Encoding.UTF8.GetBytes("0");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log($"{choice} count reset successfully");
            }
        }
    }
}
}

