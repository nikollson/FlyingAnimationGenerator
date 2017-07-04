using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MirrorSectionModel : MonoBehaviour, IGaRunningModel
{
    [System.Serializable]
    public class MirrorPart
    {
        [SerializeField]
        private HumanModelPart _leftPart;
        [SerializeField]
        private HumanModelPart _rightPart;

        public HumanModelPart LeftPart { get { return _leftPart; } }
        public HumanModelPart RightPart { get { return _rightPart; } }
    }
    [SerializeField]
    private HumanModelPart[] _normalParts;
    [SerializeField]
    private MirrorPart[] _mirrorParts;
    [SerializeField]
    private GameObject[] _headObjects;

    [SerializeField]
    private int _sectionCount = 4;
    [SerializeField]
    private float _sectionTime = 0.5f;
    [SerializeField]
    private float _randomMax = 500f;

    private List<float> _rawData;
    private List<List<Vector3>> _torqueData;
    private float _count;

    [SerializeField]
    private float _moveTimeMax = 10f;

    private bool _isEnd;
    private float _evaluate;

    public void InitValue(List<float> value)
    {
        _rawData = value;

        int vNum = 0;
        _torqueData = new List<List<Vector3>>();
        for (int i = 0; i < _sectionCount; i++)
        {
            var data = new List<Vector3>();
            for (int j = 0; j < GetVectorCount(); j++)
            {
                data.Add(new Vector3(value[vNum], value[vNum + 1], value[vNum + 2]) * _randomMax);
                vNum += 3;
            }
            _torqueData.Add(data);
        }

        var collisionGround = GetComponentsInChildren<TaggedObjectCollisionDetecter>();

        foreach (var a in collisionGround)
            a.OnEnter = DoEvaluate;
    }

    private void DoEvaluate()
    {
        if (_isEnd)
            return;
        _isEnd = true;
        _evaluate = _headObjects.Select(x => x.transform.position.z).Max();
        foreach (var a in _headObjects)
            _evaluate = Mathf.Max(_evaluate, a.transform.position.z) * 0f + _count;

        if (_count > _moveTimeMax)
            _evaluate = 0f;
    }

    public int GetDataLength()
    {
        const int vectorSize = 3;
        return GetVectorCount() * vectorSize * _sectionCount;
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

    public int GetVectorCount()
    {
        return _normalParts.Length + _mirrorParts.Length * 2;
    }

    public void Update()
    {
        if (_torqueData == null)
            return;
        if (_isEnd)
            return;

        _count += Time.deltaTime;

        var sectionNum = (int)(_count / _sectionTime) % _sectionCount;
        var isMirror = (int)(_count / _sectionTime) % (_sectionCount * 2) / _sectionCount == 1;

        for (var i = 0; i < _normalParts.Length; i++)
        {
            _normalParts[i].Rigidbody.AddRelativeForce(_torqueData[sectionNum][i]);
        }
        for (var p = 0; p < _mirrorParts.Length; p++)
        {
            var i = p * 2 + _normalParts.Length;
            if (isMirror == false)
            {
                _mirrorParts[p].LeftPart.Rigidbody.AddRelativeForce(_torqueData[sectionNum][i], ForceMode.Acceleration);
                _mirrorParts[p].RightPart.Rigidbody.AddRelativeForce(_torqueData[sectionNum][i + 1], ForceMode.Acceleration);
            }
            else
            {
                _mirrorParts[p].LeftPart.Rigidbody.AddRelativeForce(Vector3.Scale(new Vector3(-1, 1, -1), _torqueData[sectionNum][i + 1]), ForceMode.Acceleration);
                _mirrorParts[p].LeftPart.Rigidbody.AddRelativeForce(Vector3.Scale(new Vector3(-1, 1, -1), _torqueData[sectionNum][i]), ForceMode.Acceleration);
            }
        }

        if (_count > _moveTimeMax)
        {
            DoEvaluate();
        }
    }
}
