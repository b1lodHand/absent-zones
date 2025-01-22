using UnityEngine;

namespace com.absence.zonesystem.builtin
{
    /// <summary>
    /// A zone type which plays audio through an AudioSource when special events occur (3D).
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("absencee_/absent-zones/Built-in/3D/Audio Zone")]
    public class AudioZone : Zone3D
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