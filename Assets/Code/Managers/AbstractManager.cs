using System.Collections;
using UnityEngine;

public abstract class AbstractManager : MonoBehaviour
{
    public abstract void OnGameStarted();

    public abstract void OnMenu();

    public virtual void OnGameEnded() { }

    protected virtual void Update()
    {
        if (StateBus.GameStateChanged)
        {
            StateBus.StateWorkers[StateBus.GameStateChanged].MakeStaff(this);
        }
    }
}
