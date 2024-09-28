using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [SerializeField] private Text scoreText, endText, bestText, gameOverText;
    [SerializeField] private Button restartGameButton, instructionsButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] Birds;
    [SerializeField] private Sprite[] medals;
    [SerializeField] private Image medalImage;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0;
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    public void PauseGame() 
    {
        if(BirdScript.instance!= null) 
        {
            if (BirdScript.instance.isAlive) 
            {
                pausePanel.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                endText.text = "" + BirdScript.instance.Score;
                bestText.text = "" + GameCotroller.instance.GetHighScore();
                Time.timeScale = 0;
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }
    public void GoToMenuButton()
    {
        SceneFader.instance.FadeIn("MainMenu");
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        SceneFader.instance.FadeIn(Application.loadedLevelName);
    }
    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetScore(int Score)
    {
        scoreText.text = "" + Score;
    }
    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        endText.text = "" + score;
        if(score > GameCotroller.instance.GetHighScore()) 
        {
            GameCotroller.instance.SetHighScore(score);
        }
        bestText.text = "" + GameCotroller.instance.GetHighScore();
        if (score <= 20) 
        {
            medalImage.sprite = medals[0];
        }
        else if(score>20 && score <= 40) 
        {
            medalImage.sprite = medals[1];
            if (GameCotroller.instance.IsGreenBirdUnlocked() == 0)
                GameCotroller.instance.UnlockGreenBird();
        }
        else if (score > 40)
        {
            medalImage.sprite = medals[2];
            if (GameCotroller.instance.IsGreenBirdUnlocked() == 0)
                GameCotroller.instance.UnlockGreenBird();
            if (GameCotroller.instance.IsRedBirdUnlocked() == 0)
                GameCotroller.instance.UnlockRedBird();
        }
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }
}
