using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //настройки передвижения 
    public float forwardSpeed = 10f;
    public float laneSwitchSpeed = 15f;
    public float laneWidth = 2f;
    public float IncreaseSpeed = 1f;
    public float IncreaseCoef = 1.01f;

    //Настройки прыжка
    public float jumpHeight = 4f;
    public float jumpDuration = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;

    private int currentLane = 1;
    private float targetX;
    private bool isJumping = false;
    private bool isGrounded = true;
    private float jumpStartTime;
    private float jumpStartY;
    private Rigidbody rb;
    private float timeLeft = 0f;

    void Start()
    {
        targetX = transform.position.x;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Update()
    {
        timeLeft += Time.deltaTime;
        if(timeLeft > IncreaseSpeed)
        {
            timeLeft = 0f;
            forwardSpeed *= IncreaseCoef;
        }
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        CheckGround();
        HandlePCInput();
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(newPosition.x, targetX, laneSwitchSpeed * Time.deltaTime);
        transform.position = newPosition;
        if (isJumping)
        {
            HandleJump();
        }
    }

    void CheckGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        isGrounded = hitColliders.Length > 0;
    }

    void HandlePCInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            targetX = (currentLane - 1) * laneWidth;
        }
    }

    void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            targetX = (currentLane - 1) * laneWidth;
        }
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            isGrounded = false;
            jumpStartTime = Time.time;
            jumpStartY = transform.position.y;
        }
    }

    void HandleJump()
    {
        float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;
        if (jumpProgress < 1f) //Красиво летим вверх
        {
            float newY = jumpStartY + Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
            float newX = Mathf.Lerp(transform.position.x, targetX, laneSwitchSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
        else
        {
            isJumping = false; 
            if (!isGrounded) //падаем вниз до приземления
            {
                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(pos.y, jumpStartY, 5f * Time.deltaTime);
                transform.position = pos;
            }
        }
    }
}