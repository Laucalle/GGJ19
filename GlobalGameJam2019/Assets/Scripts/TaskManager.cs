using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    
    private float delayBetweenTasks;
    private float currentTime;
    private int numTasks;
    private int maxActiveTasks;

    // Asignar externamente
    public List<Task> tasks;
    public Player player1, player2;

    // Start is called before the first frame update
    void Start() {
        foreach (Task t in tasks) {
            t.Create( player1, player2, this );
        }

        delayBetweenTasks = 2;
        currentTime = 1;

        maxActiveTasks = 6;
        numTasks = 0;

        Invoke("TaskCompleted", 2);
    }

    private void ResetTimer() {
        currentTime = delayBetweenTasks * (maxActiveTasks - numTasks);
    }

    // Update is called once per frame
    void Update() {

        currentTime -= Time.deltaTime;

        if (numTasks >= maxActiveTasks) {
            ResetTimer();
        }

        if (currentTime <= 0) {
            ActivateTask();
            ResetTimer();
        }
    }

    void ActivateTask() {
        bool activated = false;
        int randomNumber;
        while (!activated) {
            randomNumber = Random.Range(0, tasks.Count );
            activated = tasks[randomNumber].Initialize();
        }
        numTasks++;
    }

    public void TaskCompleted() {
        numTasks--;
        currentTime -= delayBetweenTasks/2;
    }
}
