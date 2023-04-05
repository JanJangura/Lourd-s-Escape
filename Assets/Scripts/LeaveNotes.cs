using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeaveNotes : MonoBehaviour
{
    public GameObject crosshairHierarchy;
    public void ExitNote()
    {
        ReadNotes readNotes = GameObject.FindGameObjectWithTag("Notes").GetComponent<ReadNotes>();
        readNotes.exitNote();
        crosshairHierarchy.SetActive(true);
    }
}
