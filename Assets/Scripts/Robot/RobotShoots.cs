using TMPro;
using UnityEngine;

namespace Player.Inputs
{
    public class RobotShoots : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Transform _spawnBulletPoint;
        [SerializeField] private float _maxTimeToRecoveryBullet;
        [SerializeField] private TMP_Text _numberOfBullets;
        [SerializeField] private int _currentNumberOfBullets;
        [SerializeField] private int _maxNumberOfBullets;
        [SerializeField] private SoundController _soundController;

        private float _currentTimeToRecoveryBullet;

        private GameObject _currentBullet;
        private Rigidbody2D _currentBulletRigidbody;
        private TakeControl _takeControl;

        private void Start()
        {
            _takeControl = GetComponent<TakeControl>();
            _numberOfBullets.text = _currentNumberOfBullets.ToString();
            _currentTimeToRecoveryBullet = _maxTimeToRecoveryBullet;
        }

        private void Update()
        {
            _currentTimeToRecoveryBullet -= Time.deltaTime;
            if (_currentTimeToRecoveryBullet <= 0)
            {
                BulletRecovery();
            }
        }

        public void Shot(bool _isShooting)
        {
            if (_isShooting == true && _currentNumberOfBullets > 0 && _takeControl.ControlOff == false)
            {
                _soundController.AttackSound();
                _currentBullet = Instantiate(_bullet, _spawnBulletPoint.position, Quaternion.identity);
                _currentBulletRigidbody = _currentBullet.GetComponent<Rigidbody2D>();
                _currentBulletRigidbody.linearVelocityX = _bulletSpeed;
                _currentNumberOfBullets--;
                _numberOfBullets.text = _currentNumberOfBullets.ToString();
            }
        }

        public void ShotHandler()
        {
            if (_currentNumberOfBullets > 0 && _takeControl.ControlOff == false)
            {
                _soundController.AttackSound();
                _currentBullet = Instantiate(_bullet, _spawnBulletPoint.position, Quaternion.identity);
                _currentBulletRigidbody = _currentBullet.GetComponent<Rigidbody2D>();
                _currentBulletRigidbody.linearVelocityX = _bulletSpeed;
                _currentNumberOfBullets--;
                _numberOfBullets.text = _currentNumberOfBullets.ToString();
            }
        }

        private void BulletRecovery()
        {
            if (_currentNumberOfBullets < _maxNumberOfBullets)
            {
                _currentNumberOfBullets++;
            }
            _currentTimeToRecoveryBullet = _maxTimeToRecoveryBullet;
            _numberOfBullets.text = _currentNumberOfBullets.ToString();
        }
    }
}

