using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explodeRadius = 2;
    public int damage = 20;
        // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        for(int i=0; i<colliders.Length; ++i){
            Collider2D collider = colliders[i];
            if ( collider.gameObject.tag == "Player" ){
                collider.gameObject.GetComponent<Damageable>().Attacked(damage);
                break;
            }
        }
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
