using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        BackgroundMusicManager instance = FindObjectOfType<BackgroundMusicManager>();
        if (instance != null)
        {
            instance.SetVolume(value);
        }
    }
}
