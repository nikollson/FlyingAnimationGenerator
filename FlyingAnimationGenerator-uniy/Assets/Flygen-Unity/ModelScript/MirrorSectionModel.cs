using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MirrorSectionModel : MonoBehaviour, IGaRunningModel
{
    [System.Serializable]
    public class MirrorRigidbody
    {
        public Rigidbody LeftRigidbody;
        public Rigidbody RightRigidbody;
    }
    public List<Rigidbody> NormalRigidbodies;
    public List<MirrorRigidbody> MirrorRigidbodies;
    public GameObject[] headObjects;

    public int sectionCount = 4;
    public float sectionTime = 0.5f;
    public float randomMax = 500f;

    private List<float> rawData;
    private List<List<Vector3>> torqueData;
    private float count;

    [SerializeField]
    private float moveTimeMax = 10f;

    private bool isEnd;
    private float evaluate;

    public void InitValue(List<float> value)
    {
        rawData = value;

        int vNum = 0;
        torqueData = new List<List<Vector3>>();
        for (int i = 0; i < sectionCount; i++)
        {
            var data = new List<Vector3>();
            for (int j = 0; j < GetVectorCount(); j++)
            {
                data.Add(new Vector3(value[vNum], value[vNum + 1], value[vNum + 2]) * randomMax);
                vNum += 3;
            }
            torqueData.Add(data);
        }

        var collisionGround = GetComponentsInChildren<TaggedObjectCollisionDetecter>();

        foreach (var a in collisionGround)
            a.OnEnter = DoEvaluate;
    }

    private void DoEvaluate()
    {
        if (isEnd) return;
        isEnd = true;
        evaluate = headObjects.Select(x => x.transform.position.z).Max();
        foreach (var a in headObjects)
            evaluate = Mathf.Max(evaluate, a.transform.position.z) * 0f + count;

        if (count > moveTimeMax)
            evaluate = 0f;
    }

    public int GetDataLength()
    {
        const int vectorSize = 3;
        return GetVectorCount() * vectorSize * sectionCount;
    }

    public bool IsEnd()
    {
        return isEnd;
    }

    public float GetEvaluate()
    {
        return evaluate;
    }

    public void Erase()
    {
        Destroy(gameObject);
    }

    public List<float> GetData()
    {
        return rawData;
    }

    public int GetVectorCount()
    {
        return NormalRigidbodies.Count + MirrorRigidbodies.Count * 2;
    }

    public void Update()
    {
        if (torqueData == null) return;
        if (isEnd) return;

        count += Time.deltaTime;

        var sectionNum = (int)(count / sectionTime) % sectionCount;
        var isMirror = (int)(count / sectionTime) % (sectionCount * 2) / sectionCount == 1;

        for (var i = 0; i < NormalRigidbodies.Count; i++)
        {
            NormalRigidbodies[i].AddRelativeForce(torqueData[sectionNum][i]);
        }
        for (var p = 0; p < MirrorRigidbodies.Count; p++)
        {
            var i = p * 2 + NormalRigidbodies.Count;
            if (isMirror == false)
            {
                MirrorRigidbodies[p].LeftRigidbody.AddRelativeForce(torqueData[sectionNum][i], ForceMode.Acceleration);
                MirrorRigidbodies[p].RightRigidbody.AddRelativeForce(torqueData[sectionNum][i + 1], ForceMode.Acceleration);
            }
            else
            {
                MirrorRigidbodies[p].LeftRigidbody.AddRelativeForce(Vector3.Scale(new Vector3(-1, 1, -1), torqueData[sectionNum][i + 1]), ForceMode.Acceleration);
                MirrorRigidbodies[p].RightRigidbody.AddRelativeForce(Vector3.Scale(new Vector3(-1, 1, -1), torqueData[sectionNum][i]), ForceMode.Acceleration);
            }
        }

        if (count > moveTimeMax)
        {
            DoEvaluate();
        }
    }
}
