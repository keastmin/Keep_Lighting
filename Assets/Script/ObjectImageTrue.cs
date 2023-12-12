using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectImageTrue : MonoBehaviour
{
    [SerializeField]
    Image objImage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(objImage.name);
        if(objImage == null)
        {
            Debug.Log("≥Œ¿”");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetImage()
    {
        Debug.Log("GetImage Ω««€");
        GetComponent<InvScript>().ImageTrue(objImage);
    }
}
