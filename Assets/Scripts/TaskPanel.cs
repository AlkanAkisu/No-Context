using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    public enum Tasks
    {
        Coffee,
        Alarm,
        Flower
    }
    [SerializeField] GameObject[] destroyWhenAllTaskDone;

    public TaskPanelItemData[] taskPanelItemDatas;
    private int taskCounter;

    [System.Serializable]
    public class TaskPanelItemData
    {
        public Tasks task;
        public Transform taskItem;
    }

    private void Start()
    {
        taskCounter = 0;
 
    }
    public void TaskDone(int taskVal)
    {
        var task = (Tasks)taskVal;
        Debug.Log($"{task} is done");
        var item = Array.Find(taskPanelItemDatas, (taskPanelItemData) => taskPanelItemData.task.Equals(task));
        Destroy(item.taskItem.gameObject);

        taskCounter++;

        if (taskCounter >= 3)
            Array.ForEach(destroyWhenAllTaskDone, go => Destroy(go));
        if (task.Equals(Tasks.Alarm))
        {
            AudioManager.i.Stop("Alarm");
            Debug.Log($"Stop Alarm");
        }
        if (task.Equals(Tasks.Coffee))
            AudioManager.i.Play("Coffee");
    }

}
