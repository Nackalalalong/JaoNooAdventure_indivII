using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveArea : MonoBehaviour
{
    public GameObject gameOverScreen;
     void OnTriggerExit2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Player" ){
            gameOverScreen.SetActive(true);
        }
        Destroy(other.gameObject, 2);
    }
}
