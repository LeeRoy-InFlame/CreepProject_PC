using UnityEngine;
using UnityEngine.EventSystems;

public class SkipLine : MonoBehaviour, IPointerDownHandler
{
    private NewDialogue _newDialogue;
    public void OnPointerDown(PointerEventData eventData)
    {
        _newDialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<NewDialogue>();
        _newDialogue.SkipDialogueLine();
    }
}
