using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ExperimentRunner : MonoBehaviour
{
    public GameObject PrefabGameObject;

    public RandomGaModelGenerator randomModelGenerator;
    public ChildGaModelGenerator childModelGenerator;

    [SerializeField]
    private int _modelNum = 30;

    [SerializeField]
    private int _takeNum = 10;

    [SerializeField]
    private Vector3 _modelDistance = new Vector3(-1, 0, 0);

    List<IGaRunningModel> modelList = new List<IGaRunningModel>();
    public void Start()
    {
        MakeModels(randomModelGenerator);
    }

    private void MakeModels(IGaDataGenerator generator)
    {
        for (int i = 0; i < modelList.Count; i++)
            modelList[i].Erase();
        modelList.Clear();

        for (int i = 0; i < _modelNum; i++)
        {
            var obj = Instantiate(PrefabGameObject);
            var script = obj.GetComponent<IGaRunningModel>();
            script.InitValue(generator.GenerateData(script.GetDataLength()));
            obj.transform.parent = transform;
            obj.transform.position = transform.position + _modelDistance * i;
            modelList.Add(obj.GetComponent<IGaRunningModel>());
        }
    }

    public void Update()
    {
        bool isAllEnd = modelList.Count(x => x.IsEnd() == false) == 0;
        if (isAllEnd)
        {
            modelList = modelList.OrderByDescending(x => x.GetEvaluate()).ToList();
            childModelGenerator.SetNextParent(modelList.OrderByDescending(x => x.GetEvaluate()).Take(_takeNum).ToList());
            MakeModels(childModelGenerator);
        }
    }
}
