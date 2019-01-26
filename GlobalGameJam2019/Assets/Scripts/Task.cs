using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

    public BoxCollider2D boxCollider;

    private int currentPoints;
    private bool is_active;
    float currentTime;
    private Player player1, player2;
    public TaskManager taskManager;
    public Canvas timerCanvas;
    public Image timer;

    // Asignar externamente 
    public float completionStress;
    public float timeOutStress;
    public int pointsToCompletion;
    public float timeToCompletion;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        timerCanvas = transform.GetChild(0).GetComponent<Canvas>();
        timer = timerCanvas.transform.GetChild(0).GetComponent<Image>().transform.GetChild(0).GetComponent<Image>();
        timerCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (is_active) {
            currentTime -= Time.deltaTime;
            timer.fillAmount = currentTime / timeToCompletion;
            if (currentTime <= 0) {
                Debug.Log("Explota");
                TimeOut();
            }
        }
    }

    private void TimeOut() {
        player1.IncreaseStress(timeOutStress);
        player2.IncreaseStress(timeOutStress);
        Deactivate();
        animator.SetTrigger("TimeOut");
    }

    private void Deactivate() {
        is_active = false;
        taskManager.TaskDone();
        timerCanvas.enabled = false;
    }

    public void Create(Player p1, Player p2, TaskManager tm) {
        player1 = p1;
        player2 = p2;
        taskManager = tm;
        animator.SetTrigger("Active");
    }

    // Devuelve true si la tarea se inicializa
    public bool Initialize() {
        bool ret = !is_active;
        if (!is_active) {
            is_active = true;
            currentPoints = 0;
            currentTime = timeToCompletion;
            timerCanvas.enabled = true;
            timer.fillAmount = 360;
        }
        return ret;
    }

    public void WorkOn(Player p) {
        if (is_active) {
            currentPoints += 1;
            if (currentPoints >= pointsToCompletion) {
                p.IncreaseStress(completionStress);
                Deactivate();
                animator.SetTrigger("Done");
            }
        }
    }

}
