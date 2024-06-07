using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

namespace GruppeRathaus{
    class StoryBlock {
    public string story;
    public string optionA_Text;
    public string optionB_Text;
    public string optionC_Text;

    public StoryBlock(string story, string optionA_Text = "", string optionB_Text = "", string optionC_Text = "") {
        this.story = story;
        this.optionA_Text = optionA_Text;
        this.optionB_Text = optionB_Text;
        this.optionC_Text = optionC_Text;
    }
}

public class GameManager : MonoBehaviour
{
    public Text StartText;
    [HideInInspector] public Button ContinueButton;
    [HideInInspector] public Button BackButton;
    [HideInInspector] public Button Option_A;
    [HideInInspector] public Button Option_B;
    [HideInInspector] public Button Option_C;
    [HideInInspector] public GameObject ScenePanel;
    [HideInInspector] public InputField Passwordfield;
    StoryBlock currentBlock;
    public bool isDeveloper;

    // Reference to FirebaseManager
    private FirebaseManager firebaseManager;
    private bool block2ButtonPressed = false;

    void Awake()
    {
        isDeveloper = false;
        // Automatische Zuweisung des Buttons
        //StartText.text = "Startbildschirm";
        Option_A = GameObject.Find("Button_A").GetComponent<Button>();
        Option_B = GameObject.Find("Button_B").GetComponent<Button>();
        Option_C = GameObject.Find("Button_C").GetComponent<Button>();
        ContinueButton = GameObject.Find("Continue").GetComponent<Button>();
        BackButton = GameObject.Find("BackToAudio").GetComponent<Button>();
        ScenePanel = GameObject.Find("ScenePanel");
        Passwordfield = GameObject.Find("Passwordfield").GetComponent<InputField>();

        // Verstecke die UI-Story Elemente
        ScenePanel.gameObject.SetActive(false);
        Option_A.gameObject.SetActive(false);
        Option_B.gameObject.SetActive(false);
        Option_C.gameObject.SetActive(false);
        
        // Initialize FirebaseManager
        firebaseManager = GetComponent<FirebaseManager>();
    }

    // Initialer StoryBlock
    static StoryBlock block1 = new StoryBlock("Eine Bürgerinitiative fordert, dass 40% des Gemeinderats Juden und Jüdinnen sein sollen. Sind Sie für diese Quote?",
     "A: Ja zur 40% Quote", "B: Nein zur 40% Quote", "C: Ja, aber niedrigere Quote und ein Mahnmal");
    static StoryBlock block2 = new StoryBlock("Developermode", "Load results", "Reset", "");

    void Start()
    {
        ContinueButton.onClick.AddListener(OnContinueButtonClicked);
        Option_A.onClick.AddListener(ButtonA_clicked);
        Option_B.onClick.AddListener(ButtonB_clicked);
        Option_C.onClick.AddListener(ButtonC_clicked);

        Passwordfield.onValueChanged.AddListener(delegate { PasswordInputFieldChanged(Passwordfield.text); });
    }

    void OnContinueButtonClicked()
    {
        // Verstecke den Starttext und den Weiter-Button
        StartText.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Passwordfield.gameObject.SetActive(false);

        // Zeige die Story-UI-Elemente
        ScenePanel.gameObject.SetActive(true);
        Option_A.gameObject.SetActive(true);
        Option_B.gameObject.SetActive(true);
        Option_C.gameObject.SetActive(true);

        // Zeige den ersten StoryBlock
        DisplayBlock(block1);
    }

    void DisplayBlock(StoryBlock block)
    {
        ScenePanel.GetComponentInChildren<TMP_Text>().text = block.story;
        Option_A.GetComponentInChildren<TMP_Text>().text = block.optionA_Text;
        Option_B.GetComponentInChildren<TMP_Text>().text = block.optionB_Text;
        Option_C.GetComponentInChildren<TMP_Text>().text = block.optionC_Text;

        currentBlock = block;
    }

    public void ButtonA_clicked()
    {
        if(currentBlock==block1)
        {
        firebaseManager.SendChoiceToFirebase("A");
        DisplayNext();
        }
        else if (currentBlock == block2)
        {
            if (block2ButtonPressed)
            {
                firebaseManager.GetMostChosenOption();
                block2ButtonPressed = false; // Reset the state for future use
            }
            else
            {
                block2ButtonPressed = true;
            }
        }
    }

    public void ButtonB_clicked()
    {
        if(currentBlock==block1)
        {
        firebaseManager.SendChoiceToFirebase("B");
        DisplayNext();
        }
        else if(currentBlock==block2){

            if (block2ButtonPressed)
                {
                    firebaseManager.ResetChoicesInFirebase();
                    block2ButtonPressed = false; // Reset the state for future use
                }
                else
                {
                    block2ButtonPressed = true;
                }
        }
    }

    public void ButtonC_clicked()
    {
        if(currentBlock==block1)
        {
        firebaseManager.SendChoiceToFirebase("C");
        DisplayNext();
        }
    }

   public void UpdateScenePanel(string mostChosenOption)
{
    ScenePanel.GetComponentInChildren<TMP_Text>().text = "Most chosen option: " + mostChosenOption;
}

    public void PasswordInputFieldChanged(string newText)
        {
            if (newText == "LTT")
        {
            isDeveloper = true; 
            Debug.Log("Passwort korrekt!");
        }
        
        }

    void DisplayNext()
    {

        // Hier kannst du die Logik hinzufügen, um die nächste Frage anzuzeigen
        // Beispielsweise könntest du eine neue Frage basierend auf der getroffenen Wahl laden
        // Im Moment wird dieselbe Frage wieder angezeigt

        if(!isDeveloper){
            ScenePanel.gameObject.SetActive(false);
            Option_A.gameObject.SetActive(false);
            Option_B.gameObject.SetActive(false);
            Option_C.gameObject.SetActive(false);
        }

        DisplayBlock(block2);
        Option_C.gameObject.SetActive(false);

    }
}

}