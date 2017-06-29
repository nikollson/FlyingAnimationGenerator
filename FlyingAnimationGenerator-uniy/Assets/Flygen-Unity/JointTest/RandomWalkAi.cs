using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Models
{
    public class RandomWalkAi : MonoBehaviour
    {
        readonly List<CharacterJoint> _joints = new List<CharacterJoint>();
        readonly List<float> _currentTorqueXList = new List<float>();
        readonly List<float> _currentTorqueYList = new List<float>();
        readonly List<float> _currentTorqueZList = new List<float>();

        [SerializeField]
        private float _torqueMax = 100.0f;

        [SerializeField]
        private float _torqueVeloctityMax = 200.0f;

        public void Start()
        {
            _joints.AddRange(GetComponentsInChildren<CharacterJoint>());
            _currentTorqueXList.AddRange(_joints.Select(_ => 0f));
            _currentTorqueYList.AddRange(_joints.Select(_ => 0f));
            _currentTorqueZList.AddRange(_joints.Select(_ => 0f));
        }

        public void Update()
        {
            for (int i = 0; i < _joints.Count; i++)
            {
                var joint = _joints[i];
                var rigid = joint.GetComponent<Rigidbody>();

                _currentTorqueXList[i] += Random.Range(-_torqueVeloctityMax, _torqueVeloctityMax) * Time.deltaTime;
                _currentTorqueYList[i] += Random.Range(-_torqueVeloctityMax, _torqueVeloctityMax) * Time.deltaTime;
                _currentTorqueZList[i] += Random.Range(-_torqueVeloctityMax, _torqueVeloctityMax) * Time.deltaTime;

                _currentTorqueXList[i] = Mathf.Clamp(_currentTorqueXList[i], -_torqueMax, _torqueMax);
                _currentTorqueYList[i] = Mathf.Clamp(_currentTorqueYList[i], -_torqueMax, _torqueMax);
                _currentTorqueZList[i] = Mathf.Clamp(_currentTorqueZList[i], -_torqueMax, _torqueMax);

                rigid.AddTorque(_currentTorqueXList[i], _currentTorqueYList[i], _currentTorqueZList[i]);
            }
        }
    }
}

