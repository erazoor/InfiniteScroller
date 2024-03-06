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
        yield return new WaitForSeconds(0.1f); 
        
        GameObject leaderboardPanel = GameObject.Find("LeaderboardManager");
    
        TextMeshProUGUI[] scoreTexts = FindObjectsOfType<TextMeshProUGUI>().Where(t => t.tag == "ScoreText").ToArray();
        TextMeshProUGUI[] collectibleTexts = FindObjectsOfType<TextMeshProUGUI>().Where(t => t.tag == "CollectibleText").ToArray();
        
        if (leaderboardPanel != null)
        {
            TextMeshProUGUI[] newScoreTexts = leaderboardPanel.GetComponentsInChildren<TextMeshProUGUI>()
                .Where(text => text.tag == "ScoreText").ToArray();
            TextMeshProUGUI[] newCollectibleTexts = leaderboardPanel.GetComponentsInChildren<TextMeshProUGUI>()
                .Where(text => text.tag == "CollectibleText").ToArray();

            if (LeaderboardManager.Instance != null)
            {
                LeaderboardManager.Instance.ResetUIReferences(newScoreTexts, newCollectibleTexts);
                LeaderboardManager.Instance.AddEntry(finalScore, finalCollectibles);
                LeaderboardManager.Instance.UpdateLeaderboardUI();
            }
            else
            {
                Debug.LogError("Leaderboard Manager not found");
            }
        }
        else
        {
            Debug.LogError("Leaderboard Panel not found"); 
        }
    }
}