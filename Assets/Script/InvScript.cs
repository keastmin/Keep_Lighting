using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvScript : MonoBehaviour
{
    [SerializeField]
    Image InventoryC;

    [SerializeField]
    Image KeyC;

    private bool invenOn = false;
    private bool keyOn = false;
    // Start is called before the first frame update
    void Start()
    {
        InventoryC.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryC.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventoryC.gameObject.SetActive(false);
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            InventoryC.gameObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            InventoryC.gameObject.SetActive(false);
        }
    }
}