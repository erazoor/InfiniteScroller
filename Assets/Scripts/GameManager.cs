using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI pauseText;
    private bool isPaused = false;
    
    public LeaderboardManager leaderboardManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
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
        SceneManager.LoadScene("HUD", LoadSceneMode.Single);
        StartCoroutine(AssignUIReferencesAfterSceneLoad(finalScore, finalCollectibles));
    }
    
    private IEnumerator AssignUIReferencesAfterSceneLoad(int finalScore, int finalCollectibles)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "HUD");
    
        GameObject leaderboardPanel = GameObject.Find("Leaderboard");

        if (leaderboardPanel != null)
        {
            TextMeshProUGUI[] scoreTexts = leaderboardPanel.GetComponentsInChildren<TextMeshProUGUI>()
                .Where(text => text.gameObject.tag == "ScoreText").ToArray();
            TextMeshProUGUI[] collectibleTexts = leaderboardPanel.GetComponentsInChildren<TextMeshProUGUI>()
                .Where(text => text.gameObject.tag == "CollectibleText").ToArray();

            LeaderboardManager lbManager = LeaderboardManager.Instance;

            if (lbManager != null)
            {
                lbManager.ResetUIReferences(scoreTexts, collectibleTexts);
                lbManager.AddEntry(finalScore, finalCollectibles);
                lbManager.UpdateLeaderboardUI();
            }
            else
            {
                Debug.LogError("Leaderboard Manager not found");
            }
        }
    }
}