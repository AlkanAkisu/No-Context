using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public enum Tasks
    {
        Coffee,
        Alarm,
        Flower,
        Ignore
    }
    [SerializeField] GameObject[] destroyWhenAllTaskDone;
    [SerializeField] UnityEvent onAllTasksCompleted;
    [SerializeField] bool doNotPlayAlarm;
    [SerializeField] bool doNotPlayCoffee;
    public List<TaskPanelItemData> taskPanelItemDatas;
    private int taskCounter;

    private void OnEnable()
    {
        taskPanelItemDatas.ForEach(item => item.taskItem.gameObject.SetActive(true));

    }

    private void Start()
    {
        Invoke(nameof(PlayAlarm), 0.5f);


    }

    private void PlayAlarm()
    {
        AudioManager.i.Play("Alarm");
    }


    [System.Serializable]
    public class TaskPanelItemData
    {
        public Tasks task;
        public Transform taskItem;
    }


    public void TaskDone(Tasks taskVal)
    {
        var task = taskVal;
        var item = taskPanelItemDatas.Find((taskPanelItemData) => taskPanelItemData.task.Equals(task));

        item.taskItem.gameObject.SetActive(false);
        taskPanelItemDatas.Remove(item);


        if (taskPanelItemDatas.Count == 0)
            onAllTasksCompleted?.Invoke();

        if (task.Equals(Tasks.Alarm))
            AudioManager.i.Stop("Alarm");
        if (task.Equals(Tasks.Coffee))
            AudioManager.i.Play("Coffee");
        if (task.Equals(Tasks.Flower))
            AudioManager.i.Play("Pickup");

    }
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
