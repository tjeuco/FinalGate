using UnityEngine;

public class TurretItem : Item, IHitable
{
    public void GetHit(float dmg)
    {
        base.TakeDamage(dmg);
    }
}
