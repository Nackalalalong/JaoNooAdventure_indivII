using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyIgnoreColision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9,11);
        Physics2D.IgnoreLayerCollision(11,11);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
