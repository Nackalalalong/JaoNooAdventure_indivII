using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if ( other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.gameObject.tag == "Kunai" ){
            GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
            go.SetActive(true);
            Destroy(gameObject);
        }
    }
}
