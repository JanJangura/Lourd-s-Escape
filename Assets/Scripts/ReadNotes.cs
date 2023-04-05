using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject Camera;
    public GameObject player;
    public GameObject noteUI;
    public GameObject crosshairHierarchy;

    public bool inReach;

    void Start()
    {
        noteUI.SetActive(false);
        inReach = false;
    }

    private void Update()
    {
        if (inReach == true)
        {
            crosshairHierarchy.SetActive(false);
            noteUI.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.GetComponent<MouseLook>().enabled = false;
        }
    }

    public void exitNote()
    {
        noteUI.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.GetComponent<MouseLook>().enabled = true;
        inReach = false;
    }
}
