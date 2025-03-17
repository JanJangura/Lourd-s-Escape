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
    public bool activate = false;
    public GameObject Trigger;
    PlayerDetection PD;
    public AudioSource src;
    public AudioClip buttons;
    public GameObject doorObject;
    DoorInteraction door;
    public GameObject KPObject;
    KeyPadObject KPScript;

    int count = 0;
    private void Start()
    {
        activate = false;
        KeyPadHierarchy.SetActive(false);
        KPScript = KPObject.GetComponent<KeyPadObject>();
        door = doorObject.GetComponent<DoorInteraction>();
        PD = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerDetection>();
    }

    private void Update()
    {
        if (textOB.text == "Right" && count < 1)
        {           
            if (door)
            {
                door.OpenDoor();
                count = 1;
                logic();
                if (KPScript) KPScript.keyPadUnlocked = true;
                Exit();
            }        
        }

        if(Trigger.activeInHierarchy && activate)
        {
            PD.DisplayMessageOff();
            crosshairHierarchy.SetActive(false);
            KeyPadHierarchy.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.GetComponent<MouseLook>().enabled = false;
        }
    }

    public void KeyPadInteraction()
    {
        Trigger.SetActive(true);
        activate = true;
    }

    public void Number(int number)
    {
        if (textOB.text != "Wrong")
        {
            src.clip = buttons;
            src.Play();
            textOB.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (textOB.text == answer)
        {
            textOB.text = "Right";
            src.clip = buttons;
            src.Play();
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
        Cursor.lockState = CursorLockMode.Locked;
        Camera.GetComponent<MouseLook>().enabled = true;
        crosshairHierarchy.SetActive(true);
        Debug.Log("EXIT IS CALLED");
        Debug.Log(this);
    }
}
