public class GameEndStateWorker : IStateWorker
{
    public void MakeStaff(AbstractManager callback)
    {
        callback.OnGameEnded();
    }
}
