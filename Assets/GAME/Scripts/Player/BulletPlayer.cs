using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bulletEffect;
    [SerializeField] private float speedBullet = 10f;
    [SerializeField] private float damageBullet = 15f;
    [SerializeField] private float maxDistance = 50f;

    private Vector3 moveDirection;
    private float posStart;
    

    private void Awake()
    {
        
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }
    public float GetDamageBulletPlayer()
    {
        return damageBullet;
    }
    void Update()
    {
        if (Mathf.Abs(this.transform.position.x - posStart) > maxDistance)
        {
            DisableBullet();
            this.gameObject.SetActive(false);
        }
        MoveBullet();
    }
    void MoveBullet()
    {
        this.transform.Translate(moveDirection * speedBullet * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this.gameObject.SetActive(false);
        if (collision.TryGetComponent<IHitable>(out var ICanHit))
        {
            if (ICanHit != null)
            {
                this.gameObject.SetActive(false);
                ICanHit.GetHit(damageBullet);
                GameObject effect = Instantiate(bulletEffect, this.transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }
            
        }
     }

    public void SetDirectionBullet(Vector3 direction)
    {
        posStart = this.transform.position.x;
        moveDirection = direction;
    }
}
