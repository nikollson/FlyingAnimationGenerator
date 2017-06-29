using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public int sectionCount = 4;
    public float sectionTime = 0.5f;

    private List<List<Vector3>> torqueData;
    private float count;

    public void InitValue(List<float> value)
    {
        int vNum = 0;
        torqueData = new List<List<Vector3>>();
        for (int i = 0; i < sectionCount; i++)
        {
            var data = new List<Vector3>();
            for (int j = 0; j < GetVectorCount(); j++)
            {
                data.Add(new Vector3(value[vNum], value[vNum + 1], value[vNum + 2]));
                vNum += 3;
            }
            torqueData.Add(data);
        }
    }

    public int GetDataLength()
    {
        const int vectorSize = 3;
        return GetVectorCount() * vectorSize * sectionCount;
    }

    public int GetVectorCount()
    {
        return NormalRigidbodies.Count + MirrorRigidbodies.Count * 2;
    }

    public void Update()
    {
        if (torqueData == null) return;
        count += Time.deltaTime;
        var sectionNum = (int)(count / sectionTime) % sectionCount;
        var isMirror = (int)(count / sectionTime) % (sectionCount * 2) / sectionCount == 1;

        for (var i = 0; i < NormalRigidbodies.Count; i++)
        {
            NormalRigidbodies[i].AddTorque(torqueData[sectionNum][i]);
        }
        for (var p = 0; p < MirrorRigidbodies.Count; p++)
        {
            var i = p * 2 + NormalRigidbodies.Count;
            if (isMirror == false)
            {
                MirrorRigidbodies[p].LeftRigidbody.AddRelativeTorque(torqueData[sectionNum][i]);
                MirrorRigidbodies[p].RightRigidbody.AddRelativeTorque(torqueData[sectionNum][i + 1]);
            }
            else
            {
                MirrorRigidbodies[p].LeftRigidbody.AddRelativeTorque(Vector3.Scale(new Vector3(1, -1, -1), torqueData[sectionNum][i + 1]));
                MirrorRigidbodies[p].RightRigidbody.AddRelativeTorque(Vector3.Scale(new Vector3(1, -1, -1), torqueData[sectionNum][i]));
            }

        }
    }
}
