using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MuscleskeltalWalkerModel : MonoBehaviour, IGaRunningModel
{
    [SerializeField]
    private List<HumanModelMuscle> muscles;
    [SerializeField]
    private List<HumanModelMuscle> mirrorMuscles;

    [SerializeField]
    private int _sectionCount = 4;
    [SerializeField]
    private float _sectionTime = 0.15f;

    [SerializeField]
    private float _springPowerMax = 4000.0f;
    [SerializeField]
    private float _additionalPowerMax = 100.0f;
    [SerializeField]
    private float _lengthChangeMax = 0.1f;

    private List<float> _rawData;
    private bool _isEnd;
    private float _evaluate;
    private float _count;

    private List<List<float>> powerList;
    public void InitValue(List<float> data)
    {
        _rawData = data;
        int index = 0;
        for (int i = 0; i < muscles.Count; i++)
        {
            muscles[i].SetSpringPower(data[index] * _springPowerMax);
            muscles[i].SetLengthChange(data[index + 1] * _lengthChangeMax);
            mirrorMuscles[i].SetSpringPower(data[index] * _springPowerMax);
            mirrorMuscles[i].SetLengthChange(data[index + 1] * _lengthChangeMax);
            index += 2;
        }

        powerList = new List<List<float>>();
        for (int i = 0; i < _sectionCount; i++)
        {
            var target = new List<float>();
            for (int j = 0; j < muscles.Count; j++)
            {
                target.Add(data[index] * _additionalPowerMax);
                index++;
            }
            powerList.Add(target);
        }

        var collisionGround = GetComponentsInChildren<TaggedObjectCollisionDetecter>();
        foreach (var a in collisionGround)
            a.OnEnter = DoEvaluate;
    }

    void Update()
    {
        _count += Time.deltaTime;

        if (powerList == null)
            return;

        var sectionNum = (int)(_count / _sectionTime) % _sectionCount;
        var isMirror = (int)(_count / _sectionTime) % (_sectionCount * 2) / _sectionCount == 1;

        for (int i = 0; i < muscles.Count; i++)
        {
            muscles[i].SetAdditionalPower(powerList[sectionNum][i] * (isMirror ? -1 : 1));
            mirrorMuscles[i].SetAdditionalPower(powerList[sectionNum][i] * (isMirror ? 1 : -1));
        }
    }

    void DoEvaluate()
    {
        if (_isEnd == false)
        {
            _isEnd = true;
            _evaluate = _count;
        }
    }

    public int GetDataLength()
    {
        return muscles.Count * (_sectionCount + 2);
    }

    public bool IsEnd()
    {
        return _isEnd;
    }

    public float GetEvaluate()
    {
        return _evaluate;
    }

    public void Erase()
    {
        Destroy(gameObject);
    }

    public List<float> GetData()
    {
        return _rawData;
    }
}
