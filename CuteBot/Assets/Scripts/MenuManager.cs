using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enlarge(Text text)
    {
        if(text.fontSize == 30)
        {
            text.fontSize = 40;
        }
        else
        {
            text.fontSize = 30;
        }
    }

}
