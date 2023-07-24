using System.Collections;
using UnityEngine;
using TMPro;

public class ScoresCounter : GUIElement
{
    private TextMeshProUGUI scoresCounter;
    private int currentScores;

    protected override void Start()
    {
        scoresCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (StateBus.CurrentScoresChanged)
        {
            currentScores = StateBus.CurrentScoresChanged;
            scoresCounter.text = currentScores.ToString();
        }
    }
}
