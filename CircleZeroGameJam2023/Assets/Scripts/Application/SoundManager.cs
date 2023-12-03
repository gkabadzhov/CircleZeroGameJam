using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip soundtrackClip;
    [SerializeField]
    private AudioClip traverseClip;

    private bool shouldSwapClips = false;

    void Start()
    {
        source.clip = soundtrackClip;
        source.Play();
    }

    public void SwapClips()
    {
        shouldSwapClips = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldSwapClips)
        {
            shouldSwapClips = false;

            source.clip = source.clip == soundtrackClip ? traverseClip : soundtrackClip;
            source.Play();
        }
    }
}
