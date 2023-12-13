using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // 현재 소지 중인 아이템
    public bool KeyS;
    public bool Key2S;
    public bool HammerS;

    // Start is called before the first frame update
    void Start()
    {
        InventoryC.gameObject.SetActive(false);
        KeyC.gameObject.SetActive(false);
        Key2C.gameObject.SetActive(false);
        HammerC.gameObject.SetActive(false);
        KeyS = false;
        Key2S = false;
        HammerS = false;
    }

    public void ImageTrue(Image img)
    {
        if (img == KeyC)
        {
            KeyC.gameObject.SetActive(true);
            KeyS = true;
            Debug.Log("정문 열쇠 소지");
        }
        else if (img == Key2C)
        {
            Key2C.gameObject.SetActive(true);
            Key2S = true;
            Debug.Log("그림방 열쇠 소지");
        }
        else if (img == HammerC)
        {
            HammerC.gameObject.SetActive(true);
            HammerS = true;
            Debug.Log("망치 소지");
        }
        else
        {
            Debug.Log("img : " + img);
        }
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
    }
}