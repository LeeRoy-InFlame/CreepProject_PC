using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundAssetMenu _soundCollection;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = GetComponentInParent<AudioSource>();
        }
    }

    public void AttackSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[0]);
    }

    public void DeadSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[1]);
    }

    public void StepSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[2]);
    }

    public void PainSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[3]);
    }

    public void EnemySeePlayerSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[4]);
    }

    public void JumpSound()
    {
        _audioSource.PlayOneShot(_soundCollection._currentSound[5]);
    }
}
