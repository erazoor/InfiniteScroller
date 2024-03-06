using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLeaderboard();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateLeaderboardUI();
    }
    
    public void AddEntry(int score, int collectibles)
    {
        leaderboardEntries.Add(new LeaderboardEntry(score, collectibles));
        SortAndLimitEntries();
        SaveLeaderboard();
    }
    
    private void SortAndLimitEntries()
    {
        leaderboardEntries = leaderboardEntries.OrderByDescending(entry => entry.score)
            .ThenByDescending(entry => entry.collectibles)
            .Take(5)
            .ToList();
    }

    void LoadLeaderboard()
    {
        leaderboardEntries.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"Score{i}") && PlayerPrefs.HasKey($"Collectibles{i}"))
            {
                leaderboardEntries.Add(new LeaderboardEntry(PlayerPrefs.GetInt($"Score{i}"), PlayerPrefs.GetInt($"Collectibles{i}")));
            }
        }
    }

    public void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            PlayerPrefs.SetInt($"Score{i}", leaderboardEntries[i].score);
            PlayerPrefs.SetInt($"Collectibles{i}", leaderboardEntries[i].collectibles);
        }
        PlayerPrefs.Save();
    }
    
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] collectibleTexts;
    
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
    
    public void ResetUIReferences(TextMeshProUGUI[] newScoreTexts, TextMeshProUGUI[] newCollectibleTexts)
    {
        scoreTexts = newScoreTexts;
        collectibleTexts = newCollectibleTexts;
        UpdateLeaderboardUI();
    }
}