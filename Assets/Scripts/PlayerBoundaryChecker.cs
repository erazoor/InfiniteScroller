using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundaryChecker : MonoBehaviour
{
    public float leftLimit = -25f;
    public float bottomLimit = -5f;
    
    public LeaderboardManager leaderboardManager;
    public int playerScore;
    public int playerCollectibles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftLimit || transform.position.y < bottomLimit)
        {
            DestroyPlayer();
        }
    }

    void DestroyPlayer()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.EndGame(playerScore, playerCollectibles);
        } else {
            Debug.Log("Game Manager not found");
        }
        
        Destroy(gameObject);
    }
}
