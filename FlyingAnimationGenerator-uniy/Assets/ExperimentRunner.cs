using UnityEngine;
using System.Collections;

public class ExperimentRunner : MonoBehaviour
{
    public RandomGaModelGenerator randomModelGenerator;

    [SerializeField]
    private int _modelNum = 30;

    [SerializeField]
    private Vector3 _modelDistance = new Vector3(-1, 0, 0);

    void Start()
    {
        for (int i = 0; i < _modelNum; i++)
        {
            var obj = randomModelGenerator.GenerateModel();
            obj.transform.parent = transform;
            obj.transform.position = transform.position + _modelDistance * i;
        }
    }
}
