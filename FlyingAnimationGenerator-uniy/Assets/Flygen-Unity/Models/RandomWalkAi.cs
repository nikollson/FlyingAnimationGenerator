using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class RandomWalkAi : MonoBehaviour
    {
        readonly List<CharacterJoint> _joints = new List<CharacterJoint>();

        [SerializeField]
        private float _torqueMax = 100.0f;

        public void Start()
        {
            _joints.AddRange(GetComponentsInChildren<CharacterJoint>());
        }

        public void Update()
        {
            foreach (var joint in _joints)
            {
                var rigid = joint.GetComponent<Rigidbody>();
                float x = Random.Range(-_torqueMax, _torqueMax);
                float y = Random.Range(-_torqueMax, _torqueMax);
                float z = Random.Range(-_torqueMax, _torqueMax);
                rigid.AddTorque(x, y, z);
            }
        }
    }
}

