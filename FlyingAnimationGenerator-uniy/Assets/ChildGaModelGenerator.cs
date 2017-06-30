using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChildGaModelGenerator : MonoBehaviour, IGaDataGenerator
{
    List<IGaRunningModel> parent;

    [SerializeField]
    private float errorPar = 0.04f;

    public void SetNextParent(List<IGaRunningModel> models)
    {
        parent = models;
    }

    public List<float> GenerateData(int dataLength)
    {
        var ret = new List<float>();
        var dataList = parent.Select(x => x.GetData()).ToList();
        for (int i = 0; i < dataLength; i++)
        {
            float value = dataList[Random.Range(0, dataList.Count)][i];
            if (Random.Range(0, 1) < errorPar) value = Random.Range(-1, 1);
            ret.Add(value);
        }
        return ret;
    }
}
