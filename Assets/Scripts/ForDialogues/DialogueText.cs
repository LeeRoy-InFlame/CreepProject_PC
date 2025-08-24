using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    private TMP_Text _dialogueText;
    private float charactersPerSecond = 5;

    private void Start()
    {
        _dialogueText = GetComponent<TMP_Text>();
    }

    IEnumerator TypeText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            _dialogueText.text = textBuffer;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }
    }
}
