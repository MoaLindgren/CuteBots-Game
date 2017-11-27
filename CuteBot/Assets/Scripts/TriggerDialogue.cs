using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    DialogueManager dialogueManagerScript;
    int placeNumber;
    bool hasHappened;
    bool newDialogue;

    void Start()
    {
        newDialogue = true;
        dialogueManagerScript = GameObject.Find("DialogueManager").gameObject.GetComponent<DialogueManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        hasHappened = !hasHappened;
        if(hasHappened && newDialogue)
        {

            if (other.tag =="Player" )
            {
                string tempString = this.gameObject.name;
                char temp = tempString[tempString.Length - 1];
                placeNumber = temp - '0';
                print(placeNumber);
                dialogueManagerScript.StartPhrase(placeNumber);
            }

        }
    }
    void OnTriggerExit(Collider other)
    {

        this.newDialogue = false;
    }
}
