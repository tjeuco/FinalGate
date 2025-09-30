

public abstract class GruntStateBase
{
    protected EnemyGrunt enemyGrunt;
    protected GruntStateBase(EnemyGrunt enemyGrunt)
    {
        this.enemyGrunt = enemyGrunt;
    }

    public abstract void OnEnter();

    public abstract void Execute();

    public abstract void OnExit();
}
