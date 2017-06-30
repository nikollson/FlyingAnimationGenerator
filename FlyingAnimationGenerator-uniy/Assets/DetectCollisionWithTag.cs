using UnityEngine;
using System;

public class DetectCollisionWithTag : MonoBehaviour
{
    public Action OnEnter { get; set; }

    [SerializeField]
    private string tagName = "";

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == tagName && OnEnter != null)
            OnEnter();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == tagName && OnEnter != null)
            OnEnter();
    }
}
