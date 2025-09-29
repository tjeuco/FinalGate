using UnityEngine;

public class BoxItem : Item, IHitable
{
    public void GetHit(float dmg)
    {
        base.TakeDamage(dmg);
    }
}
