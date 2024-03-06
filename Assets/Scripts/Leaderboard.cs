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