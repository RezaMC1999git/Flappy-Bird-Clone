using System.Collections;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;
    [SerializeField] private GameObject FadeCanvas;
    [SerializeField] private Animator FadeAnim;
    private void Awake()
    {
        MakeSingleton();
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
    public void FadeIn(string LevelName) 
    {
        StartCoroutine(FadeInAnimation(LevelName));
    }
    public void FadeOut() 
    {
        StartCoroutine(FadeOutAnimation());
    }
    IEnumerator FadeInAnimation(string level) 
    {
        FadeCanvas.SetActive(true);
        FadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCourotine.WaitForRealSeconds(0.3f));
        Application.LoadLevel(level);
        FadeOut();
    }
    IEnumerator FadeOutAnimation() 
    {
        FadeAnim.Play("FadeOut");
        yield return StartCoroutine(MyCourotine.WaitForRealSeconds(0.5f));
        FadeCanvas.SetActive(false);
    }
}
