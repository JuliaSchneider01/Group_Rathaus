using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GruppeRathaus
{
    class StoryBlock
    {
        public string story;
        public string optionA_Text;
        public string optionB_Text;
        public string optionC_Text;

        public StoryBlock(string story, string optionA_Text = "", string optionB_Text = "", string optionC_Text = "")
        {
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

        StoryBlock currentBlock;

        private FirebaseManager firebaseManager;

        private Color originalColor;

        public string currentChoice;

        void Awake()
        {


            Option_A = GameObject.Find("Button_A").GetComponent<Button>();
            Option_B = GameObject.Find("Button_B").GetComponent<Button>();
            Option_C = GameObject.Find("Button_C").GetComponent<Button>();
            ContinueButton = GameObject.Find("Continue").GetComponent<Button>();
            BackButton = GameObject.Find("BackToAudio").GetComponent<Button>();
            ScenePanel = GameObject.Find("ScenePanel");


            // Verstecke die UI-Story Elemente
            ScenePanel.gameObject.SetActive(false);
            Option_A.gameObject.SetActive(false);
            Option_B.gameObject.SetActive(false);
            Option_C.gameObject.SetActive(false);

            firebaseManager = GetComponent<FirebaseManager>();

            originalColor = Option_A.GetComponent<Image>().color;

        }

        // Initialer StoryBlock
        static StoryBlock block1 = new StoryBlock("Eine Bürgerinitiative fordert, dass 40% des Gemeinderats Juden und Jüdinnen sein sollen.\nSind Sie für diese Quote?",
         "A: Ja zur 40% Quote", "B: Nein zur 40% Quote", "C: Ja, aber 30% Quote und ein Mahnmal");
        static StoryBlock block2 = new StoryBlock("Umfrage beendet!", "", "", "");

        void Start()
        {   
            ContinueButton.onClick.AddListener(OnContinueButtonClicked);
            Option_A.onClick.AddListener(ButtonA_clicked);
            Option_B.onClick.AddListener(ButtonB_clicked);
            Option_C.onClick.AddListener(ButtonC_clicked);

        }

        void OnContinueButtonClicked()
        {

            if(currentBlock != block1){

                // Verstecke den Starttext und den Weiter-Button
                StartText.gameObject.SetActive(false);
                ContinueButton.gameObject.SetActive(false);
                BackButton.gameObject.SetActive(false);


                // Zeige die Story-UI-Elemente
                ScenePanel.gameObject.SetActive(true);
                Option_A.gameObject.SetActive(true);
                Option_B.gameObject.SetActive(true);
                Option_C.gameObject.SetActive(true);

                // Zeige den ersten StoryBlock
                DisplayBlock(block1);

            }
            else {
                DisplayNext();
                firebaseManager.SendChoiceToFirebase(currentChoice);
                ContinueButton.gameObject.SetActive(false);

            }
           
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
                ResetButtonColors();
                currentChoice = "A";
                Option_A.GetComponent<Image>().color = Color.green;
                ContinueButton.gameObject.SetActive(true);   

        }

        public void ButtonB_clicked()
        {
                ResetButtonColors();
                currentChoice = "B";
                Option_B.GetComponent<Image>().color = Color.green;
                ContinueButton.gameObject.SetActive(true); 

        }

        public void ButtonC_clicked()
        {
                ResetButtonColors();
                currentChoice = "C";
                Option_C.GetComponent<Image>().color = Color.green;
                ContinueButton.gameObject.SetActive(true); 
                
            
        }

        public void ResetButtonColors()
        {
            Option_A.GetComponent<Image>().color = originalColor;
            Option_B.GetComponent<Image>().color = originalColor;
            Option_C.GetComponent<Image>().color = originalColor;
        }


        void DisplayNext()
        {


            Option_A.gameObject.SetActive(false);
            Option_B.gameObject.SetActive(false);
            Option_C.gameObject.SetActive(false);

            DisplayBlock(block2);


        }
    }

}