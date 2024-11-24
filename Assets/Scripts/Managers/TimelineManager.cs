using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] PlayableDirector _playableDirector;
    [Space]
    [SerializeField] TimelineAsset _defeat;
    [SerializeField] TimelineAsset _victory;

    public void PlayDefeat()
    {
        _playableDirector.Play(_defeat);
    }

    public void PlayVictory()
    {
        _playableDirector.Play(_victory);
    }
}
