using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator door;
    public bool inReach;

    private void Start()
    {
        inReach = false;
    }

    private void Update()
    {
        if(inReach == true)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("Activate");
        inReach = false;
    }
}
