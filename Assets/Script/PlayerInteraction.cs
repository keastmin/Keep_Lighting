using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject _obj; // 생성할 오브젝트
    public float maxDistance = 3f; // Raycast 최대 거리
    public Transform controllerTransform; // 컨트롤러 Transform

    private LineRenderer lineRenderer; // 라인 렌더러 참조
    private GameObject lastIneractionObject; // 마지막으로 상호작용한 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(_obj == null)
        {
            _obj = GetComponent<GameObject>();
            Debug.LogError("게임 오브젝트가 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayStart = controllerTransform.position + controllerTransform.forward * 0.2f; // 약간의 오프셋 추가
        RaycastHit hit;
        Vector3 rayEnd = rayStart + controllerTransform.forward * maxDistance;
        
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray가 오브젝트에 닿았을 경우

            if (hit.collider.CompareTag("Item"))
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
                    DestroyInteractWithObject(hitObject);
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


        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            CreateObjectInDirection();
        }
    }

    void DestroyInteractWithObject(GameObject hitObject)
    {
        Destroy(hitObject);
    }

    void CreateObjectInDirection()
    {
        Vector3 rayStart = controllerTransform.position + controllerTransform.forward * 0.2f; // 약간의 오프셋 추가
        RaycastHit hit;
        if (Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            Vector3 createPosition = hit.point + hit.normal * 0.1f;
            Instantiate(_obj, createPosition, Quaternion.identity);
        }
    }
}
