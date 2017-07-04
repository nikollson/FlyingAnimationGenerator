using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RandomGaModelGenerator : MonoBehaviour, IGaDataGenerator
{
    public List<float> GenerateData(int dataCount)
    {
        var valueList = new List<float>();
        for (int i = 0; i < dataCount; i++)
            valueList.Add(Random.Range(-1f, 1f));

        return valueList;
    }
}
