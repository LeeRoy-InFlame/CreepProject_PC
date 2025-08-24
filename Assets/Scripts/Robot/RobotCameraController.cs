using UnityEngine;

public class RobotCameraController : MonoBehaviour
{
    [SerializeField] private Animator _animation;

    public void Shaking()
    {
        _animation.SetTrigger("Shake");
    }
}
