using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControlDie : MonoBehaviour
{
     PlayerController pc;


     private void OnTriggerEnter2D(Collider2D collision)
     {

           if (collision.gameObject.tag == "Player")
        {
            // pc.isHurt = true;
        //     pc.lifes--;
        //     pc.Life();
         }
    
     }
}
