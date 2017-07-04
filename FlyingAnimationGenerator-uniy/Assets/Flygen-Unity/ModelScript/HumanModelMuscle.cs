using UnityEngine;
using System.Collections;

public class HumanModelMuscle : MonoBehaviour
{
    [SerializeField]
    private Transform _leftTransform;
    [SerializeField]
    private Rigidbody _leftRigidbody;

    [SerializeField]
    private Transform _rightTransform;
    [SerializeField]
    private Rigidbody _rightRigidbody;


    private float _startDistance;
    private float _springPower;
    private float _lengthChange;
    private float _additionalPower;

    void Start()
    {
        _startDistance = GetDistance().magnitude;
    }

    void Update()
    {
        var distance = GetDistance();
        var dir = distance.normalized;
        var targetDistance = _startDistance * (1 + _lengthChange);
        var pullPower = (distance.magnitude - targetDistance) * _springPower + _additionalPower;

        _rightRigidbody.AddForceAtPosition(dir * pullPower * -1, _rightRigidbody.transform.position);
        _leftRigidbody.AddForceAtPosition(dir * pullPower, _leftTransform.transform.position);
    }

    Vector3 GetDistance()
    {
        return _rightRigidbody.transform.position - _leftTransform.transform.position;
    }

    public void SetSpringPower(float power)
    {
        _springPower = power;
    }

    public void SetLengthChange(float length)
    {
        _lengthChange = length;
    }
    public void SetAdditionalPower(float power)
    {
        _additionalPower = power;
    }
}
