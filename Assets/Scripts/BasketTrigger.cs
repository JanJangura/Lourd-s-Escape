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

    private void Start()
    {
        appleCount = 0;
        Debug.Log(appleCount);
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.TryGetComponent<TagsScript>(out var Tags))
        {
            if(Tags.All.Contains(tagCheck)) // This checks if the list contains a scriptable object with the assigned scriptable object RealApples
            {
                appleCount += 1;
                Debug.Log(appleCount);
                Debug.Log(tagCheck);
                if (appleCount == totalAppleNeeded)
                {
                    DoorInteraction door = GameObject.FindGameObjectWithTag("Door1").GetComponent<DoorInteraction>();
                    door.inReach = true;
                }
            }                    
        }        
    }

    private void OnTriggerExit(Collider hitInfo)
    {
        if (hitInfo.TryGetComponent<TagsScript>(out var tags))
        {
            appleCount--;
        }
    }
}
