using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    public string answer = "1234";
    public GameObject Camera;
    public TextMeshProUGUI textOB;
    public GameObject player;
    public GameObject KeyPadHierarchy;
    public GameObject crosshairHierarchy;
    DoorInteraction door;
    public bool activate = false;
    public GameObject Trigger;

    int count = 0;
    private void Start()
    {
        activate = false;
        KeyPadHierarchy.SetActive(false);
        door = GameObject.FindGameObjectWithTag("Door2").GetComponent<DoorInteraction>();
    }

    private void Update()
    {
        if (textOB.text == "Right" && count < 1)
        {           
            door.inReach = true;
            count = 1;
            logic();
            Exit();
        }
        
        if(Trigger.activeInHierarchy && activate == true)
        {
            crosshairHierarchy.SetActive(false);
            KeyPadHierarchy.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.GetComponent<MouseLook>().enabled = false;
        }
    }

    public void Number(int number)
    {
        if (textOB.text != "Wrong")
        {
            textOB.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (textOB.text == answer)
        {
            textOB.text = "Right";
        }
        else
        {
            textOB.text = "Wrong";
            StartCoroutine(SecondCounter());           
        }
    }
    public void Clear()
    {
        {
            textOB.text = "";
        }
    }

    private IEnumerator SecondCounter()
    {
        yield return new WaitForSeconds(1);
        Clear();
    }

    public void logic()
    {
        this.tag = "NOOPEN";
    }

    public void Exit()
    {
        activate = false;
        Trigger.SetActive(false);
        Clear();
        KeyPadHierarchy.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.GetComponent<MouseLook>().enabled = true;
        crosshairHierarchy.SetActive(true);
    }
}
