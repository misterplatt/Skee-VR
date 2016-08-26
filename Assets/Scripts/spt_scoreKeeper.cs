using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spt_scoreKeeper : MonoBehaviour {

    public static int score = 0;
    public static int ballsRemaining = 5;
    public static bool infiniteMode = true;
    private Text counterText;

	// Use this for initialization
	void Start () {
        counterText = GetComponent<Text>();
        if (gameObject.name == "BallsRemaining" && infiniteMode) counterText.text = "∞";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)) infiniteMode = !infiniteMode;
        if(gameObject.name == "Score") counterText.text = score.ToString();
        if (gameObject.name == "BallsRemaining" && !infiniteMode) counterText.text = ballsRemaining.ToString();
    }
}
