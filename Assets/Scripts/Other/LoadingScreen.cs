using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    [Header("UI ��������")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _progressText;

    private void Awake()
    {
        // ��������, �� ���� ������ � �������� �����
        Instance = this;
        SetProgress(1f);
    }

    private void Start()
    {
        Hide(); // ������ ��� ������
    }

    public void Show()
    {
        if (_canvasGroup == null) return;

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        if (_canvasGroup == null) return;

        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void SetProgress(float value)
    {
        if (_progressBar != null)
            _progressBar.value = value;

        if (_progressText != null)
            _progressText.text = $"{Mathf.RoundToInt(value * 100f)}%";
    }
}





