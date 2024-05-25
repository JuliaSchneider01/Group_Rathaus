using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SzeneManager : MonoBehaviour
{
    public Button rathausButton; 
    public string sceneName;     

    void Start()
    {
        
        rathausButton.onClick.AddListener(SwitchScene);
        
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
