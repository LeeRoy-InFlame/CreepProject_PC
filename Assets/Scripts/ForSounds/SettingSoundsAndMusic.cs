using UnityEngine;
using UnityEngine.UI;

public class SettingSoundsAndMusic : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioSource _musicSource;

    private const string MUSIC_KEY = "MusicVolume";
    private const string SOUND_KEY = "SoundVolume";

    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        _soundSlider.value = PlayerPrefs.GetFloat(SOUND_KEY, 1f);

        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _soundSlider.onValueChanged.AddListener(SetSoundVolume);

        SetMusicVolume(_musicSlider.value);
        SetSoundVolume(_soundSlider.value);
    }

    private void SetMusicVolume(float _volume)
    {
        _musicSource.volume = _volume;
        PlayerPrefs.SetFloat(MUSIC_KEY, _volume);
    }

    private void SetSoundVolume(float _volume)
    {
        AudioListener.volume = _volume;
        PlayerPrefs.SetFloat(SOUND_KEY, _volume);
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }

}
