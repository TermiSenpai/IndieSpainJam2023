
using UnityEngine;

public class PlayerMovement : IMovementStrategy
{
    private readonly Rigidbody2D playerRb;

    public PlayerMovement(Rigidbody2D rb)
    {
        playerRb = rb;
    }

    public void Move(Vector2 direction)
    {
        playerRb.MovePosition(playerRb.position + direction * Time.fixedDeltaTime * PlayerStats.Instance.moveSpeed);
    }
}
