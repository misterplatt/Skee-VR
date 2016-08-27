using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class spt_restartButton : spt_menuButton
{
    protected override void HandleClick()
    {
        base.HandleClick();
        rebootMachine();
    }

    void rebootMachine() {
        //Destroy all skeeballs
        GameObject[] skeeballs = GameObject.FindGameObjectsWithTag("skeeBall");
        for (int i = 0; i < skeeballs.Length; i++) {
           Destroy(skeeballs[i]);
        }
        //Reset scorekeeper values
        spt_scoreKeeper.gameOver = false;
        spt_scoreKeeper.timeRemainingInSeconds = spt_scoreKeeper.startingTimeLimit;
        spt_scoreKeeper.score = 0;
        spt_scoreKeeper.ballsRemaining = spt_scoreKeeper.startingBallCount;
        spt_scoreKeeper.ballsToSpawn = spt_scoreKeeper.startingBallCount;
        //Disable the menu canvas
        transform.parent.gameObject.SetActive(false);
    }
}