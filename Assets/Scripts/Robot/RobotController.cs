using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject[] _explosions;
    [SerializeField] private GameObject _deathRobot;

    private GameObject _currentRobot;

    private void Update()
    {
        IDontFeelWell();
    }
    private void IDontFeelWell()
    {//изменение спрайта в зависимости от полученных повреждений
        if (_health.CurrentHealth < _health.MaxHealth * 0.75)
        {
            _explosions[0].SetActive(true);
        }

        if (_health.CurrentHealth < _health.MaxHealth * 0.50)
        {
            _explosions[1].SetActive(true);
        }

        if (_health.IsAlive == false)
        {
            _currentRobot = Instantiate(_deathRobot, transform.position, Quaternion.identity);
        }
    }
}
