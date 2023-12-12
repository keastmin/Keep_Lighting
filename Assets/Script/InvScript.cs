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

    [SerializeField]
    Image Key2C;

    [SerializeField]
    Image HammerC;


    private bool KeyS = false;
    private bool Key2S = false;
    private bool HammerS = false;

    // Start is called before the first frame update
    void Start()
    {
        InventoryC.gameObject.SetActive(false);
        KeyC.gameObject.SetActive(false);
        Key2C.gameObject.SetActive(false);
        HammerC.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            InventoryC.gameObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            InventoryC.gameObject.SetActive(false);
        }
        
        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            if(KeyS == false)
            {
                KeyC.gameObject.SetActive(true);
                KeyS = true;
            }
            else
            {
                KeyC.gameObject.SetActive(false);
                KeyS = false;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.Four))
        {
            if (Key2S == false)
            {
                Key2C.gameObject.SetActive(true);
                Key2S = true;
            }
            else
            {
                Key2C.gameObject.SetActive(false);
                Key2S = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (HammerS == false)
            {
                HammerC.gameObject.SetActive(true);
                HammerS = true;
            }
            else
            {
                HammerC.gameObject.SetActive(false);
                HammerS = false;
            }
        }
    }
}