using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationDialogueTrigger : MonoBehaviour, Triggerable
{
    public GameObject[] conversationDialogueForms;
    public ConversationDialogue conversationDialogue;
    public bool hasFollowingDialogue = false;
    public GameObject followingDialogueTrigger;
    public void TriggerConversationDialogue(){
        if ( hasFollowingDialogue ){
            FindObjectOfType<DialogueManager>().StartConversationDialogue(conversationDialogue, conversationDialogueForms, this);
        }
        else {
            FindObjectOfType<DialogueManager>().StartConversationDialogue(conversationDialogue, conversationDialogueForms);
        }
        
    }

    public void TriggerFunction(){
        TriggerConversationDialogue();
    }

    public void DialogueFinish(){
        followingDialogueTrigger.GetComponent<Triggerable>().TriggerFunction();
    }
}
