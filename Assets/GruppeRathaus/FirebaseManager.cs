using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace GruppeRathaus
{
    public class FirebaseManager : MonoBehaviour
    {
        private string firebaseURL = "https://digital-humanities.uni-tuebingen.de/ltt-rathaus/";

        public void SendChoiceToFirebase(string choice)
        {
            StartCoroutine(PostChoice(choice));
        }

        

        IEnumerator PostChoice(string choice)
        {
            WWWForm form = new WWWForm();
            form.AddField("choice", choice);

            UnityWebRequest www = UnityWebRequest.Post(firebaseURL + "update_choice.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Choice count updated successfully");
            }
        }

        
}
}
