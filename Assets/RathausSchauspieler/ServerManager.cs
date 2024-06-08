using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace GruppeRathaus {
    public class ServerManager : MonoBehaviour {
        // Firebase URL (replace with your actual Firebase URL)
        private string firebaseURL =  "https://digital-humanities.uni-tuebingen.de/ltt-rathaus/";

        public SurveyManager surveyManager;

        public void GetOptionCountsFromFirebase() {
            StartCoroutine(GetOptionCountsRoutine());
        }

        public void ResetChoicesInFirebase() {
            StartCoroutine(ResetChoices());
        }

        public IEnumerator GetOptionCountsRoutine() {
            string url = firebaseURL + "get_option_counts.php";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
            } else {
                string jsonResponse = request.downloadHandler.text;
                OptionCounts optionCounts = JsonUtility.FromJson<OptionCounts>(jsonResponse);

                // Ergebnisse an den SurveyManager senden
                surveyManager.UpdateBarChart(optionCounts);
            }
        }

        [System.Serializable]
        public class OptionCounts {
            public int A;
            public int B;
            public int C;
        }

        IEnumerator ResetChoices() {
            string url = firebaseURL + "reset_choices.php";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
            } else {
                Debug.Log("Choices reset successfully");
            }
        }
    }
}
