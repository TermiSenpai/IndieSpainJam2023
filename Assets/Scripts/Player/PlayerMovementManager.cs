using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private IMovementStrategy movementStrategy;
    private Animator playerAnimator;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        movementStrategy = new PlayerMovement(playerRb);
    }

    private void Update()
    {
        ReadInput();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ReadInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    private void UpdateAnimation()
    {
        playerAnimator.SetFloat("Horizontal", moveInput.x);
        playerAnimator.SetFloat("Vertical", moveInput.y);
        playerAnimator.SetBool("IsMoving", moveInput != Vector2.zero);
    }

    private void MovePlayer()
    {
        movementStrategy.Move(moveInput);
    }
}
