using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GruppeRathaus{

public class BackToAudio : MonoBehaviour
{
    [HideInInspector] public Button backButton;

    void Awake()
    {
       
        backButton = GameObject.Find("BackToAudio").GetComponent<Button>();
    }

    void Start()
    {
        backButton.onClick.AddListener(SwitchToAudio);
    }

    void SwitchToAudio()
    {
        SceneManager.LoadScene("Audio");
    }
}
}


