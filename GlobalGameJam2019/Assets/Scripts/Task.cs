using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {

    private BoxCollider2D boxCollider;

    private int points;
    private bool is_active;
    private float currentTime;
    private Player player1, player2;
    private TaskManager taskManager;

    // Asignar externamente 
    public float completationStress;
    public float timeOutStress;
    public int maxPoints;
    public float timeToCompletation;

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        points = 0;
    }

    // Update is called once per frame
    void Update() {
        if (is_active) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) {
                TimeOut();
            }
        }
    }

    private void TimeOut() {
        
        player1.IncreaseStress(timeOutStress);
        player2.IncreaseStress(timeOutStress);
        is_active = false;
    }

    public void Create(Player p1, Player p2, TaskManager tm) {
        player1 = p1;
        player2 = p2;
        taskManager = tm;
    }

    // Devuelve true si la tarea se inicializa
    public bool Initialize() {
        bool ret = !is_active;
        if (!is_active) {
            is_active = true;
            points = 0;
            currentTime = timeToCompletation;
        }
        return ret;
    }

    public void WorkOn(Player p) {
        if (is_active) {
            points += 10;
            if (points >= maxPoints) {
                p.IncreaseStress(completationStress);
                is_active = false;
            }
        }
    }

}
