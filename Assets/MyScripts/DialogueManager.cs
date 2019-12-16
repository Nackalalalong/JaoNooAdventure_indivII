using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class DialogueManager : MonoBehaviour
{
    private bool isPlayingDialogue = false;
    int mode; // 0 for solo dialogue 1 for conversation dialogue
    private Queue<string> sentences;
    private Text dialogueText;
    private GameObject dialogueForm;
    private Queue<KeyValuePair<string,int>> conversationSentences;
    private GameObject[] conversationDialogueForms;
    public GameObject player;
    private MyCharacterUserControl myCharacterUserControl;
    private bool addComponentsWhenFinish = false;  
    private ConversationDialogueTrigger conversationDialogueTrigger;  

    void Update(){
        if ( Input.GetKeyDown(KeyCode.Return) && isPlayingDialogue ){
            if ( mode == 0 ){
                DisplayNextSentence();
            }
            else if ( mode == 1){
                DisplayNextConversationDialogue();
            }
        }
    }

    void Start(){
        sentences = new Queue<string>();
        conversationSentences = new Queue<KeyValuePair<string, int>>();
        myCharacterUserControl = player.GetComponent<MyCharacterUserControl>();
    }

    public void StartConversationDialogue(ConversationDialogue conversationDialogue, GameObject[] conversationDialogueForms){
        mode = 1;
        this.conversationDialogueForms = conversationDialogueForms;
        myCharacterUserControl.SetCanControl(false);
        isPlayingDialogue = true;
        for(int i=0; i<conversationDialogue.sentences.Length; ++i){
            conversationSentences.Enqueue(
                new KeyValuePair<string, int>(conversationDialogue.sentences[i],conversationDialogue.formNumbers[i]));
        }
        DisplayNextConversationDialogue();
    }

    public void StartConversationDialogue(ConversationDialogue conversationDialogue, GameObject[] conversationDialogueForms, ConversationDialogueTrigger conversationDialogueTrigger){
        addComponentsWhenFinish = true;
        this.conversationDialogueTrigger = conversationDialogueTrigger;
        StartConversationDialogue(conversationDialogue, conversationDialogueForms);
    }

    private void DisplayNextConversationDialogue(){
        if ( conversationSentences.Count == 0 ){
            EndConversationDialogue();
            return;
        }
        foreach(GameObject gameObject in conversationDialogueForms){
            gameObject.SetActive(false);
        }
        KeyValuePair<string,int> nextConversationSentences = conversationSentences.Dequeue();
        string sentence = nextConversationSentences.Key;
        int formNumber = nextConversationSentences.Value;
        conversationDialogueForms[formNumber].GetComponentInChildren<Text>().text = sentence;
        conversationDialogueForms[formNumber].SetActive(true);
    }

    private void EndConversationDialogue(){
        isPlayingDialogue = false;
        myCharacterUserControl.SetCanControl(true);
        foreach(GameObject gameObject in conversationDialogueForms){
            gameObject.SetActive(false);
        }
        if ( addComponentsWhenFinish ){
            addComponentsWhenFinish = false;
            conversationDialogueTrigger.DialogueFinish();
        }
    }
    public void StartDialogue(Dialogue dialogue, GameObject dialogueForm){
        mode = 0;
        myCharacterUserControl.SetCanControl(false);
        this.dialogueForm = dialogueForm;   
        dialogueText = dialogueForm.GetComponentInChildren<Text>();
        dialogueForm.SetActive(true);
        isPlayingDialogue = true;
        foreach ( string sentence in dialogue.sentences ){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private void DisplayNextSentence(){
        if ( sentences.Count == 0 ){
            EndDialogue();
            return ;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue(){
        isPlayingDialogue = false;
        dialogueForm.SetActive(false);
        myCharacterUserControl.SetCanControl(true);
    }

    public bool IsDisplayingDialogue(){
        return isPlayingDialogue;
    }
}
