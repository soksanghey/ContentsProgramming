using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicPlayerController : MonoBehaviour
{
    public GameObject playerPanel;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public Image volumeIcon;
    public TextMeshProUGUI statusText;

    private AudioSource audioSource;

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerPanel.SetActive(false);
        OnVolumeChanged(volumeSlider.value);
    }

    public void OnPlayerToggled(bool isOn)
    {
        playerPanel.SetActive(isOn);
    }

    public void OnVolumeChanged(float volume)
    {
        audioSource.volume = volume / 10.0f;
        volumeText.text = "볼륨: " + volume.ToString("F0");
        volumeIcon.fillAmount = volume / 10.0f;
        
        if (volume == 0)
        {
            volumeIcon.color = Color.gray;
        }
        else if (volume <= 5)
        {
            volumeIcon.color = Color.yellow;
        }
        else
        {
            volumeIcon.color = Color.green;
        }
    }

    public void OnPlayClicked()
    {
        PlayMusic();
        statusText.text = "재생 중";
    }

    public void OnStopClicked()
    {
        StopMusic();
        statusText.text = "정지 중";
    }
}

