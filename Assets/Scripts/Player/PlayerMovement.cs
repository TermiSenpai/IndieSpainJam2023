using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    #region Inspector variables
    [Header("Variables")]
    [SerializeField] float movementSpeed = 5f;
    public Tilemap fogTilemap;
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

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    private void LateUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
        UpdateFog();
    }

    private void UpdateFog()
    {
        Vector3Int currentPlayerPos = fogTilemap.WorldToCell(transform.position);
        for(int i= -3; i <= 3; i++)
        {
            for(int j = -3; j <= 3; j++)
            {
                fogTilemap.SetTile(currentPlayerPos + new Vector3Int(i, j, 0), null);
            }
        }
    }

    #endregion
}
