using UnityEngine;

[CreateAssetMenu(fileName = "SceneMusicConfig", menuName = "Audio/Scene Music Config")]
public class SceneMusicConfig : ScriptableObject
{
    [Tooltip("Адрес сцены в Addressables")]
    public string sceneAddress;

    [Tooltip("Какой трек включать для этой сцены")]
    public AudioClip musicClip;
}
