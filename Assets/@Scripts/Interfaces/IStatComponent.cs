using UnityEngine;

public interface IStatComponent
{
    void AddStat(IStat stat);
    
    void RemoveStat(IStat stat);
}
