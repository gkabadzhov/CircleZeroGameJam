using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip   shootClip;
    private bool        shouldTriggerShootClip = false;
    [SerializeField]
    private AudioClip   dashClip;
    private bool        shouldTriggerDashClip = false;

    public void TriggerShootClip()  { shouldTriggerShootClip = true; }
    public void TriggerDashClip()   { shouldTriggerDashClip = true; }

    private void Update()
    {
        if(shouldTriggerDashClip)
        {
            source.PlayOneShot(dashClip);
            shouldTriggerDashClip = false;
        }

        if (shouldTriggerShootClip)
        {
            source.PlayOneShot(shootClip);
            shouldTriggerShootClip = false;
        }
    }

}
