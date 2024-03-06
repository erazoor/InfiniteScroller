using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

[System.Serializable]
public class LeaderboardEntry
{
    public int score;
    public int collectibles;

    public LeaderboardEntry(int newScore, int newCollectibles)
    {
        score = newScore;
        collectibles = newCollectibles;
    }
}

public class LeaderboardManager : MonoBehaviour
{
	private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] collectibleTexts;

	void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HUD")
        {
            LoadLeaderboardFromJSON();
            UpdateLeaderboardUI();
        }
    }

	public void LoadLeaderboardFromJSON()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboardEntries = data.entries.OrderByDescending(entry => entry.score).ThenByDescending(entry => entry.collectibles).ToList();
            UpdateLeaderboardUI();
        }
    }

	public void UpdateLeaderboardUI()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < leaderboardEntries.Count)
            {
                scoreTexts[i].text = $"Score: {leaderboardEntries[i].score}";
                collectibleTexts[i].text = $"Collectibles: {leaderboardEntries[i].collectibles}";
            }
            else
            {
                scoreTexts[i].text = "";
                collectibleTexts[i].text = "";
            }
        }
    }
}
