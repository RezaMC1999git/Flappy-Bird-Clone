using UnityEngine;

public class GameCotroller : MonoBehaviour
{
    public static GameCotroller instance;
    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";
    private const string GREEN_BIRD = "Green Bird";
    private const string RED_BIRD = "Red Bird";
    private void Awake()
    {
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();
    }

    void MakeSingleton() 
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void IsTheGameStartedForTheFirstTime() 
    {
        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime")) 
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(GREEN_BIRD, 1);
            PlayerPrefs.SetInt(RED_BIRD, 1);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
        }
    }
    public void SetHighScore(int score) 
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }
    public int GetHighScore() 
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
    public void SetSelectedBird(int selcet) 
    {
        PlayerPrefs.SetInt(SELECTED_BIRD, selcet);
    }
    public int GetSelectedBird() 
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    } 
    public void UnlockGreenBird() 
    {
        PlayerPrefs.SetInt(GREEN_BIRD, 1);
    }
    public int IsGreenBirdUnlocked() 
    {
        return PlayerPrefs.GetInt(GREEN_BIRD);
    }
    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD, 1);
    }
    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD);
    }
}
