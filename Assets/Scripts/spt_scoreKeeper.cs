using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spt_scoreKeeper : MonoBehaviour {

    //SCORE
    public static int score = 0;
    
    //TIMES
    public static int startingTimeLimit = 30;
    public static float timeRemainingInSeconds = startingTimeLimit;

    //BALL COUNTS
    public static int startingBallCount = 5;
    public static int ballsToSpawn = startingBallCount;
    public static int ballsRemaining = startingBallCount;

    //MODES
    public static bool infiniteMode = false;
    public static bool timeTrialMode = false;
    public static bool gameOver = false;

    public GameObject menuCanvas;
    private Text counterText;

    // Use this for initialization
    void Start () {
        counterText = GetComponent<Text>();
        startingBallCount = 5;
        ballsToSpawn = startingBallCount;
        ballsRemaining = startingBallCount;
        if (timeTrialMode) infiniteMode = true;
        if (gameObject.name == "BallsRemaining" && infiniteMode) counterText.text = "∞";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)) infiniteMode = true;
        //Update machine dispays
        if (gameObject.name == "Score") counterText.text = score.ToString();
        if (gameObject.name == "BallsRemaining" && !infiniteMode) counterText.text = ballsRemaining.ToString();
        if (gameObject.name == "TimeRemaining" && timeTrialMode && timeRemainingInSeconds > 0){
            timeRemainingInSeconds -= Time.deltaTime;
            counterText.text = Mathf.Ceil(timeRemainingInSeconds % 60).ToString();
        }

        //Listen for game over conditions
        if (ballsRemaining == 0 || timeRemainingInSeconds <= 0 || Input.GetKeyDown(KeyCode.Escape)) {
            GameOver();
        }
    }

    void GameOver() {
        gameOver = true;
        if (menuCanvas != null){
            menuCanvas.SetActive(true);
            menuCanvas.transform.FindChild("Final Score").GetComponent<Text>().text = "SCORE: " + score;
        }
    }
}
