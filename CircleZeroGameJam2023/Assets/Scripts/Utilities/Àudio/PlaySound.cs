using UnityEngine;

namespace OTBG.Utilities.Àudio
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}
