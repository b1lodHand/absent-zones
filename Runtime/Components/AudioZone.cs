using UnityEngine;

namespace com.absence.zonesystem
{
    /// <summary>
    /// A zone type which plays audio through an AudioSource when special events occur.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("absencee_/absent-zones/Built-in/Audio Zone")]
    public class AudioZone : Zone
    {
        [SerializeField] private AudioSource m_source;
        [SerializeField] private bool m_playOnEnter = true;
        [SerializeField] private bool m_playOnExit = false;

        protected override void OnEnter_Internal(GameObject enteredOne)
        {
            if (m_playOnEnter) m_source.Play();
        }

        protected override void OnExit_Internal(GameObject exitedOne)
        {
            if (m_playOnExit) m_source.Play();
        }

        protected override void Reset()
        {
            base.Reset();
            m_source = GetComponent<AudioSource>();
        }
    }

}