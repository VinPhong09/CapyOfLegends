using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlongWayTaskList", menuName = "ScriptableObjects/AlongWayTask/TaskList")]
public class AlongWayTaskList : ScriptableObject
{
    public List<AlongWayTask> _taskList;
}
