public class MenuStateWorker : IStateWorker
{
    public void MakeStaff(AbstractManager callback)
    {
        callback.OnMenu();
    }
}
