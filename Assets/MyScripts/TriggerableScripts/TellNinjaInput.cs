using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellNinjaInput : MonoBehaviour, Triggerable
{
    public GameObject form;
    public void TriggerFunction(){
        form.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.T) ){
            form.SetActive(false);
            Destroy(this);
        }
    }
}
