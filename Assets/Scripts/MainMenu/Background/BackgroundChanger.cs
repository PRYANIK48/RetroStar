using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public GameObject[] Backgrounds;
    public void ChangeBG(int index)
    {
        DeActivateAll();
        Backgrounds[index].SetActive(true);
    }
    private void DeActivateAll()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            Backgrounds[i].SetActive(false);
        }
    }
}
