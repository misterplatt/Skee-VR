using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class spt_returnButton : spt_menuButton
{
    protected override void HandleClick()
    {
        base.HandleClick();
        ReturnToMenu();
    }

    void ReturnToMenu()
    {
        //Reset scorekeeper values
        spt_scoreKeeper.gameOver = false;
        spt_scoreKeeper.timeRemainingInSeconds = spt_scoreKeeper.startingTimeLimit;
        spt_scoreKeeper.score = 0;
        spt_scoreKeeper.ballsRemaining = spt_scoreKeeper.startingBallCount;
        spt_scoreKeeper.ballsToSpawn = spt_scoreKeeper.startingBallCount;
        spt_scoreKeeper.infiniteMode = false;
        spt_scoreKeeper.timeTrialMode = false;

        SceneManager.LoadScene("MainMenu");
    }
}
