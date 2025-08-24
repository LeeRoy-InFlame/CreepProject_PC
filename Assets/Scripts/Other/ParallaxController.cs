using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] _layers;
    [SerializeField] private float[] _shiftCoefficients;

    private int _numberOfLayers;

    private void Start()
    {
        _numberOfLayers = _layers.Length;
    }

    private void Update()
    {
        for (int i = 0; i < _numberOfLayers; i++)
        {
            _layers[i].position = transform.position * _shiftCoefficients[i];
        }
    }
}
