using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FPSController_LWM : MonoBehaviour
{   //LWM
    public CharacterController playerMover;
    public float LookSense = 1000f;
    public Transform pBody;
    float TurnX = 0f;
    public float moveSpeed = 24f;
    public float Gforce = -9.81f;
    Vector3 Velocity;
    public Transform CheckFeet;
    public float FeetSize = 0.4f;
    public LayerMask Socks;
    bool FloorHugger;
    public float JumpHeight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        LockMouse();
    }
    void Update()
    {
        Gravity();
        GroundCheck();
        Jump();
        LookControl();
        Sprint();
        MoveControl();
    }
    void LockMouse() // <- locks the mouse to the game window
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LookControl() // <- controls the camera movement
    {
        float mouseX = Input.GetAxis("Mouse X") * LookSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * LookSense * Time.deltaTime;
        TurnX -= mouseY;
        TurnX = Mathf.Clamp(TurnX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(TurnX, 0f, 0f);
        pBody.Rotate(Vector3.up * mouseX);
    }
    void MoveControl() // <- controls the ground based movement
    {
        float VertiGo = Input.GetAxis("Vertical");
        float HoriGo = Input.GetAxis("Horizontal");
        Vector3 move = transform.right * HoriGo + transform.forward * VertiGo;
        playerMover.Move(move * moveSpeed * Time.deltaTime);
    }
    void Gravity() // <- controls gravity
    {
        Velocity.y += Gforce * Time.deltaTime;
        playerMover.Move(Velocity * Time.deltaTime);
    }
    void GroundCheck() // <- checks for ground
    {
        FloorHugger = Physics.CheckSphere(CheckFeet.position, FeetSize, Socks);
        if (FloorHugger)
        {
            Velocity.y = -1f;
        }
    }
    void Sprint() // <- controls the movement speed when using sprint key
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 48f;
        }
        else
        {
            moveSpeed = 24f;
        }
    }
    void Jump() // <- controls jump and determines jump velocity
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Velocity.y = Mathf.Sqrt((JumpHeight)*(-2)*(Gforce));
        }
    }
}
