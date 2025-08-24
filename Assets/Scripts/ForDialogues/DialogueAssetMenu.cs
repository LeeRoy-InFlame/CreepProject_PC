using UnityEngine;

[CreateAssetMenu]
public class DialogueAssetMenu : ScriptableObject
{
    [TextArea]
    public string[] _dialogueStrings;
}
