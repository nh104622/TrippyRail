//==============================================================================	
//Interface Script
//
//==============================================================================
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Interface : MonoBehaviour {
	//==========================================================================
	public PlayerShip playerScript;
	private int totalScores=9;
	public GameObject player;
	private int playerScore;
	public bool gameOver=false;
	public bool lost=false;
	private string pName="";    // hold player name for adding score
	private bool nameSubmitted = false;	// check if player already submitted name
	public int currReset=0;
	public GameObject PlayAgainMenu;    // hold reference to Canvas for play again menu
	public GameObject HighScoreMenu;    // Holds reference to high score menu
	public HighScore highScoreScript;	// Holds referenc to high score script to call 'AddScore'
	


	//==========================================================================
	void Start () {

	}
	
	//--------------------------------------------------------------------------
	void Update () {

		if (playerScript != null)
			playerScore = playerScript.score;
		
		if (playerScript == null && (!gameOver && !lost)) { // if player has been destroyed and we have not entered into the gameover or lost state
			HandleGameOver();
		}
	}

	void HandleGameOver()
    {
		if (HighScore())	// if they got a high score
		{
			gameOver = true;
			PlayAgainMenu.SetActive(true);  // enable game over panel
			HighScoreMenu.SetActive(true);  // Enable high score panel
		}
		else
		{
			lost = true;
			PlayAgainMenu.SetActive(true);  // enable game over panel
		}
	}

	//--------------------------------------------------------------------------
	bool HighScore() {
		
		bool highScore = false;
		for (int i=0; i<totalScores; i++) {
			if (playerScore>=PlayerPrefs.GetInt (i+"HScore"))
				highScore=true;
		}
		
		return highScore;
	}


	// Called by UI functions to reset game
	public void ResetGame(int scene)
    {
		SceneManager.LoadScene(scene);	// reset game
    }

	public void GetPlayerName(string name)
    {
		pName = name;
    }



	//--------------------------------------------------------------------------
	void OnGUI() {
		if (pName != "" && !nameSubmitted)  // Prevent name from being submitted more than once on gui change
		{
			nameSubmitted = true;
			highScoreScript.AddScore(pName, playerScore);
		}

	}
}
