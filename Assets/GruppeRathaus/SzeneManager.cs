using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GruppeRathaus{
    public class SzeneManager : MonoBehaviour
{
    [HideInInspector] public Button rathausButton; 
     

    void Start()
    {
        
        rathausButton.onClick.AddListener(SwitchScene);
        
    }

    void Awake()
    {
        // Automatische Zuweisung des Buttons
        rathausButton = GameObject.Find("Rathaus").GetComponent<Button>();
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("Rathaus");
    }
}

}