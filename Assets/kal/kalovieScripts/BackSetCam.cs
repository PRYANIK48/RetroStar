using UnityEngine;

public class BackSetCam : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    private int BgNow;
    void Start()
    {

    }


    void Update()
    {
        BgNow = SaveSettings.Background;

        if (BgNow == 1)
        {
            bg1.gameObject.SetActive(true);
            bg2.gameObject.SetActive(false);
        }

        if (BgNow == 2)
        {
            bg1.gameObject.SetActive(false);
            bg2.gameObject.SetActive(true);
        }
    }
}
