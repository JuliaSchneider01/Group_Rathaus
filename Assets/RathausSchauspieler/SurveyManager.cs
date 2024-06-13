using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 


namespace GruppeRathaus{
public class SurveyManager : MonoBehaviour
{
   
    [HideInInspector] public Button Option_A;
    [HideInInspector] public Button Option_B;
    [HideInInspector] public GameObject ResultA;
    [HideInInspector] public GameObject ResultB;
    [HideInInspector] public GameObject ResultC;
    
    [HideInInspector] public Slider resultASlider;
    [HideInInspector] public Slider resultBSlider;
    [HideInInspector] public Slider resultCSlider;

    [HideInInspector] public GameObject TextA;
    [HideInInspector] public GameObject TextB;
    [HideInInspector] public GameObject TextC;
    private float animationDuration = 1.0f;

    // Reference to FirebaseManager
    public ServerManager serverManager;
    void Awake()
    {
        Option_A = GameObject.Find("Button_A").GetComponent<Button>();
        Option_B = GameObject.Find("Button_B").GetComponent<Button>();
        ResultA = GameObject.Find("ResultA");
        ResultB = GameObject.Find("ResultB");
        ResultC = GameObject.Find("ResultC");
        TextA = GameObject.Find("TextA");
        TextB = GameObject.Find("TextB");
        TextC = GameObject.Find("TextC");
        
        
        
        serverManager = GetComponent<ServerManager>();
    }

    
    void Start()
    {
        
        Option_A.onClick.AddListener(ButtonA_clicked);
        Option_B.onClick.AddListener(ButtonB_clicked);

        resultASlider = ResultA.GetComponentInChildren<Slider>();
        resultBSlider = ResultB.GetComponentInChildren<Slider>();
        resultCSlider = ResultC.GetComponentInChildren<Slider>();
       
    }

   

    public void ButtonA_clicked()
    {
        
        serverManager.GetOptionCountsFromFirebase();
        Debug.Log("serverManager"+serverManager);
             
    }

    public void ButtonB_clicked()
    {
       
        serverManager.ResetChoicesInFirebase();
                   
    }

    

     public void UpdateBarChart(ServerManager.OptionCounts optionCounts)
        {
            StartCoroutine(AnimateSlider(resultASlider, optionCounts.A));
            StartCoroutine(AnimateSlider(resultBSlider, optionCounts.B));
            StartCoroutine(AnimateSlider(resultCSlider, optionCounts.C));

            TextA.GetComponentInChildren<Text>().text = "A: " + optionCounts.A;
            TextB.GetComponentInChildren<Text>().text = "B: " + optionCounts.B;
            TextC.GetComponentInChildren<Text>().text = "C: " + optionCounts.C;
        }

        private IEnumerator AnimateSlider(Slider slider, float targetValue)
        {
            float startValue = slider.value;
            float time = 0f;

            while (time < animationDuration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(startValue, targetValue, time / animationDuration);
                yield return null;
            }

            slider.value = targetValue; 
        }
    }

    

    
}



