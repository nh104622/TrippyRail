using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

	private int totalScores = 9;    // Total high scores

	public Transform pScores;
	public Transform pNames;

    private void Awake()
    {
		if (!PlayerPrefs.HasKey(0+"HScore"))
        {
			AddDefaultScores();
        }
		if (pScores != null && pNames != null)
        {
			DisplayHighScore();
		}
		
    }


    void DisplayHighScore()
    {
		int counter = 1;
		for (int i = 0; i < totalScores; i++)
		{
			pNames.Find("PlayerName" + counter).GetComponent<Text>().text = PlayerPrefs.GetString(i + "HScoreName");
			pScores.Find("PlayerScore" + counter).GetComponent<Text>().text = PlayerPrefs.GetInt(i + "HScore").ToString();
			counter++;
		}
	}


	void AddDefaultScores()
	{
		AddScore("Nick", 6000);
		AddScore("AAA", 4000);
		AddScore("BBB", 3000);
		AddScore("CCC", 2200);
		AddScore("FFF", 1800);
		AddScore("ZZZ", 1500);
		AddScore("RGB", 1200);
		AddScore("ASS", 1000);
		AddScore("ABC", 500);
		AddScore("XXX", 800);
	}

	//Called by Interface to add new player high scores
	public void AddScore(string name, int score)
	{
		int newScore;
		string newName;
		int oldScore;
		string oldName;

		newScore = score;
		newName = name;
		for (int i = 0; i < totalScores; i++)
		{
			if (PlayerPrefs.HasKey(i + "HScore"))
			{
				if (PlayerPrefs.GetInt(i + "HScore") < newScore)
				{
					// new score is higher than the stored score
					oldScore = PlayerPrefs.GetInt(i + "HScore");
					oldName = PlayerPrefs.GetString(i + "HScoreName");
					PlayerPrefs.SetInt(i + "HScore", newScore);
					PlayerPrefs.SetString(i + "HScoreName", newName);
					newScore = oldScore;
					newName = oldName;
				}
			}
			else
			{
				PlayerPrefs.SetInt(i + "HScore", newScore);
				PlayerPrefs.SetString(i + "HScoreName", newName);
				newScore = 0;
				newName = "";
			}
		}
	}
}
