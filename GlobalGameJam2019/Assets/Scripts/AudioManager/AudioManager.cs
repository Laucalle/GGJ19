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

    void Start() {
        audioSource = GetComponent<AudioSource>();

        nodeDict["START"] = new AudioNode(new Vector2(0, 0), null, null, clips[0]);
        nodeDict["LowStressLoop"] = new AudioNode(new Vector2(0, 33), null, null, clips[1]);
        nodeDict["MediumStressStart"] = new AudioNode(new Vector2(33, 66), null, null, clips[2]);
        nodeDict["MediumStressLoop"] = new AudioNode(new Vector2(33, 66), null, null, clips[3]);
        nodeDict["MediumStressExit"] = new AudioNode(new Vector2(66, 100), null, null, clips[4]);
        nodeDict["HighStressStart"] = new AudioNode(new Vector2(66, 100), null, null, clips[5]);
        nodeDict["HighStressLoop"] = new AudioNode(new Vector2(66, 100), null, null, clips[6]);
        nodeDict["HighStressExit"] = new AudioNode(new Vector2(33, 66), null, null, clips[7]);

        nodeDict["START"].setReachableNodes(new List<AudioNode>{nodeDict["LowStressLoop"]});
        nodeDict["START"].setDefaultNode( nodeDict["LowStressLoop"] );
        nodeDict["LowStressLoop"].setReachableNodes(new List < AudioNode >{ nodeDict["LowStressLoop"], nodeDict["MediumStressStart"] });
        nodeDict["LowStressLoop"].setDefaultNode(nodeDict["MediumStressStart"]);
        nodeDict["MediumStressStart"].setReachableNodes(new List<AudioNode> { nodeDict["LowStressLoop"], nodeDict["MediumStressLoop"] });
        nodeDict["MediumStressStart"].setDefaultNode(nodeDict["MediumStressLoop"]);
        nodeDict["MediumStressLoop"].setReachableNodes(new List<AudioNode> { nodeDict["MediumStressLoop"], nodeDict["MediumStressStart"], nodeDict["MediumStressExit"] });
        nodeDict["MediumStressLoop"].setDefaultNode(nodeDict["MediumStressExit"]);
        nodeDict["MediumStressExit"].setReachableNodes(new List<AudioNode> { nodeDict["MediumStressLoop"], nodeDict["HighStressStart"] });
        nodeDict["MediumStressExit"].setDefaultNode(nodeDict["MediumStressLoop"]);
        nodeDict["HighStressStart"].setReachableNodes(new List<AudioNode> {nodeDict["MediumStressExit"], nodeDict["HighStressLoop"] });
        nodeDict["HighStressStart"].setDefaultNode(nodeDict["HighStressExit"]);
        nodeDict["HighStressLoop"].setReachableNodes(new List<AudioNode> { nodeDict["HighStressLoop"], nodeDict["HighStressExit"] });
        nodeDict["HighStressLoop"].setDefaultNode(nodeDict["HighStressExit"]);
        nodeDict["HighStressExit"].setReachableNodes(new List<AudioNode>{nodeDict["MediumStressLoop"] });
        nodeDict["HighStressExit"].setDefaultNode(nodeDict["MediumStressLoop"]);

    }

    // Update is called once per frame
    void Update () {
        if (!audioSource.isPlaying){
            audioSource.Play();
        }
    }
}
