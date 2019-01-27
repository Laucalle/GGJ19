using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioNode{

    [SerializeField]
    private AudioClip track;
    [SerializeField]
    private Vector2 interval;
    [SerializeField]
    private List<AudioNode> reachableNodes;
    [SerializeField]
    private AudioNode defaultNode;

    public AudioNode(Vector2 _interval, List<AudioNode> _reachableNodes, AudioNode defaultNode, AudioClip track){
        this.interval = _interval;
        this.reachableNodes = _reachableNodes;
        this.track = track;
    }

    public Vector2 getInterval(){
        return this.interval;
    }

    public AudioClip getAudioClip(){
        return this.track;
    }
    public void setReachableNodes(List<AudioNode> _reachableNodes){
        this.reachableNodes = _reachableNodes;
    }

    public void setDefaultNode(AudioNode _defaultNode){
        this.defaultNode = _defaultNode;
    }

    public bool isWithinStressInterval(float stress){
        return this.interval.x <= stress && stress  <= this.interval.y;
    }

    public AudioNode nextChild(float stress){
        AudioNode returnNode = defaultNode;
        foreach(AudioNode node in reachableNodes){
            if (node.isWithinStressInterval(stress))
                returnNode = node;
        }
        return returnNode;
    }


}
