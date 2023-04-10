using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public GameObject door;
    public GameObject key;
    DoorInteraction DI;
    public bool OPENDOOR = false;
    public AudioSource src;
    public AudioClip dst;

    private void Start()
    {
        DI = door.GetComponent<DoorInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OPENDOOR == true)
        {
            key.SetActive(false);
            src.clip = dst;
            src.Play();
        }
        if(OPENDOOR == true && !key.activeInHierarchy)
        {
            DI.inReach = true;
        }
    }
}
