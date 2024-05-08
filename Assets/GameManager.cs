using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

class StoryBlock {
    public string story;
    public string optionA_Text;
    public string optionB_Text;
    public string optionC_Text;
    public StoryBlock optionA_Block;
    public StoryBlock optionB_Block;
    public StoryBlock optionC_Block;

    public StoryBlock(string story, string optionA_Text = "", string optionB_Text = "", string optionC_Text ="", 
                        StoryBlock optionA_Block =null, StoryBlock optionB_Block = null, StoryBlock optionC_Block = null){
        this.story = story;
        this.optionA_Text = optionA_Text;
        this.optionB_Text = optionB_Text;
        this.optionC_Text = optionC_Text;
        this.optionA_Block = optionA_Block;
        this.optionB_Block = optionB_Block;
        this.optionC_Block = optionC_Block;
    }
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI SzeneText;
    [HideInInspector] public Button Option_A;
    [HideInInspector] public Button Option_B;
    [HideInInspector] public Button Option_C;
    StoryBlock currentBlock;
    
    void Awake()
    {
        Option_A = GameObject.Find("Button_A").GetComponent<Button>();
        Option_B = GameObject.Find("Button_B").GetComponent<Button>();
        Option_C = GameObject.Find("Button_C").GetComponent<Button>();
    }

    static StoryBlock block3 = new StoryBlock("Freut mich!", "", "", "");
    static StoryBlock block2 = new StoryBlock("Oh", "", "", "");
    static StoryBlock block1 = new StoryBlock("Wie gehts?", "Gut", "schlecht", "", block3, block2);

   

    void Start()
    {
       DisplayBlock(block1);
    }

    void DisplayBlock(StoryBlock block){
        SzeneText.text = block.story;
        Option_A.GetComponentInChildren<TMP_Text>().text = block.optionA_Text;
        Option_B.GetComponentInChildren<TMP_Text>().text = block.optionB_Text;
        Option_C.GetComponentInChildren<TMP_Text>().text = block.optionC_Text;

        currentBlock = block;

    }

    public void ButtonA_clicked(){
        DisplayBlock(currentBlock.optionA_Block);
    }
    
    public void ButtonB_clicked(){
        DisplayBlock(currentBlock.optionB_Block);
    }
    
    public void ButtonC_clicked(){
        DisplayBlock(currentBlock.optionC_Block);
        
    }

}
