using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundaryChecker : MonoBehaviour
{
    public float leftLimit = -15f;
    public float bottomLimit = -5f;
    
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

	public void UpdateScore(int newScore)
	{
	    playerScore = newScore;

    	PlayerBoundaryChecker boundaryChecker = GetComponent<PlayerBoundaryChecker>();
    	if (boundaryChecker != null)
	    {
        	boundaryChecker.playerScore = newScore;
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
