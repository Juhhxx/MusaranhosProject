using System;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    private Collider _collider;

    public UnityEvent OnTrigger;
    private void Start()
    {
        _collider = GetComponent<Collider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            OnTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
