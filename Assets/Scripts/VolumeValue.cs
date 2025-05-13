using UnityEngine;


public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float Volume = 1f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        Volume = SaveSettings.VolumeSetting;
    }
    void Update()
    {
        audioSrc.volume = Volume;
    }
    public void SetVolume(float Vol)
    {
        Volume = Vol;
    }
}