using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HumanModel : MonoBehaviour
{
    [SerializeField]
    private HumanModelPart[] _allParts;
    [SerializeField]
    private HumanModelPart _head;
    [SerializeField]
    private HumanModelPart _trunk;
    [SerializeField]
    private HumanModelPart[] _enableHitGroundParts;


}
