using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f; 
    public string sceneToLoad; 

    private void Start()
    {
        
        fadeImage.gameObject.SetActive(false);
    }

    
    public void OnButtonClick()
    {
        StartCoroutine(LoadSceneWithTransition());
    }

    private IEnumerator LoadSceneWithTransition()
    {
        
        fadeImage.gameObject.SetActive(true);

        
        yield return StartCoroutine(FadeToBlack());

        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
           
            if (asyncOperation.progress >= 0.9f)
            {
                
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return StartCoroutine(FadeToClear());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeToClear()
    {
        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 - Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }

        
        fadeImage.gameObject.SetActive(false);
    }

    
    public void TransitionImmediately()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeToBlack());
        SceneManager.LoadScene(sceneToLoad);
    }
}
