using UnityEngine;

public class KeyControlledBody : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _time;

    public float speed = 3.0f;
    public float rotSpeed = 4.0f;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        int t = (int)_time;
        _time += Time.deltaTime;

        Vector3 setVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            setVelocity += new Vector3(speed, 0, 0);
        if (Input.GetKey(KeyCode.D))
            setVelocity += new Vector3(-speed, 0, 0);
        if (Input.GetKey(KeyCode.Q))
            setVelocity += new Vector3(0, speed, 0);
        if (Input.GetKey(KeyCode.E))
            setVelocity += new Vector3(0, -speed, 0);
        if (Input.GetKey(KeyCode.S))
            setVelocity += new Vector3(0, 0, speed);
        if (Input.GetKey(KeyCode.W))
            setVelocity += new Vector3(0, 0, -speed);

        if (setVelocity != Vector3.zero)
            _rigidbody.velocity = setVelocity;

        Vector3 setRotation = Vector3.zero;

        if (Input.GetKey(KeyCode.F))
            setRotation += new Vector3(0, rotSpeed, 0);
        if (Input.GetKey(KeyCode.H))
            setRotation += new Vector3(0, -rotSpeed, 0);
        if (Input.GetKey(KeyCode.T))
            setRotation += new Vector3(rotSpeed, 0, 0);
        if (Input.GetKey(KeyCode.G))
            setRotation += new Vector3(-rotSpeed, 0, 0);
        if (Input.GetKey(KeyCode.R))
            setRotation += new Vector3(0, 0, rotSpeed);
        if (Input.GetKey(KeyCode.Y))
            setRotation += new Vector3(0, 0, -rotSpeed);

        if (setRotation != Vector3.zero)
            _rigidbody.angularVelocity = setRotation;
    }
}
