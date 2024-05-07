using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI SzeneText;
    public Button Option_A;
    public Button Option_B;
    public Button Option_C;
    
    // Start is called before the first frame update
    void Start()
    {
        SzeneText.text = "Welcome";
        Option_A.GetComponentInChildren<TMP_Text>().text = "option1";
        Option_B.GetComponentInChildren<TMP_Text>().text = "option2";
        Option_C.GetComponentInChildren<TMP_Text>().text = "option3";
    }
}

