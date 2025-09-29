

public abstract class EnemyStateBase
{
    protected EnemyGrunt _enemyGrunt;
    protected EnemyStateBase(EnemyGrunt enemyGrunt)
    {
        _enemyGrunt = enemyGrunt;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void OnExit()
    {

    }
}
