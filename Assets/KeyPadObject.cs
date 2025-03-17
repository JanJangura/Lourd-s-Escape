using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadObject : MonoBehaviour
{
    public GameObject KeyPadHierarchy;
    KeyPad keyPadScript;
    public bool keyPadUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        keyPadScript = KeyPadHierarchy.GetComponent<KeyPad>();
        keyPadUnlocked = false;
    }

    public void ActivateKeyPad()
    {
        KeyPadHierarchy.SetActive(true);
        if (keyPadScript)
        {
            KeyPadHierarchy.SetActive(true);
            keyPadScript.KeyPadInteraction();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
