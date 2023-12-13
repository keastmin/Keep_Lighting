using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;

    public CharacterController characterController;

    public float moveSpeed = 10f;
    public float gravity = -20f;
    public float yVelocity = 0;

    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;

    void Update()
    {
        // 키보드
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        yVelocity += (gravity * Time.deltaTime);
        moveDirection.y = yVelocity;
        characterController.Move(moveDirection * Time.deltaTime);

        // 마우스
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        rotationY += mouseMoveX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveY * sensitivity * Time.deltaTime;

        if (rotationX > 60f)
        {
            rotationX = 60f;
        }
        if(rotationX < -60f)
        {
            rotationX = -60f;
        }
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}