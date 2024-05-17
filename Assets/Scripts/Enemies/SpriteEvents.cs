using UnityEngine;

public class SpriteEvents : MonoBehaviour
{
    AttackState state; // Reference to the AttackState script

    private void Awake() => state = GetComponentInParent<AttackState>(); // Get the AttackState component from the parent GameObject

    // Method to play the emerge sound
    public void PlayEmergeSound() => state.PlayEmergeSound(); // Call the PlayEmergeSound method in the AttackState script

    // Method to execute the attack
    public void Attack() => state.Attack(); // Call the Attack method in the AttackState script

    // Method called when the attack animation finishes
    public void OnFinishAttack() => state.OnFinishAttack(); // Call the OnFinishAttack method in the AttackState script
}
