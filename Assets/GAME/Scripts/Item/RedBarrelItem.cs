using UnityEngine;

public class RedBarrelItem : Item, IHitable
{
    public void GetHit(float dmg)
    {
        base.TakeDamage(dmg);
    }
}
