using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject NoteHierarchy;
    public bool inReach = false;

    private PlayerDetection Player;
    void Start()
    {
        NoteHierarchy.SetActive(false);
        inReach = false;
        Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerDetection>();
    }

    private void Update()
    {
        if (inReach == true)
        {
            Player.NoteUIEnter();
        }       
    }
}
