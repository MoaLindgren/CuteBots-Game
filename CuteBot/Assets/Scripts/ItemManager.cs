using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float pushForce = 2.0f;

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        Vector3 direction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirection * pushForce;
    }

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
