using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

   

    /* public void IsMoveable(PlayerMovement player, Collider moveableObject)
     {
         float gravity = 1.0f;
         float pullF = 10f;
         Vector3 D = (player.transform.position - moveableObject.gameObject.transform.position) / gravity; // line from crate to player

         float pushF = -10f;
         Vector3 D2 = (player.transform.position - moveableObject.gameObject.transform.position) / gravity;
         float dist = D.magnitude;
         Vector3 pullDir = D.normalized;
         float pullForDist = (dist - (200)) / 2.0f;
         if (pullForDist > 20) pullForDist = 20;
         pullF += pullForDist;
         moveableObject.gameObject.GetComponent<Rigidbody>().velocity += pullDir;
     }*/


}
