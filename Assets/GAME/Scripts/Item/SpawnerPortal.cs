using UnityEngine;

public class SpawnerPortal : Item,IHitable
{
    public void GetHit(float dmg)
    {
        base.TakeDamage(dmg);
    }
}
