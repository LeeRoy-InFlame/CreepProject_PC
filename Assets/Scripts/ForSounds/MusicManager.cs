using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MusicType
{
    MainMenu,
    Cutscene,
    Level,
    RunRobot,
    Creep
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<MusicEntry> _musicEntries;

    private Dictionary<MusicType, AudioClip> _musicDict;

    private void Awake()
    {
        // Если уже есть экземпляр в сцене – уничтожаем новый
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // ⚠️ Убрали DontDestroyOnLoad (каждая сцена сама имеет свой MusicManager)
        // DontDestroyOnLoad(gameObject);

        _musicDict = new Dictionary<MusicType, AudioClip>();
        foreach (var entry in _musicEntries)
        {
            if (!_musicDict.ContainsKey(entry.type))
                _musicDict.Add(entry.type, entry.clip);
        }
    }

    public void PlayMusic(MusicType type, float fadeTime = 1f)
    {
        if (!_musicDict.ContainsKey(type))
        {
            Debug.LogWarning($"Музыка для {type} не найдена!");
            return;
        }

        StartCoroutine(SwitchMusic(_musicDict[type], fadeTime));
    }

    private IEnumerator SwitchMusic(AudioClip newClip, float fadeTime)
    {
        if (_audioSource.isPlaying)
        {
            // Плавно заглушаем текущую музыку
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                _audioSource.volume = Mathf.Lerp(1, 0, t / fadeTime);
                yield return null;
            }
            _audioSource.Stop();
        }

        _audioSource.clip = newClip;
        _audioSource.Play();

        // Плавно поднимаем громкость
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            _audioSource.volume = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }
        _audioSource.volume = 1;
    }

    public void StopMusic(float fadeTime = 1f)
    {
        StartCoroutine(FadeOut(fadeTime));
    }

    private IEnumerator FadeOut(float fadeTime)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            _audioSource.volume = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }
        _audioSource.Stop();
        _audioSource.clip = null;
    }
}

[System.Serializable]
public class MusicEntry
{
    public MusicType type;
    public AudioClip clip;
}


