using TMPro;
using UnityEngine;

namespace Player.Inputs
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _player;
        [SerializeField] private float _speedBullet;
        [SerializeField] private Transform _spawnPointBullet;
        [SerializeField] private TMP_Text _numberOfBullets;
        [SerializeField] private int _currentNumberOfBullets;

        private PlayerAnimationController _playerAnimationController;
        private SoundController _soundController;

        public int CurrentNumberOfBullet => _currentNumberOfBullets;
        public bool ICanShooting => _currentNumberOfBullets > 0;

        private void Start()
        {
            _playerAnimationController = GetComponent<PlayerAnimationController>();
            _soundController = GetComponentInChildren<SoundController>();
            UpdateUI();
        }

        public void Shoot(bool isShooting)
        {
            if (isShooting)
            {
                Shooting();
            }
        }

        private void Shooting()
        {
            if (!ICanShooting) return;

            _playerAnimationController.Shoot();
            _soundController.AttackSound();

            var bullet = Instantiate(_bullet, _spawnPointBullet.position, Quaternion.identity);
            var bulletRb = bullet.GetComponent<Rigidbody2D>();
            _currentNumberOfBullets--;

            // Направление выстрела
            float direction = _player.transform.localScale.x > 0 ? 1f : -1f;
            bulletRb.linearVelocity = new Vector2(_speedBullet * direction, 0f);

            UpdateUI();
        }

        public void PickUpAmmo(int ammoPoints)
        {
            _currentNumberOfBullets += ammoPoints;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _numberOfBullets.text = _currentNumberOfBullets.ToString();
        }
    }
}


