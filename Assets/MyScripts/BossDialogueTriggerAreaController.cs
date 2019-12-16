using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueTriggerAreaController :MonoBehaviour, Triggerable
{
    public GameObject boss;
    public void TriggerFunction(){
        StartCoroutine(DelayAct());
    }

    IEnumerator DelayAct(){
        yield return new WaitForSeconds(1.5f);
        boss.GetComponent<BossController>().SetCanAct(true);
    }
}
