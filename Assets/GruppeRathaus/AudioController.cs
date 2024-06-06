using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GruppeRathaus{

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource; 
    public Button toggleButton;     
    public Sprite playSprite;       
    public Sprite stopSprite; 
    public Button forwardButton;    
    public Button backButton;       

    void Start()
    {
        toggleButton.onClick.AddListener(TogglePlayPause);
        forwardButton.onClick.AddListener(ForwardAudio);
        backButton.onClick.AddListener(BackwardAudio);
        
        UpdateButtonSprite();

    }

    public void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        
        }
        else
        {
            audioSource.Play();
        }
        
        //Debug.Log(audioSource.isPlaying);
       
        UpdateButtonSprite();
    }
    

    public void UpdateButtonSprite()
    {
        if (audioSource.isPlaying)
        {
            toggleButton.image.sprite = stopSprite;
        }
        else
        {
            toggleButton.image.sprite = playSprite;
        }
    }

    public void ForwardAudio()
{
    if (audioSource.time + 30f <= audioSource.clip.length)
    {
        audioSource.time += 30f; 
    }
}

    void BackwardAudio()
{
    if (audioSource.time - 30f >= 0f)
    {
        audioSource.time -= 30f; 
    }
}
}

}

