using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;


public class FirebaseManager : MonoBehaviour
{
    // Firebase URL (replace with your actual Firebase URL)
    private string firebaseURL = "https://rathaus-33b2b-default-rtdb.europe-west1.firebasedatabase.app/";

     public void SendChoiceToFirebase(string choice)
    {
        StartCoroutine(PostChoice(choice));
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

        // Update the count in the 'options' node
        url = firebaseURL + "options/" + choice + ".json";
        UnityWebRequest optionsRequest = UnityWebRequest.Get(url);
        yield return optionsRequest.SendWebRequest();

        if (optionsRequest.result == UnityWebRequest.Result.ConnectionError || optionsRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(optionsRequest.error);
        }
        else
        {
            string optionsJsonResponse = optionsRequest.downloadHandler.text;
            int currentOptionsCount = 0;
            if (!string.IsNullOrEmpty(optionsJsonResponse) && optionsJsonResponse != "null")
            {
                currentOptionsCount = int.Parse(optionsJsonResponse);
            }

            // Increment the options count
            currentOptionsCount++;
            string updatedOptionsJson = currentOptionsCount.ToString();

            UnityWebRequest optionsPutRequest = new UnityWebRequest(url, "PUT");
            byte[] optionsBodyRaw = Encoding.UTF8.GetBytes(updatedOptionsJson);
            optionsPutRequest.uploadHandler = new UploadHandlerRaw(optionsBodyRaw);
            optionsPutRequest.downloadHandler = new DownloadHandlerBuffer();
            optionsPutRequest.SetRequestHeader("Content-Type", "application/json");

            yield return optionsPutRequest.SendWebRequest();

            if (optionsPutRequest.result == UnityWebRequest.Result.ConnectionError || optionsPutRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(optionsPutRequest.error);
            }
            else
            {
                Debug.Log("Options count updated successfully");
            }
        }
    }
}

}
