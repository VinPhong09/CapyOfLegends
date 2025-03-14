using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AlongWayTask", menuName = "ScriptableObjects/AlongWayTask/Task")]
public class AlongWayTask : ScriptableObject
{
    public string _text;
    public int _rewardAmount;
    public int _goal = 1;
    public enum typesOfTaskEnum :byte{
        kill,
        progress,
        skill,
        lvl
    }

    public typesOfTaskEnum taskType = typesOfTaskEnum.kill;

    public CurrencySO _rewardCurrency;

}
