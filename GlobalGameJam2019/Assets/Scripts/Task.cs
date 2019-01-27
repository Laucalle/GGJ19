using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

    public BoxCollider2D boxCollider;

    private int currentPoints;
    private bool is_active;
    private float currentTime;
    private Player player1, player2;
    private TaskManager taskManager;
    private Canvas timerCanvas;
    private Canvas completionCanvas;
    private Image timer;
    private Image completionImage;

    // Asignar externamente 
    public float completionStress;
    public float timeOutStress;
    public int pointsToCompletion;
    public float timeToCompletion;
    public Animator animator;
    public AudioClip onBrokenClip, whileBrokenClip, onFixedClip;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        is_active = false;

        timerCanvas = transform.GetChild(0).GetComponent<Canvas>();
        timer = timerCanvas.transform.GetChild(0).GetComponent<Image>().transform.GetChild(0).GetComponent<Image>();
        timerCanvas.enabled = false;

        completionCanvas = transform.GetChild(1).GetComponent<Canvas>();
        completionImage = completionCanvas.transform.GetChild(0).GetComponent<Image>().transform.GetChild(0).GetComponent<Image>();
        completionCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (is_active) {
            currentTime -= Time.deltaTime;
            timer.fillAmount = currentTime / timeToCompletion;
            if (currentTime <= 0) {
                TimeOut();
            }
        }
    }
    private void playBroken(){
        audioSource.clip = whileBrokenClip;
        audioSource.loop = true;
        audioSource.Play();

    }
    private void TimeOut() {
        player1.IncreaseStress(timeOutStress);
        player2.IncreaseStress(timeOutStress);
		Deactivate(false);
        DeactivateCompletionCanvas();
        animator.SetTrigger("IdleBroken");
        if (onBrokenClip!=null){
            audioSource.PlayOneShot(onBrokenClip);
            Invoke("playBroken", onBrokenClip.length);
        }
        else if(whileBrokenClip!=null){
            playBroken();
        }
        
    }

    private void Deactivate(bool isTaskDone) {
        is_active = false;
        taskManager.TaskDone(isTaskDone);
        timerCanvas.enabled = false;
        audioSource.Stop();
        audioSource.loop = false;
        if (onFixedClip != null){
            audioSource.PlayOneShot(onFixedClip);
        }

    }

    public void Create(Player p1, Player p2, TaskManager tm) {
        player1 = p1;
        player2 = p2;
        taskManager = tm;
    }

    // Devuelve true si la tarea se inicializa
    public bool Initialize() {
        bool wasnt_active = !is_active;
        if (!is_active) {
            is_active = true;
            currentPoints = 0;
            currentTime = timeToCompletion;

            timerCanvas.enabled = true;
            timer.fillAmount = 360;
            
            completionImage.fillAmount = 0; 

            animator.SetTrigger("Active");
        }
        return wasnt_active;
    }

    public bool WorkOn(Player p) {
        bool was_active = is_active;
        if (is_active) {
            currentPoints += 1;
            completionCanvas.enabled = true;
            completionImage.fillAmount = (float) currentPoints / pointsToCompletion;
            if (currentPoints >= pointsToCompletion) {
                p.IncreaseStress(completionStress);
				Deactivate(true);
                animator.SetTrigger("Idle");
                Invoke("DeactivateCompletionCanvas", 0.3f);
            }
        }
        return was_active;
    }

    private void DeactivateCompletionCanvas() {
        completionCanvas.enabled = false;
    }

}
