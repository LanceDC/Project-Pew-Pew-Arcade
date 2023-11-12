using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed;
    public float gravity = -9.81f;
    public float mouseSensetivity;
    public float jumpHeight;
    public AudioSource source;

    [HideInInspector] public CharacterController controller;

     
    private bool onGround;
    private PlayerControls controls;
    private Vector2 cameraMove;
    private Vector2 playerMove;
    private Vector3 velocity;
    private float xRotation = 0f;
    


    [SerializeField] private Transform cam;


    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.MoveCamera.performed += ctx => cameraMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.MoveCamera.canceled += ctx => cameraMove = Vector2.zero;

        controls.Gameplay.Move.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => playerMove = Vector2.zero;

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MovePlayer();


    }

    private void MovePlayer()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        

        float yStore = velocity.y;

        onGround = controller.isGrounded;

        //plays ths sound of the player if walking and not on the ground
        if (vertical != 0f && !source.isPlaying && onGround || horizontal != 0f && !source.isPlaying && onGround)
        { 
            source.Play();
        }

        //Makes sure that the fall speed is constant so the player,
        //cannot just slam into the ground when they walk off a ledge
        if (onGround && yStore < 0f)
        {
            yStore = -2f;
        }

        velocity = (Vector3.forward * vertical + Vector3.right * horizontal);
        velocity = velocity.normalized * moveSpeed;
        velocity.y = yStore;

        velocity.y -= gravity * Time.deltaTime;
        velocity = transform.TransformDirection(velocity);

        controller.Move(velocity * Time.deltaTime);

    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float horizontal = (mouseX + cameraMove.x) * mouseSensetivity;

        float mouseY = Input.GetAxis("Mouse Y");
        float vertical = (mouseY + cameraMove.y) * mouseSensetivity;

        //Rotates the camerea around the x axis so the player can look up and down
        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * horizontal);
    }

    private void Jump()
    {

        if (onGround)
            //calculation of vertical height, only works when negative
            //               Height off jump * time in air * gravity
            velocity.y = Mathf.Sqrt(-jumpHeight * -4f * gravity);
    }

}
