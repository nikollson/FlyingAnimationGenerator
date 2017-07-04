using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

class HumanModelState
{
    public enum StandStateType { Stand, Fall }


    public StandStateType StandState { get; set; }


}
