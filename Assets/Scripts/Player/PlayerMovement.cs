using UnityEngine;


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
        
        if (moveInput != Vector2.zero)
        {
            playerAnimator.SetFloat("Horizontal", moveX);
            playerAnimator.SetFloat("Vertical", moveY);
        }        

        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude * 10);
    }    

    private void LateUpdate()
    {
        playerRb.MovePosition(playerRb.position + movementSpeed * Time.fixedDeltaTime * moveInput);
    }
    #endregion
}
