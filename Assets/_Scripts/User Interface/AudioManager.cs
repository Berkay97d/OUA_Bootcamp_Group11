using System;
using _Scripts.Grid_System;
using ChessPieces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    public AudioSource fireSound; // Ate� sesi
    public AudioSource movementSound; // Hareket sesi
    public Slider fmVolumeSlider; // Slider

    private void Start()
    {
        // Slider'�n de�erini ses seviyesine ayarlama
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.volume = volumeSlider.value;

        // Slider'a bir event listener ekleyerek ses seviyesini g�ncelle
        volumeSlider.onValueChanged.AddListener(SetVolume);

        fmVolumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        
        ChessPieceMovement.OnChessPieceMove += ChessPieceMovementOnOnChessPieceMove;
    }

    private void OnDestroy()
    {
        ChessPieceMovement.OnChessPieceMove -= ChessPieceMovementOnOnChessPieceMove;
    }

    private void ChessPieceMovementOnOnChessPieceMove(ChessPiece arg1, GridObject arg2, GridObject arg3)
    {
        PlayMovementSounds();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);  // Ayar� kaydet
        PlayerPrefs.Save();
    }

    void OnSliderValueChanged(float value)
    {
        // Slider'�n de�erine g�re her iki sesin seviyelerini e�it derecede ayarla
        float newVolume = Mathf.Clamp01(value);

        fireSound.volume = newVolume;
        movementSound.volume = newVolume;
    }
    
    public void PlayFireSound()
    {
        // Ateş seslerini bir kere oynat
        if (!fireSound.isPlaying)
        {
            fireSound.PlayOneShot(fireSound.clip);
        }
    }

    private void PlayMovementSounds()
    {
        // hareket seslerini bir kere oynat
        if (!movementSound.isPlaying)
        {
            movementSound.PlayOneShot(movementSound.clip);
        }
    }
}
