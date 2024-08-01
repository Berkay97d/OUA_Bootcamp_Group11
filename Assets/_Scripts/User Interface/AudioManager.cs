using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    public AudioSource fireSound; // Ateþ sesi
    public AudioSource movementSound; // Hareket sesi
    public Slider fmVolumeSlider; // Slider

    private void Start()
    {
        // Slider'ýn deðerini ses seviyesine ayarlama
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.volume = volumeSlider.value;

        // Slider'a bir event listener ekleyerek ses seviyesini güncelle
        volumeSlider.onValueChanged.AddListener(SetVolume);

        fmVolumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);  // Ayarý kaydet
        PlayerPrefs.Save();
    }

    void OnSliderValueChanged(float value)
    {
        // Slider'ýn deðerine göre her iki sesin seviyelerini eþit derecede ayarla
        float newVolume = Mathf.Clamp01(value);

        fireSound.volume = newVolume;
        movementSound.volume = newVolume;
    }
}
