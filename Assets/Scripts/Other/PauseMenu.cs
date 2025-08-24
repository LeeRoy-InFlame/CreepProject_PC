using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _interfacePanel;
    [SerializeField] private TakeControl _takeControl;
    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_takeControl.ControlOff == false)
            {
                Pause();
            }

            else if (_isPaused == true)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        _pausePanel.SetActive(true);
        _interfacePanel.SetActive(false);
        Time.timeScale = 0f;
        _isPaused = true;
        _takeControl.ControlOff = true;

    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
        _interfacePanel.SetActive(true);
        Time.timeScale = 1f;
        _isPaused = false;
        _takeControl.ControlOff = false;
    }
}
