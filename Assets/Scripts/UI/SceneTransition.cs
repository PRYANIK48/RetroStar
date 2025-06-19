using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(TransitionFadeOut(sceneName));
    }
    private IEnumerator TransitionFadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        if (fadeImage != null )
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
