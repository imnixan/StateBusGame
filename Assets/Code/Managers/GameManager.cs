using System.Collections;
using UnityEngine;

public class GameManager : AbstractManager
{
    private bool inGame;
    private bool inGameEnd;
    private int CurrentScores;
    private Rocket rocket;

    private void Start()
    {
        rocket = GetComponentInChildren<Rocket>();
    }

    public override void OnGameStarted()
    {
        CurrentScores = 0;
        inGame = true;
        inGameEnd = false;
    }

    public override void OnMenu()
    {
        inGame = false;
        inGameEnd = false;
        rocket.ReturnOnStart();
    }

    public override void OnGameEnded()
    {
        inGame = false;
        inGameEnd = true;
        rocket.ReturnOnStart();
    }

    private void CheckRecord()
    {
        int finalScore = StateBus.CurrentScoresChanged;
        if (finalScore > PlayerPrefs.GetInt("Record"))
        {
            PlayerPrefs.SetInt("Record", finalScore);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape) && (inGame || inGameEnd))
        {
            StateBus.GameStateChanged += StateMachine.GameStates.Menu;
        }

        if (inGame && StateBus.EnemyKilled)
        {
            CurrentScores++;
            StateBus.CurrentScoresChanged += CurrentScores;
            CheckRecord();
        }
    }
}
