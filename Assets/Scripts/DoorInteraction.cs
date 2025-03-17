using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator door;
    public AudioSource src;
    public AudioClip dst;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OpenDoor()
    {
        src.clip = dst;
        src.Play();
        GetComponent<Animator>().SetTrigger("Activate");
    }
}
