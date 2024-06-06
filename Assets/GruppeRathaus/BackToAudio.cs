using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GruppeRathaus{

public class BackToAudio : MonoBehaviour
{
    [HideInInspector] public Button backButton;  // Der "Zurück" Button, um zur "Audio" Szene zu wechseln

    void Awake()
    {
        // Automatische Zuweisung des Buttons
        backButton = GameObject.Find("BackToAudio").GetComponent<Button>();
    }

    void Start()
    {
        // Füge den Event-Listener hinzu
        backButton.onClick.AddListener(SwitchToAudio);
    }

    void SwitchToAudio()
    {
        // Wechsel zur Szene "Audio"
        SceneManager.LoadScene("Audio");
    }
}
}


