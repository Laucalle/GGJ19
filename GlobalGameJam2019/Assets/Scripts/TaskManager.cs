using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {
    
    private float delayBetweenTasks;
    private float currentTime;
    public int numTasks;
    private int maxActiveTasks;

    public Canvas taskManagerCanvas;
    private Text timerText, pointsText;

    private float timeLeft;

    // Asignar externamente
    public List<Task> tasks;
    public Player player1, player2;

	private int quantityOfTasksCompleted;

    // Start is called before the first frame update
    void Start() {
        tasks = new List<Task>();
        int childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++) {
            transform.GetChild(i).GetComponent<Task>().Create(player1, player2, this);
            tasks.Add(transform.GetChild(i).GetComponent<Task>());
        }
        delayBetweenTasks = 2;
        currentTime = 1;

        maxActiveTasks = System.Math.Min(6, tasks.Count);
        numTasks = 0;

        if (maxActiveTasks > 1) {
            Invoke("ActivateTask", 2);
        }

		quantityOfTasksCompleted = 0;

        timerText = taskManagerCanvas.transform.GetChild(0).GetComponent<Text>();
        pointsText = taskManagerCanvas.transform.GetChild(1).GetComponent<Text>();
        pointsText.enabled = false;
        timeLeft = 181;
    }

    private void ResetTimer() {
        currentTime = delayBetweenTasks * numTasks;
    }

    // Update is called once per frame
    void Update() {

        UpdateTimer();

        timeLeft -= Time.deltaTime;
        currentTime -= Time.deltaTime;

        if (numTasks >= maxActiveTasks) {
            ResetTimer();
        }

        if (currentTime <= 0) {
            ActivateTask();
            ResetTimer();
        }

        if (timeLeft <= 0) {
            pointsText.text = "Score: " + quantityOfTasksCompleted.ToString();
            pointsText.enabled = true;
            Time.timeScale = 0;
        }
    }

    void UpdateTimer() {
        float minutes = (int) timeLeft / 60,
              seconds = (int) (timeLeft % 60);
        string message = minutes.ToString() + ":" + seconds.ToString("00");
        timerText.text = message;
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
