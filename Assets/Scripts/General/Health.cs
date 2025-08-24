using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private UnityEvent _gettingDamage;
    [SerializeField] private ParticleSystem _bloodSpatter;
    [SerializeField] private Image _healthbar;
    [SerializeField] private int _numberScoreForEnemy;

    private TMP_Text _scorePanel;


    public static int CurrentScore;

    private float _currentHealth;

    public bool IsAlive => _currentHealth > 0;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void GetTreatment(float _healthPoints)//метод для лечения. Срабатывает через триггер из скрипта "Healing".
    {
        _currentHealth += _healthPoints;
        if (_healthbar != null)
        {
            _healthbar.fillAmount = _currentHealth / _maxHealth;
        }

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void TakeDamage(float _damage)
    {
        if(_currentHealth != 0)
        {
            _gettingDamage.Invoke();
            _currentHealth -= _damage;
            if (_healthbar != null)
            {
                _healthbar.fillAmount = _currentHealth / _maxHealth;
            }
        }

        if (_currentHealth <= 0)
        {
            Scoring();
        }

        if (_bloodSpatter != null )
        {
            _bloodSpatter.Play();
        }
    }

    public void Scoring()
    {
        _scorePanel = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        CurrentScore += _numberScoreForEnemy;
        _scorePanel.text = CurrentScore.ToString();
    }
}
