using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundChanger : MonoBehaviour
{
    public GameObject[] Backgrounds;
    
    private void Awake()
    {
        ChangeBG(PlayerPrefs.GetInt("BGID"));
    }

    public void ChangeBG(int index)
    {
        DeActivateAll();
        Backgrounds[index].SetActive(true);
        PlayerPrefs.SetInt("BGID",index);
    }

    private void DeActivateAll()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            Backgrounds[i].SetActive(false);
        }
    }
}
