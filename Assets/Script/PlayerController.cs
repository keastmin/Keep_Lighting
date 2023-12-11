using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpPower = 15;
    public float gravity = -50;
    public float rotSpeed = 100;
    float yVelocity = 0;
    float camAngle;
    float bodyAngle;
    //test1
    CharacterController cc;

    public Transform camera;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 플레이어 이동
        RotateCamera();
        //좌우는 몸체 회전 하고 싶다.
        RotateBody();
        //사용자 입력에 따라 이동하고 싶다.
        Move();
    }

    private void RotateCamera()
    {
        float value = Input.GetAxis("Mouse Y");
        camAngle += value * rotSpeed * Time.deltaTime;
        camAngle = Mathf.Clamp(camAngle, -40, 25);

        camera.localEulerAngles = new Vector3(-camAngle, 0, 0);
    }
    //좌우는 몸체 회전 하고 싶다.
    private void RotateBody()
    {
        float value = Input.GetAxis("Mouse X");
        bodyAngle += value * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, bodyAngle, 0);
    }
    private void Move()
    {
        //사용자 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //방향이 필요
        Vector3 dir = new Vector3(h, 0, v) * speed;
        //카메라가 바라보는 방향으로 방향 전환
        dir = camera.TransformDirection(dir);

        // 바닥에 있으면 수직 속도를 0으로 하자
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        //중력을 적용하고 싶다.
        //v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        //이동하고 싶다.
        cc.Move(dir * Time.deltaTime);
    }
}
