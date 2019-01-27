using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    
    private float delayBetweenTasks;
    private float currentTime;
    public int numTasks;
    private int maxActiveTasks;

    // Asignar externamente
    private List<Task> tasks;
    public Player player1, player2;

	private int quantityOfTasksCompleted;

    // Start is called before the first frame update
    void Start() {
        int childrenCount = transform.childCount;
        Debug.Log(childrenCount);
        for (int i = 0; i < childrenCount; i++) {
            transform.GetChild(i).GetComponent<Task>().Create(player1, player2, this);
            tasks.Add(transform.GetChild(i).GetComponent<Task>());
        }

        /*
        foreach (Task t in tasks) {
            t.Create( player1, player2, this );
        }*/

        delayBetweenTasks = 5;
        currentTime = 1;

        maxActiveTasks = System.Math.Min(6, tasks.Count);
        numTasks = 0;

        if (maxActiveTasks > 1) {
            Invoke("ActivateTask", 2);
        }
		quantityOfTasksCompleted = 0;
    }   

    private void ResetTimer() {
        currentTime = 5;// delayBetweenTasks * numTasks;
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
        int randomNumber = Random.Range(0, tasks.Count );
        if ( tasks[randomNumber].Initialize() ) {
            numTasks++;
        }
    }

    public void TaskDone(bool isTaskDone) {
        numTasks--;
        currentTime -= delayBetweenTasks/2;

		if (isTaskDone) {
			quantityOfTasksCompleted++;
		}
    }
		
	public int getQuantityOfTasksCompleted() {
		return quantityOfTasksCompleted;	
	}
}
