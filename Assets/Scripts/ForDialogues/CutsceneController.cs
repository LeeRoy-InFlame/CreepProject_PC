using Player.Inputs;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private TakeControl _takeControl;
    [SerializeField] private PlayableAsset[] _playables;
    [SerializeField] private PlayableDirector _cutscene;
    private bool _isSkipCutscene;
    private int _numberPlayable = 0;

    private void Start()
    {
        _isSkipCutscene = false;
    }

    private void Update()
    {
        _isSkipCutscene = Input.GetButtonDown(GlobalStringVars.ESCAPE);
        if (_isSkipCutscene == true)
        {
            SkipCutscene();
            _isSkipCutscene = false;
            Debug.Log("Катсцена пропущена");
        }
    }

    public void NextCutscene()
    {
        if (_numberPlayable <= _playables.Length)
        {
            _numberPlayable ++;
        }

        else if(_numberPlayable == _playables.Length)
        {
            gameObject.SetActive(false);
        }
    }

    public void ReturnControl()
    {
        if (_takeControl != null)
        {
            _takeControl.ControlOff = false;
        }
    }

    public void PlayCutscene()
    {
        if (_numberPlayable < _playables.Length)
        {
            _cutscene.playableAsset = _playables[_numberPlayable];
            _cutscene.time = 0f;
            _cutscene.Play();
        }
    }

    public void SkipCutscene()
    {
        if (_cutscene.state == PlayState.Playing)
        {
            _cutscene.time = _cutscene.duration;
            _cutscene.Evaluate();
        }
    }
}
