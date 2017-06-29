using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RandomGaModelGenerator : MonoBehaviour, IGaModelGenerator
{
    public GameObject prefab;

    [SerializeField]
    private float _randomRangeMax = 100;

    public GameObject GenerateModel()
    {
        var result = Instantiate(prefab);
        var script = result.GetComponent<IGaRunningModel>();
        var dataCount = script.GetDataLength();

        var valueList = new List<float>();
        for (int i = 0; i < dataCount; i++)
            valueList.Add(Random.Range(-_randomRangeMax, _randomRangeMax));

        script.InitValue(valueList);
        return result;
    }
}
