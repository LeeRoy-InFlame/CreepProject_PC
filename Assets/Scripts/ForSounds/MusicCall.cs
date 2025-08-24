using UnityEngine;

public class MusicCall : MonoBehaviour
{
    [SerializeField] private MusicType type;
    [SerializeField, Min(0f)] private float fadeTime = 1f;

    private void Start()
    {
        if(Checkpoint._lastCheckpointPosition != Vector2.zero)
        {
            Play();
        }
    }

    public void Play()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayMusic(type, fadeTime);
    }

    public void Stop()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.StopMusic(fadeTime);
    }
}

