using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTracker : MonoBehaviour
{
    public Text track;
    public GameObject[] enemies;
    private int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = 0;
        foreach(GameObject enemy in enemies){
            if ( enemy != null){
                count++;
            }
        }
        track.text = "จำนวนศัตรูที่เหลือ: " + count;
    }

    public bool CanWarp(){
        return count == 0;
    }
}
