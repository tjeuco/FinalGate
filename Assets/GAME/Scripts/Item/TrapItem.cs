using UnityEngine;

public class TrapItem : MonoBehaviour
{
    [SerializeField] private float damageStay = 5f;
    [SerializeField] private float damageEnter = 10f;
    [SerializeField] private float forceBounce = 1000f;
    private PlayerControl player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trap");
            collision.TryGetComponent<PlayerControl>(out player);
            if (player == null) return;

            player.GetComponent<HealthManager>().TakeDamage(damageEnter);
            //Vector2 forceDirection = (player.transform.position - this.transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceBounce);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<HealthManager>().TakeDamage(damageStay * Time.deltaTime);
        }
    }

}
