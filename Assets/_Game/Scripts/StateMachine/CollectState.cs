public class CollectState : IState<Enemy>
{
    private float numberBirck;
    public void OnEnter(Enemy enemy)
    {
        enemy.CollectBrick();
    }

    public void OnExecute(Enemy enemy)
    {
        if (!enemy.haveBrick)
        {
            enemy.ChangeState(new MoveToBridgeState());
        }
        else
        {
            enemy.CollectBrick();
        }
    }
    public void OnExit(Enemy eneny)
    {
    }
}
