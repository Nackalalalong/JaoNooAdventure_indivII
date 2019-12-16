using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapermanAppearController : MonoBehaviour
{
    public void Countdown(float time){
        Destroy(gameObject, time);
    }

}
