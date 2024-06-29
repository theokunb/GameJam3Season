using System;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public event Action Collided;
    public event Action ColliderExited;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out GorelkaBehaviour _))
        {
            Collided?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GorelkaBehaviour _))
        {
            ColliderExited?.Invoke();
        }
    }
}
