using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] Birds;
    private bool isGreenBirdUnlocked, isRedBirdUnlocked;
    public static MenuController instance;
    private void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
        CheckIfBirdsAreUnlicked();
    }
    void MakeInstance() 
    {
        if (instance == null)
            instance = this;
    }
    void CheckIfBirdsAreUnlicked() 
    {
        if(GameCotroller.instance.IsGreenBirdUnlocked() == 1) 
        {
            isGreenBirdUnlocked = true;
        }
        if (GameCotroller.instance.IsRedBirdUnlocked() == 1)
        {
            isRedBirdUnlocked = true;
        }
    }
    public void ChangeBird() 
    {
        if(GameCotroller.instance.GetSelectedBird() == 0) 
        {
            if(isGreenBirdUnlocked == true) 
            {
                Birds[0].SetActive(false);
                GameCotroller.instance.SetSelectedBird(1);
                Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
            }
        }
        else if(GameCotroller.instance.GetSelectedBird() == 1) 
        {
            if (isRedBirdUnlocked) 
            {
                Birds[1].SetActive(false);
                GameCotroller.instance.SetSelectedBird(2);
                Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
            }
            else 
            {
                Birds[1].SetActive(false);
                GameCotroller.instance.SetSelectedBird(0);
                Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
            }
        }
        else if(GameCotroller.instance.GetSelectedBird() == 2) 
        {
            Birds[2].SetActive(false);
            GameCotroller.instance.SetSelectedBird(0);
            Birds[GameCotroller.instance.GetSelectedBird()].SetActive(true);
        }
    }
    public void StartGame() 
    {
        SceneFader.instance.FadeIn("GamePlay");
    }
}
