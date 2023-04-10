using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private PlayerDetection Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerDetection>();
    }
    public void NoteUIExit()
    {
        Player.NoteUIExit();
    }
}
