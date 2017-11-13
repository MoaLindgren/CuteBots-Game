using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDraw : MonoBehaviour {
    /* Du vill göra en timer som är random för varje gång den körs. 
     * När timern når 0 ska det hända något.  */


    float interval = 0;
    bool input = false;

	
	void Start ()
    {
        interval = Random.Range(4, 7);
        if (Input.GetKey(KeyCode.Space))
        {
            input = true;
        }

        if ( interval <= 0 && interval > -1 && input == true)
        {
            print("You win!!");
        }

    }
	
	
	void Update ()
    {

        interval -= Time.deltaTime;
        if (interval <= 0)
        {
            print("BANG");
        }
        if (interval <= -1 && input == false)
        {
            print("You lose!");
        }
    }

}
