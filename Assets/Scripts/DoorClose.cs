using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorClose : MonoBehaviour
{
    //[SerializeField]
    //private Tags tagCheck;

    public AudioSource src;
    public AudioClip dst;

    public GameObject Door;
    public DoorInteraction interaction;

    bool doOnce;
    private void Start()
    {
        doOnce = false;
        interaction = Door.GetComponent<DoorInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (interaction && !doOnce)
            {
                src.clip = dst;
                src.Play();
                interaction.OpenDoor();
                doOnce = true;
            }
        }
    }
}
