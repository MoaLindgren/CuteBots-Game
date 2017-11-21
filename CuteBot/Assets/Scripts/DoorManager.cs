using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

    private Animator anim;

    private string doorAnimation;
    private string playerTag;
    private string guardTag;
    private string[] doorStates;

    private int value;

    [SerializeField]
    private bool lockedDoor;
    [SerializeField]
    private bool brokenDoor;
    [SerializeField]
    private bool keyCollected;
    private bool player;

    private PlayerManager playerManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        keyCollected = false;

        playerTag = "Player";
        guardTag = "Guard";
        doorAnimation = "DoorAnimation";
        doorStates = new string[] { "Broken Door", "Open Door", "Locked Door" };

        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        SetValues();
    }
    void SetValues()
    {
        for (int i = 0; i < doorStates.GetUpperBound(0) + 1; i++)
        {
            if (this.gameObject.tag == doorStates[i])
            {
                value = i;
                SetDoors();
            }
        }
    }
    void SetDoors()
    {
        switch (value)
        {
            case 0:
                this.brokenDoor = true;
                break;
            case 1:
                this.lockedDoor = false;
                break;
            case 2:
                this.lockedDoor = true;
                break;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (this.brokenDoor)
        {
            Debug.Log("This door is broken, you can't pass.");
        }
        else if (!lockedDoor)
        {
            Debug.Log("Door opens.");
            anim.Play(doorAnimation);
        }
        else if (lockedDoor)
        {
            if (col.gameObject.tag == guardTag)
            {
                Debug.Log("You are a guard and can pass any door C:");
                anim.Play(doorAnimation);
            }
            if (col.gameObject.tag == playerTag)
            {
                Debug.Log("This door is locked!");
                //this.keyCollected = playerManager.key;
                if (keyCollected)
                {
                    Debug.Log("Seems like you have a key, let's open the door!");
                    anim.Play(doorAnimation);
                    this.lockedDoor = false;
                  //  playerManager.key = false;
                }
            }
        }
    }
}
