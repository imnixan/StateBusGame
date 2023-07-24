using System.Collections;
using UnityEngine;
using TMPro;

public class TimeManager : AbstractManager
{
    private const int MaxTime = 60;
    private TextMeshProUGUI timeCounter;
    private WaitForSecondsRealtime waitForSeconds;

    private void Start()
    {
        timeCounter = GetComponent<TextMeshProUGUI>();
        waitForSeconds = new WaitForSecondsRealtime(1);
    }

    public override void OnGameStarted()
    {
        StartCoroutine(StartTimer());
    }

    public override void OnMenu()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartTimer()
    {
        for (int secondsLeft = MaxTime; secondsLeft >= 0; secondsLeft--)
        {
            timeCounter.text = secondsLeft.ToString();
            yield return waitForSeconds;
        }
        StateBus.GameStateChanged += StateMachine.GameStates.GameEnd;
    }
}
