using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    
    private float timeBetweenTasks;
    private float currentTime;

    // Asignar externamente
    public List<Task> tasks;
    Player player1, player2;

    // Start is called before the first frame update
    void Start() {
        foreach (Task t in tasks) {
            t.Create( player1, player2, this );
        }

        timeBetweenTasks = 5;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update() {

        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0) {




            currentTime = timeBetweenTasks;
        }
    }


}
