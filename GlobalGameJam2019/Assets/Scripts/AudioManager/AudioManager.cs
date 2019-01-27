using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Dictionary<string, AudioNode> nodeDict = new Dictionary<string, AudioNode>();
    [SerializeField]
    private List<AudioClip>clips;
    [SerializeField]
    private PlayerGameController playerGameController;

    private AudioNode currentNode;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        
        nodeDict["START"] = new AudioNode(new Vector2(0, 0), null, null, clips[0]);
        nodeDict["LowStressLoop"] = new AudioNode(new Vector2(0, 20), null, null, clips[1]);
        nodeDict["MediumStressStart"] = new AudioNode(new Vector2(20, 50), null, null, clips[2]);
        nodeDict["MediumStressLoop"] = new AudioNode(new Vector2(20, 50), null, null, clips[3]);
        nodeDict["MediumStressExit"] = new AudioNode(new Vector2(50, 100), null, null, clips[4]);
        nodeDict["HighStressStart"] = new AudioNode(new Vector2(50, 100), null, null, clips[5]);
        nodeDict["HighStressLoop"] = new AudioNode(new Vector2(50, 100), null, null, clips[6]);
        nodeDict["HighStressExit"] = new AudioNode(new Vector2(20, 50), null, null, clips[7]);

        nodeDict["START"].setReachableNodes(new List<AudioNode>{nodeDict["LowStressLoop"]});
        nodeDict["START"].setDefaultNode( nodeDict["LowStressLoop"] );
        nodeDict["LowStressLoop"].setReachableNodes(new List < AudioNode >{ nodeDict["LowStressLoop"], nodeDict["MediumStressStart"] });
        nodeDict["LowStressLoop"].setDefaultNode(nodeDict["MediumStressStart"]);
        nodeDict["MediumStressStart"].setReachableNodes(new List<AudioNode> { nodeDict["LowStressLoop"], nodeDict["MediumStressLoop"] });
        nodeDict["MediumStressStart"].setDefaultNode(nodeDict["MediumStressLoop"]);
        nodeDict["MediumStressLoop"].setReachableNodes(new List<AudioNode> { nodeDict["MediumStressLoop"], nodeDict["LowStressLoop"], nodeDict["MediumStressExit"] });
        nodeDict["MediumStressLoop"].setDefaultNode(nodeDict["MediumStressExit"]);
        nodeDict["MediumStressExit"].setReachableNodes(new List<AudioNode> {  nodeDict["HighStressStart"] });
        nodeDict["MediumStressExit"].setDefaultNode(nodeDict["HighStressStart"]);
        nodeDict["HighStressStart"].setReachableNodes(new List<AudioNode> {nodeDict["MediumStressLoop"], nodeDict["HighStressLoop"] });
        nodeDict["HighStressStart"].setDefaultNode(nodeDict["HighStressLoop"]);
        nodeDict["HighStressLoop"].setReachableNodes(new List<AudioNode> { nodeDict["HighStressLoop"], nodeDict["MediumStressLoop"] });
        nodeDict["HighStressLoop"].setDefaultNode(nodeDict["HighStressExit"]);
        

        currentNode = nodeDict["START"];
        audioSource.clip = currentNode.getAudioClip();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
        float totalstress = playerGameController.getCurrentMaxStressLevel();

        AudioNode nextAudioNode = currentNode.nextChild(totalstress);

        if (currentNode.getAudioClip().name != nextAudioNode.getAudioClip().name){
            //float currentTime = audioSource.time;
            audioSource.loop = false;
            //audioSource.time = currentTime;
            //audioSource.Play();
        }
        if (!audioSource.isPlaying){
            currentNode = nextAudioNode;
            audioSource.clip = currentNode.getAudioClip();
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
