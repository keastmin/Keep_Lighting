using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 3f; // Raycast 최대 거리
    public Transform controllerTransform; // 컨트롤러 Transform

    private LineRenderer lineRenderer; // 라인 렌더러 참조
    private GameObject lastIneractionObject; // 마지막으로 상호작용한 오브젝트
    private bool flashCheck = false; // 손전등을 주운 여부

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
        if (!flashCheck)
        {
            rayStart = controllerTransform.position + controllerTransform.forward * 0.1f; // 약간의 오프셋 추가
            rayEnd = rayStart + controllerTransform.forward * maxDistance;
        }
        //else
        //{
        //    rayStart = controllerTransform.position + controllerTransform.up * 0.1f;
        //    rayEnd = rayStart + controllerTransform.up * maxDistance;
        //}

        RaycastHit hit;
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray가 오브젝트에 닿았을 경우

            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Flash"))
            {
                GameObject hitObject = hit.collider.gameObject;
                hitObject.GetComponent<ItemInteraction>().TurnOnInteraction();

                if(lastIneractionObject != null && lastIneractionObject != hitObject)
                {
                    lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                }

                lastIneractionObject = hitObject;

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    if (hitObject.CompareTag("Item"))
                    {
                        Destroy(hitObject);
                    }
                    else if(hitObject.CompareTag("Flash"))
                    {
                        hitObject.GetComponent<FlashInteraction>().PickupFlash();
                        controllerTransform = hitObject.GetComponentInParent<Transform>();
                        //flashCheck = true;
                    }
                }
            }
            else
            {
                if (lastIneractionObject != null)
                {
                    lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                    lastIneractionObject = null;
                }
            }
        }
        else
        {
            if(lastIneractionObject != null)
            {
                lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                lastIneractionObject = null;
            }
        }
        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, rayEnd);
    }
}
