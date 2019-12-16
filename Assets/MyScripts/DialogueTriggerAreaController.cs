using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerAreaController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if ( other.gameObject.tag == "Player" ){
            Destroy(GetComponent<BoxCollider2D>());
            if ( GetComponent<DialogueTrigger>() != null ){
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            else {
                GetComponent<ConversationDialogueTrigger>().TriggerConversationDialogue();
            }
        }
        
    }

}
