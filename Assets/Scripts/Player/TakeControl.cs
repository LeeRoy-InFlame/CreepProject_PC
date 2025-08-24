using UnityEngine;

public class TakeControl : MonoBehaviour
{
    public bool ControlOff;

    public void Off()
    {
        ControlOff = false;
    }

    public void On()
    {
        ControlOff = true;
    }
}

