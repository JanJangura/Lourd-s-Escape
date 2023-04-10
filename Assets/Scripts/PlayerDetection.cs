using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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
    public Camera Camera;
    public GameObject Trigger;

    private ObjectInteractions objectInteractions;
    private DoorInteraction doorInteraction;
    private KeyPad keyPad;
    private ReadNotes readNotes;


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


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayCastRange, layermask)) // This gives a raycast to the game object position.
            {
                switch (hitInfo.collider.tag)
                {
                    case "ObjectInteraction":
                        GrabbableObject(hitInfo);
                        //Debug.Log("OBJECT");
                        break;
                    case "Doors":
                        Doors(hitInfo);
                        // Debug.Log("DOORS");
                        break;
                    case "KeyPad":
                        KeyPad(hitInfo);
                        break;
                    case "Notes":
                        Notes(hitInfo);
                        break;
                    default:
                        break;
                }
            }
        }
    }   

private void GrabbableObject(RaycastHit hitInfo)
    {       
        if (objectInteractions == null)
        {           
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
            doorInteraction.inReach = true;
        }

    }

    private void KeyPad(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out keyPad))
        {
            crosshairHierarchy.SetActive(false);
            keyPad.activate = true;
            Trigger.SetActive(true);
        }
    }

    private void Notes(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out readNotes))
        {
            readNotes.inReach = true;
        }
    }

    public void NoteUIEnter()
    {
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
}
