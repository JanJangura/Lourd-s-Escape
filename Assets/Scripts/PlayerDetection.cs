using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float rayCastRange = 1f;
    public Transform objectGrabPointTransform; // This is the position of where the object we grab will be held at   
    public GameObject player;
    public GameObject GrabPoint;
    public GameObject KeyPadHierarchy;
    public GameObject NoteHierarchy;
    public GameObject crosshairHierarchy;
    public GameObject lightSwitchHierarchy;
    public GameObject KeyHierarchy;
    public Camera Camera;
    public GameObject Trigger;
    public GameObject InteractiveMsg;

    public AudioSource src;
    public AudioClip lightswitch;
    public AudioClip PickUpItem;


    private ObjectInteractions objectInteractions;
    private DoorInteraction doorInteraction;
    private KeyPadObject keyPad;
    private ReadNotes readNotes;
    private LightSwitch LS;
    private Keys keys;


    [SerializeField]
    private Tags keyPadCheck;
    [SerializeField]
    private Tags noteCheck;

    public LayerMask layermask;

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            RayCastTool();
        }
    }
    void RayCastTool()
    {
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * rayCastRange));
        Debug.DrawRay(transform.position, transform.forward * rayCastRange, Color.red);


        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayCastRange, layermask)) // This gives a raycast to the game object position.
        {
            switch (hitInfo.collider.tag)
            {
                case "ObjectInteraction":
                    DisplayMessage();                   
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GrabbableObject(hitInfo);
                        //Debug.Log("OBJECT");
                    }
                    break;
                case "Doors":
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Doors(hitInfo);
                        // Debug.Log("DOORS");
                    }
                    break;
                case "KeyPad":
                    DisplayMessage();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        KeyPad(hitInfo);
                    }
                    break;
                case "Notes":
                    DisplayMessage();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Notes(hitInfo);
                    }
                    break;
                case "LightSwitch":
                    DisplayMessage();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lightSwitch(hitInfo);
                    }
                    break;
                case "Key":
                    DisplayMessage();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        KeyObject(hitInfo);
                    }
                    break;

                case "":
                    DisplayMessageOff();
                    break;
                default:
                    DisplayMessageOff();
                    break;
            }
        }
    }   

private void GrabbableObject(RaycastHit hitInfo)
    {       
        if (objectInteractions == null)
        {
            src.clip = PickUpItem;
            src.Play();
            // If not carrying an object, try to grab
            if (hitInfo.transform.TryGetComponent(out objectInteractions))
            {
                objectInteractions.Grab(objectGrabPointTransform);
                GrabPoint.SetActive(true);
            }
        }
        else
        {
            // Currently Carrying Something BECAUSE the empty Game object is not NULL, so we want to drop it
            objectInteractions.Drop();
            objectInteractions = null;
            GrabPoint.SetActive(false);
        }
    }

    private void Doors(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out doorInteraction))
        {
            // Debug.Log("cows");          
            DisplayMessageOff();
            doorInteraction.OpenDoor();
        }
    }

    private void KeyPad(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out keyPad))
        {
            DisplayMessageOff();
            if (!keyPad.keyPadUnlocked)
            {
                keyPad.ActivateKeyPad();
            }
        }
    }

    private void Notes(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out readNotes))
        {
            DisplayMessageOff();
            readNotes.inReach = true;
        }
    }

    public void lightSwitch(RaycastHit hitInfo)
    {
        DisplayMessageOff();
        bool isOn = true;
        src.clip = lightswitch;
        src.Play();
        if (isOn && lightSwitchHierarchy.activeInHierarchy)
        {          
            lightSwitchHierarchy.SetActive(false);
        }
        else if (isOn && !lightSwitchHierarchy.activeInHierarchy)
        {
            lightSwitchHierarchy.SetActive(true);
        }
    }

    private void KeyObject(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out keys))
        {
            keys.OPENDOOR = true;
        }
    }

    public void NoteUIEnter()
    {
        DisplayMessageOff();
        crosshairHierarchy.SetActive(false);
        NoteHierarchy.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Camera.GetComponent<MouseLook>().enabled = false;
    }

    public void NoteUIExit()
    {
        NoteHierarchy.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.GetComponent<MouseLook>().enabled = true;
        crosshairHierarchy.SetActive(true);
        readNotes.inReach = false;
    }

    private void DisplayMessage()
    {
        InteractiveMsg.SetActive(true);
        crosshairHierarchy.SetActive(false);
    }

    public void DisplayMessageOff()
    {
        InteractiveMsg.SetActive(false);
        crosshairHierarchy.SetActive(true);
    }
}
