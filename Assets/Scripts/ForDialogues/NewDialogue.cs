using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Player.Inputs;
using UnityEngine.Playables;

public class NewDialogue : MonoBehaviour
{
    [SerializeField] private DialogueAssetMenu[] _dialogueAssets;
    [SerializeField] private TakeControl _takeControl;

    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private UnityEvent _startDialogueEvent;
    [SerializeField] private UnityEvent _endDialogueEvent;
    [SerializeField] private UnityEvent _endAllDialogueEvent;

    [SerializeField] private float charactersPerSecond;

    private float _timer = 0;
    private float _interval;
    private string _textBuffer;
    private char[] _chars;
    private int _numberDialogue = 0;
    private int _numberString;
    private int _numberChar = 0;
    private bool _isSkipDialogue = false;

    private void Start()
    {
        _startDialogueEvent.Invoke();
        StartDialogue();
        if (_takeControl != null)
        {
            _takeControl.On();
        }

    }

    private void OnEnable()
    {
        _isSkipDialogue = false;
    }

    private void Update()
    {
        _isSkipDialogue = Input.GetButtonDown(GlobalStringVars.ESCAPE);
        if (Input.GetKeyDown(KeyCode.E) && _numberChar == _chars.Length)
        {
            _numberString++;
            StartCoroutine(TypeTextUncapped(_dialogueAssets[_numberDialogue]._dialogueStrings[_numberString]));

        }

        //запускает Event когда произнесена последняя реплика в диалоге или когда нажата кнопка пропуска диалога
        if (_numberString == _dialogueAssets[_numberDialogue]._dialogueStrings.Length - 1 || _isSkipDialogue == true)
        {
            _isSkipDialogue = false;
            EndDialogue();
        }
    }

    IEnumerator TypeTextUncapped(string line)
    {
        _interval = 1 / charactersPerSecond;
        _textBuffer = null;
        _chars = line.ToCharArray();
        _numberChar = 0;

        while (_numberChar < _chars.Length)
        {
            if (_timer < Time.deltaTime)
            {
                _textBuffer += _chars[_numberChar];
                _dialogueText.text = _textBuffer;
                _timer += _interval;
                _numberChar++;
            }
            else
            {
                _timer -= Time.deltaTime;
                yield return null;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SkipDialogueLine();
            }
        }
    }

    public void SkipDialogueLine()
    {
        if (_numberChar != _chars.Length)
        {
            _dialogueText.text = _dialogueAssets[_numberDialogue]._dialogueStrings[_numberString];
            _numberChar = _chars.Length;
        }
        else
        {
            _numberString++;
            StartCoroutine(TypeTextUncapped(_dialogueAssets[_numberDialogue]._dialogueStrings[_numberString]));
        }
    }

    public void StartDialogue()
    {
        _dialoguePanel.SetActive(true);
        StartCoroutine(TypeTextUncapped(_dialogueAssets[_numberDialogue]._dialogueStrings[_numberString]));
    }

    public void NextDialogue()
    {
        if (_numberDialogue <= _dialogueAssets.Length - 1)
        {
            _numberDialogue ++;
        }
    }

    public void EndDialogue()
    {
        _numberString = 0;
        _endDialogueEvent.Invoke();
        _dialogueText.text = null;
        _dialoguePanel.SetActive(false);

        if (_numberDialogue == _dialogueAssets.Length - 1)
        {
            if(_takeControl != null)
            {
                _takeControl.Off();
            }
            
            gameObject.SetActive(false);

            _endAllDialogueEvent.Invoke();
        }

        else
        {
            NextDialogue();
        }
    }
}
