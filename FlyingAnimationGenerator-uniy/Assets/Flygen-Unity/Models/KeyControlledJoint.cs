using UnityEngine;

namespace Models
{
    public class KeyControlledJoint : MonoBehaviour
    {

        public CharacterJoint TargetJoint;
        private Rigidbody _targetRigidbody;

        [SerializeField]
        private float _torquePower = 100f;

        public void Start()
        {
            _targetRigidbody = TargetJoint.GetComponent<Rigidbody>();
        }

        public void Update()
        {
            Vector3 torque = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
                torque += new Vector3(0, 0, _torquePower);
            if (Input.GetKey(KeyCode.D))
                torque += new Vector3(0, 0, -_torquePower);
            if (Input.GetKey(KeyCode.W))
                torque += new Vector3(0, _torquePower, 0);
            if (Input.GetKey(KeyCode.S))
                torque += new Vector3(0, -_torquePower, 0);
            if (Input.GetKey(KeyCode.Q))
                torque += new Vector3(_torquePower, 0, 0);
            if (Input.GetKey(KeyCode.E))
                torque += new Vector3(-_torquePower, 0, 0);

            _targetRigidbody.AddTorque(torque);
        }
    }
}
