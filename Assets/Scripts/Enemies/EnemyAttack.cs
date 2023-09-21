using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float hitDamage;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask damageableLayer;

    public virtual void Attack()
    {
        // Lanzar un Raycast hacia la derecha desde la posición del objeto
        Vector2 raycastDirection = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastLength, damageableLayer);

        // Si el Raycast golpea algo
        if (hit.collider != null)
        {
            // Intenta obtener el componente IDamageable del objeto golpeado
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            Debug.Log($"{hit.collider.gameObject.name} recived {hitDamage} damage!");
            // Si el objeto golpeado implementa la interfaz IDamageable

            // Llama al método TakeDamage() en el objeto
            damageable?.TakeDamage(hitDamage);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}
