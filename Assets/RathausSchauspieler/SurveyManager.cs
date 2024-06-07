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
    public GameObject ResultA;
    public GameObject ResultB;
    public GameObject ResultC;
    
    [HideInInspector] public Slider resultASlider;
    [HideInInspector] public Slider resultBSlider;
    [HideInInspector] public Slider resultCSlider;

    // Reference to FirebaseManager
    public ServerManager serverManager;
    //public static SurveyManager Instance { get; private set; }
    void Awake()
    {
        Option_A = GameObject.Find("Button_A").GetComponent<Button>();
        Option_B = GameObject.Find("Button_B").GetComponent<Button>();
        
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
        // Display results on the bar chart sliders
        // Debug.Log("Update Bar wird aufgerufen");
        resultASlider.value = optionCounts.A;
        resultBSlider.value = optionCounts.B;
        resultCSlider.value = optionCounts.C;
    }

    }

    
}



