using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    private int count = 0;
    [SerializeField]
    private Tags tagCheck;

    public AudioSource src;
    public AudioClip dst;

    public GameObject Door;
    DoorInteraction interaction;
    private void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count == 0 && other.TryGetComponent<TagsScript>(out var Tags))
        {
            if (Tags.All.Contains(tagCheck))
            {
                interaction = Door.GetComponent<DoorInteraction>();
                src.clip = dst;
                src.Play();
                interaction.inReach = true;
                count = 1;
            }
        }
    }
}
