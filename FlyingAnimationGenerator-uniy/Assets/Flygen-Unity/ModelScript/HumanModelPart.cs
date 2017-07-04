using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HumanModelPart : MonoBehaviour
{
    private Vector3 _startPosition;

    public void Start()
    {
        _startPosition = transform.position;
    }

    public Vector3 GetMoveDistance()
    {
        return this.transform.position - _startPosition;
    }
}
