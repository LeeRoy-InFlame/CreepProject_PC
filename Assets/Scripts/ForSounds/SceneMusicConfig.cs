using UnityEngine;

[CreateAssetMenu(fileName = "SceneMusicConfig", menuName = "Audio/Scene Music Config")]
public class SceneMusicConfig : ScriptableObject
{
    [Tooltip("����� ����� � Addressables")]
    public string sceneAddress;

    [Tooltip("����� ���� �������� ��� ���� �����")]
    public AudioClip musicClip;
}
