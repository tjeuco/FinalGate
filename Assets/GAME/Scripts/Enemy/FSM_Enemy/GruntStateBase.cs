

public abstract class GruntStateBase
{
    protected EnemyGrunt _enemyGrunt;
    protected GruntStateBase(EnemyGrunt enemyGrunt)
    {
        _enemyGrunt = enemyGrunt;
    }

    public abstract void OnEnter();

    public abstract void Execute();

    public abstract void OnExit();
}
