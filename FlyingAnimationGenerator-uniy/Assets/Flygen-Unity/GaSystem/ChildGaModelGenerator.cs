using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChildGaModelGenerator : MonoBehaviour, IGaDataGenerator
{
    public enum MergeMode { Shuftule, TwoPick };

    List<IGaRunningModel> parent;

    [SerializeField]
    private float errorPar = 0.04f;

    public MergeMode mode;

    public void SetNextParent(List<IGaRunningModel> models)
    {
        parent = models;
    }

    public List<float> GenerateData(int dataLength)
    {
        if (mode == MergeMode.Shuftule) return MiracleSuftule(dataLength);
        return TwoPick(dataLength);
    }

    private List<float> MiracleSuftule(int dataLength)
    {
        var ret = new List<float>();
        var dataList = parent.Select(x => x.GetData()).ToList();
        for (int i = 0; i < dataLength; i++)
        {
            float value = dataList[Random.Range(0, dataList.Count)][i];
            if (Random.Range(0f, 1f) < errorPar) value = Random.Range(-1f, 1f);
            ret.Add(value);
        }
        return ret;
    }

    private List<float> TwoPick(int dataLength)
    {
        var ret = new List<float>();
        var dataList = parent.Select(x => x.GetData()).ToList();
        int a = Random.Range(0, dataList.Count);
        int b = Random.Range(0, dataList.Count);
        for (int i = 0; i < dataLength; i++)
        {
            float value = Random.Range(0, 1) == 0 ? dataList[a][i] : dataList[b][i];
            if (Random.Range(0f, 1f) < errorPar) value = Random.Range(-1f, 1f);
            ret.Add(value);
        }
        return ret;
    }
}
