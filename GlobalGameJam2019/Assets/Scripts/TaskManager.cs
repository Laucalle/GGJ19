using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {
    
    private float delayBetweenTasks;
    private float currentTime;
    public int numTasks;
    private int maxActiveTasks;

    private float GAME_TIME;

    public Canvas taskManagerCanvas;
    private Text timerText, pointsText;
    public GameObject endGamePanel;

    private float timeLeft;

    // Asignar externamente
    public List<Task> tasks;
    public Player player1, player2;

	private int quantityOfTasksCompleted;

    private bool gameEnded;

    // Start is called before the first frame update
    void Start() {
        GAME_TIME = 121;
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

        gameEnded = false;

        if (maxActiveTasks > 1) {
            Invoke("ActivateTask", 2);
        }

		quantityOfTasksCompleted = 0;

        timerText = taskManagerCanvas.transform.GetChild(0).GetComponent<Text>();
        //endGamePanel = taskManagerCanvas.transform.GetChild(1).GetComponent<GameObject>();
        pointsText = endGamePanel.transform.GetChild(0).GetComponent<Text>();
        endGamePanel.SetActive(false);
        //pointsText.enabled = false;
        timeLeft = GAME_TIME ;
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

        if (timeLeft <= 0 && !gameEnded) {
            Time.timeScale = 0;
            endGamePanel.SetActive(true);
            gameEnded = true;
            pointsText.text = "Score: " + quantityOfTasksCompleted.ToString();
            Mute();
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

    private void Mute() {
        foreach (Task t in tasks) {
            t.Mute();
        }
    }
}
