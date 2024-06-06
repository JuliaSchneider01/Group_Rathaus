using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace GruppeRathaus{
    public class FirebaseManager : MonoBehaviour
{
    public GameManager gameManager;
    // Firebase URL (replace with your actual Firebase URL)
    private string firebaseURL = "https://rathaus-33b2b-default-rtdb.europe-west1.firebasedatabase.app/";

    public void SendChoiceToFirebase(string choice)
    {
        StartCoroutine(PostChoice(choice));
    }


    public void GetMostChosenOption()
    {
        StartCoroutine(GetMostChosenOptionRoutine());
    }

    public void ResetChoicesInFirebase()
    {
        StartCoroutine(ResetChoices());
    }

    IEnumerator PostChoice(string choice)
    {
        string url = firebaseURL + choice + ".json";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            int currentCount = 0;
            if (!string.IsNullOrEmpty(jsonResponse) && jsonResponse != "null")
            {
                currentCount = int.Parse(jsonResponse);
            }

            // Increment the choice count
            currentCount++;
            string updatedJson = currentCount.ToString();

            UnityWebRequest putRequest = new UnityWebRequest(url, "PUT");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(updatedJson);
            putRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            putRequest.downloadHandler = new DownloadHandlerBuffer();
            putRequest.SetRequestHeader("Content-Type", "application/json");

            yield return putRequest.SendWebRequest();

            if (putRequest.result == UnityWebRequest.Result.ConnectionError || putRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(putRequest.error);
            }
            else
            {
                Debug.Log("Choice count updated successfully");
            }
        }
    }


    
    IEnumerator GetMostChosenOptionRoutine()
    {
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

            // Bestimme die am häufigsten gewählte Option
            string mostChosenOption = GetMostChosenOption(optionCounts);
            Debug.Log("Most chosen option: " + mostChosenOption);

            // Aktualisiere das Szene-Panel mit der am häufigsten gewählten Option
            gameManager.UpdateScenePanel(mostChosenOption);
        }
    }

    public string GetMostChosenOption(OptionCounts optionCounts)
    {
        string mostChosenOption = "";
        int maxCount = 0;

        if (optionCounts.A > maxCount)
        {
            mostChosenOption = "A";
            maxCount = optionCounts.B;
        }

        if (optionCounts.B > maxCount)
        {
            mostChosenOption = "B";
            maxCount = optionCounts.B;
        }

        if (optionCounts.C > maxCount)
        {
            mostChosenOption = "C";
        }

        return mostChosenOption;
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

