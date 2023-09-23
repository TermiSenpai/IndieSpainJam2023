using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] Transform AttackPoint;

    private Animator anim;
    Vector2 mousePos;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordAttack();
        }
    }

    private void SwordAttack()
    {
        anim.SetFloat("Horizontal", mousePos.x);
        anim.SetFloat("Vertical", mousePos.y);

        anim.SetTrigger("SwordAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.raidusRange, stats.enemyLayer);

        // RaycastHit2D enemyHit = Physics2D.Raycast(AttackPoint.position, Vector2.right, stats.raidusRange, stats.enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {

            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        //Gizmos.DrawRay(AttackPoint.position, Vector2.right);
        Gizmos.DrawWireSphere(AttackPoint.position, stats.raidusRange);
    }

}
