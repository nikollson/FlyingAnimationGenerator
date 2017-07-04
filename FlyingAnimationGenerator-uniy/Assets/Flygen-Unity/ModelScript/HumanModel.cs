using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

class HumanModel : MonoBehaviour
{
    [SerializeField]
    private HumanModelPart _head;
    [SerializeField]
    private HumanModelPart _trunk;
    [SerializeField]
    private HumanModelPart[] _allParts;
    [SerializeField]
    private HumanModelPart[] _enableHitGroundParts;

    public HumanModelPart Head { get { return _head; } }
    public HumanModelPart Trunk { get { return _trunk; } }
    public HumanModelState State { get; private set; }

    public IObservable<Collision> OnFallBodyAsObserbable()
    {
        var ret = Observable.Empty<Collision>();
        ret.Merge(_allParts
            .Where(x => _enableHitGroundParts.Contains(x) == false)
            .Select(x => x.OnCollisionEnterAsObservable())
            .ToArray());
        return ret;
    }
}
