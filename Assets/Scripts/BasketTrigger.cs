using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    int appleCount = 0;
    public int totalAppleNeeded = 10;

    [SerializeField]
    private Tags tagCheck;

    private List<ObjectInteractions> realApples = new List<ObjectInteractions>();

    public GameObject doorObject;
    DoorInteraction door;

    private void Start()
    {
        appleCount = 0;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.TryGetComponent<TagsScript>(out var Tags))
        {
            if(Tags.All.Contains(tagCheck)) // This checks if the list contains a scriptable object with the assigned scriptable object RealApples
            {
                ObjectInteractions Apple = hitInfo.GetComponent<ObjectInteractions>();
                if(Apple) {
                    realApples.Add(Apple);
                }
            }                    
        }        
    }

    private void OnTriggerExit(Collider hitInfo)
    {
        if (hitInfo.TryGetComponent<TagsScript>(out var Tags))
        {
            if (Tags.All.Contains(tagCheck)) // This checks if the list contains a scriptable object with the assigned scriptable object RealApples
            {
                ObjectInteractions Apple = hitInfo.GetComponent<ObjectInteractions>();
                if (Apple)
                {
                    realApples.Remove(Apple);
                }
            }
        }
    }

    void Update()
    {
        if (realApples.Count > 0)
        {
            foreach (var apple in realApples)
            {
                if (apple.isDropped && !apple.isCounted)
                {
                    apple.isCounted = true;
                    appleCount++;
                    if (appleCount == totalAppleNeeded)
                    {
                        door = doorObject.GetComponent<DoorInteraction>();
                        if (door) door.OpenDoor();
                    }
                }
            }
        }
    }
}

