using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public GameObject dialogueForm;

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueForm);
    }
}
