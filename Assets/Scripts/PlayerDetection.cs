using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float rayCastRange = 1f;
    public Transform objectGrabPointTransform; // This is the position of where the object we grab will be held at   
    public GameObject GrabPoint;
    public GameObject KeyPadHierarchy;
    public GameObject NoteHierarchy;

    private ObjectInteractions objectInteractions;
    private DoorInteraction doorInteraction;
    private KeyPad keyPad;
    private ReadNotes readNotes;

    public GameObject crosshairHierarchy;

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
            if (Input.GetKeyDown(KeyCode.E))
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
            KeyPadHierarchy.SetActive(true);            
        }
    }

    private void Notes(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out readNotes))
        {
            crosshairHierarchy.SetActive(false);
            readNotes.inReach = true;
        }
    }
}
