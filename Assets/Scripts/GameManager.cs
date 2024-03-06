using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI pauseText;
    private bool isPaused = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseText.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseText.gameObject.SetActive(false);
    }
    
    public void EndGame(int finalScore, int finalCollectibles)
	{
 		SaveScoreToJSON(finalScore, finalCollectibles);
    	SceneManager.LoadScene("HUD", LoadSceneMode.Single);
	}

	private void SaveScoreToJSON(int score, int collectibles)
	{
    	string path = Application.persistentDataPath + "/leaderboard.json";
    	LeaderboardData leaderboardData = new LeaderboardData();

    	if (File.Exists(path))
    	{
        	string json = File.ReadAllText(path);
        	leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
    	}

    	leaderboardData.entries.Add(new LeaderboardEntry(score, collectibles));
    	leaderboardData.entries = leaderboardData.entries.OrderByDescending(entry => entry.score).ThenByDescending(entry => entry.collectibles).ToList();
    	string newJson = JsonUtility.ToJson(leaderboardData, true);
    	File.WriteAllText(path, newJson);
	}
}