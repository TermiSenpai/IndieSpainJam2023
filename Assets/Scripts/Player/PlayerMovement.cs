using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    #region Inspector variables
    [Header("Variables")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float runSpeed = 10f;
    #endregion

    #region Private Variables
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    #endregion

    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    private void LateUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }
    #endregion

    #region Inputs events
    
    #endregion
}
