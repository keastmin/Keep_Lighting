using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 3f; // Raycast �ִ� �Ÿ�
    public Transform controllerTransform; // ��Ʈ�ѷ� Transform

    private LineRenderer lineRenderer; // ���� ������ ����
    private GameObject lastInteractionObject; // ���������� ��ȣ�ۿ��� ������Ʈ

    private Vector3 rayStart; // Ray�� ����
    private Vector3 rayEnd; // Ray�� ��

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ���ο� ���� ray ���� ����
        rayStart = controllerTransform.position + controllerTransform.forward * 0.1f; // �ణ�� ������ �߰�
        rayEnd = rayStart + controllerTransform.forward * maxDistance;

        RaycastHit hit;
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray�� ������Ʈ�� ����� ���

            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Flash"))
            {
                GameObject hitObject = hit.collider.gameObject;
                hitObject.GetComponent<ItemInteraction>().TurnOnInteraction();

                if(lastInteractionObject != null && lastInteractionObject != hitObject)
                {
                    lastInteractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                }

                lastInteractionObject = hitObject;

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    if (hitObject.CompareTag("Item"))
                    {
                        hitObject.GetComponent<ObjectImageTrue>().GetImage();
                        Destroy(hitObject);
                    }
                    else if(hitObject.CompareTag("Flash"))
                    {
                        hitObject.GetComponent<FlashInteraction>().PickupFlash();
                        controllerTransform = hitObject.transform.parent;
                    }
                }
            }
            else
            {
                if (lastInteractionObject != null)
                {
                    lastInteractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                    lastInteractionObject = null;
                }
            }
        }
        else
        {
            if(lastInteractionObject != null)
            {
                lastInteractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                lastInteractionObject = null;
            }
        }
        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, rayEnd);
    }
}
