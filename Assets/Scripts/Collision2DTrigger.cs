using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision2DTrigger : MonoBehaviour
{
    public delegate void CollisionEnter2D(Collision2D collision);
    public event CollisionEnter2D OnCollisionEnter;

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }

    public delegate void CollisionExit2D(Collision2D collision);
    public event CollisionExit2D OnCollisionExit;

    void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }

    public delegate void CollisionStay2D(Collision2D collision);
    public event CollisionStay2D OnCollisionStay;

    void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStay?.Invoke(collision);
    }

    public delegate void TriggerEnter2D(Collider2D collider);
    public event TriggerEnter2D OnTriggerEnter;

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEnter?.Invoke(collider);
    }

    public delegate void TriggerExit2D(Collider2D collider);
    public event TriggerExit2D OnTriggerExit;

    void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit?.Invoke(collider);
    }

    public delegate void TriggerStay2D(Collider2D collider);
    public event TriggerStay2D OnTriggerStay;

    void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerStay?.Invoke(collider);
    }
}
