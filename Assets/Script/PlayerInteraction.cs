using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 3f; // Raycast 최대 거리
    public Transform controllerTransform; // 컨트롤러 Transform

    private LineRenderer lineRenderer; // 라인 렌더러 참조
    private GameObject lastInteractionObject; // 마지막으로 상호작용한 오브젝트

    private Vector3 rayStart; // Ray의 시작
    private Vector3 rayEnd; // Ray의 끝

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 손전등 여부에 따른 ray 방향 조정
        rayStart = controllerTransform.position + controllerTransform.forward * 0.1f; // 약간의 오프셋 추가
        rayEnd = rayStart + controllerTransform.forward * maxDistance;

        RaycastHit hit;
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray가 오브젝트에 닿았을 경우

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
