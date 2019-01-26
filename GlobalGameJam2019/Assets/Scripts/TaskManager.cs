using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    
    private float timeBetweenTasks;
    private float currentTime;
    private int numTasks;
    private int maxActiveTasks;
    private Random random;

    // Asignar externamente
    public List<Task> tasks;
    Player player1, player2;

    // Start is called before the first frame update
    void Start() {
        foreach (Task t in tasks) {
            t.Create( player1, player2, this );
        }

        random = new Random();

        timeBetweenTasks = 5;
        currentTime = 1;

        maxActiveTasks = 6;
        numTasks = 0;
    }

    // Update is called once per frame
    void Update() {

        currentTime -= Time.deltaTime;

        if (numTasks >= maxActiveTasks) {
            currentTime = timeBetweenTasks;
        }

        if (currentTime <= 0) {
            ActivateTask();
            currentTime = timeBetweenTasks;
        }
    }

    void ActivateTask() {
        bool activated = false;
        while (!activated) {
            int randomNumber = random.Next(0, tasks.Count() );
            activated = tasks[randomNumber].Initialize();
        }
        numTasks++;
    }

    public void TaskCompleted() {
        numTasks--;
    }
}
