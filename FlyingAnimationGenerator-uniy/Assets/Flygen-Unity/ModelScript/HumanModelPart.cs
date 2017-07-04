using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HumanModelPart : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    private Vector3 _startPosition;

    public Rigidbody Rigidbody { get { return _rigidbody; } }

    public void Start()
    {
        _startPosition = transform.position;
    }

    public Vector3 GetMoveDistance()
    {
        return this.transform.position - _startPosition;
    }
}
