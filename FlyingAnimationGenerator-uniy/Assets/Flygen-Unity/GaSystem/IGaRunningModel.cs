using System.Collections.Generic;

public interface IGaRunningModel
{
    void InitValue(List<float> value);
    int GetDataLength();
    bool IsEnd();
    float GetEvaluate();
    void Erase();
    List<float> GetData();
}
