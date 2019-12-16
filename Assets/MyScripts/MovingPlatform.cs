using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startPos;
     public Transform target;
     public float speed;
     private bool moveUp;
     void Start()
     {
         startPos = transform.position;
         moveUp = true;
     }
     void Update()
     {
         float step = speed * Time.deltaTime;
         if (transform.position == target.position)
         {
             moveUp = false;
         }
         else if (transform.position == startPos)
         {
             moveUp = true;
         }
         if(moveUp == false)
         {
             transform.position = Vector3.MoveTowards (transform.position, startPos, step);
         }
         else if (moveUp)
         {
             transform.position = Vector3.MoveTowards(transform.position, target.position, step);
         }
     }

     // Check if the Player is on a Platform and set the Player as child of the Platform
     void OnCollisionEnter2D (Collision2D other) {
         if (other.gameObject.tag == "Player" || other.gameObject.tag == "Zombie") {
             other.transform.parent = transform;
         }
     }
 
     // Check if the Player leaves a Platform and set it back out of Parent
     void OnCollisionExit2D (Collision2D other) {
         if (other.gameObject.tag == "Player" || other.gameObject.tag == "Zombie") {
             other.transform.parent = null;
         }
     }
}
