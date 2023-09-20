using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    IdleDown,
    DownLeft,
    IdleLeft,
    UpLeft,
    IdleUp,
    UpRight,
    IdleRight,
    DownRight
}

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    #region Inspector variables
    [Header("Variables")]
    [SerializeField] float movementSpeed = 5f;
    #endregion

    #region Private Variables
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;
    private Direction direction = Direction.IdleDown;
    #endregion

    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
        
        if (moveInput != Vector2.zero)
        {

            playerAnimator.SetFloat("Horizontal", moveX);
            playerAnimator.SetFloat("Vertical", moveY);
        }

        IdleDirection();
    }

    private void IdleDirection()
    {
        if (moveInput != Vector2.zero)
            direction = DetermineDirection(moveInput);


    }

    private Direction DetermineDirection(Vector2 input)
    {
        // Calcula el ángulo en radianes del vector de entrada
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;

        // Mapea el ángulo a una dirección
        if (angle >= -22.5f && angle < 22.5f)
        {
            return Direction.IdleRight;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            return Direction.UpRight;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            return Direction.IdleUp;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            return Direction.UpLeft;
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            return Direction.IdleLeft;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            return Direction.DownLeft;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            return Direction.IdleDown;
        }
        else
        {
            return Direction.DownRight;
        }
    }

    private void LateUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }
    #endregion
}
