public class GameStateWorker : IStateWorker
{
    public void MakeStaff(AbstractManager callback)
    {
        callback.OnGameStarted();
    }
}
