using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace GruppeRathaus {
    public class FirebaseManager : MonoBehaviour {
        public GameManager gameManager;
        private string serverURL = "http://localhost/unity_server/";

        public void SendChoiceToFirebase(string choice) {
            StartCoroutine(PostChoice(choice));
        }

        public void GetMostChosenOption() {
            StartCoroutine(GetMostChosenOptionRoutine());
        }

        public void ResetChoicesInFirebase() {
            StartCoroutine(ResetChoices());
        }

        IEnumerator PostChoice(string choice) {
            string url = serverURL + "choices.php";
            Dictionary<string, string> postData = new Dictionary<string, string> {
                { "choice", choice }
            };
            string json = JsonUtility.ToJson(postData);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
            } else {
                Debug.Log("Choice sent successfully");
            }
        }

        IEnumerator GetMostChosenOptionRoutine() {
            string url = serverURL + "most_chosen_option.php";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
            } else {
                string jsonResponse = request.downloadHandler.text;
                MostChosenOptionResponse response = JsonUtility.FromJson<MostChosenOptionResponse>(jsonResponse);
                string mostChosenOption = response.mostChosen;
                Debug.Log("Most chosen option: " + mostChosenOption);

                gameManager.UpdateScenePanel(mostChosenOption);
            }
        }

        IEnumerator ResetChoices() {
            string url = serverURL + "reset-choices.php";
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes("{}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
            } else {
                Debug.Log("Choices reset successfully");
            }
        }

        [System.Serializable]
        public class MostChosenOptionResponse {
            public string mostChosen;
        }
    }
}
