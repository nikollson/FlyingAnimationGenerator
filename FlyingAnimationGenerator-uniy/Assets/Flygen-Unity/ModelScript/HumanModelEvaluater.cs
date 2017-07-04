using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UniRx.Triggers;

class HumanModelEvaluater
{
    private HumanModel _humanModel;
    private float _dontFallTime;
    private float _limitTime;

    public HumanModelEvaluater(HumanModel humanModel, float limitTime)
    {
        _humanModel = humanModel;
        _limitTime = limitTime;
    }

    public float EvaluateHeadPosition()
    {
        return _humanModel.Head.GetMoveDistance().z;
    }

    public float EvaluateTrunkPosition()
    {
        return _humanModel.Trunk.GetMoveDistance().z;
    }

    public float EvaluateDontFallTime()
    {
        return _dontFallTime;
    }
}
