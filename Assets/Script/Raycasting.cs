using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{

    private Vector3 ScreenCenter;
    private GameObject LastIneractionObject;
    private bool is_transfered = false;
    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }
    void Update()
    {
        Raycast();
    }
    void Raycast()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Item_Key"))
                {
                    GameObject hitObject;
                    hitObject = hit.collider.gameObject;
                    hitObject.GetComponent<ObjectImageTrue>().GetImage();
                }
                else
                {
                    Poltergeist polter = hit.transform.gameObject.GetComponent<Poltergeist>();
                    if (!is_transfered)
                    {
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
                        polter.MovingObject();
                        is_transfered = true;
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
                        is_transfered = false;
                    }
                }
            }
        }
    }
}
