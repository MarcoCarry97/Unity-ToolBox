using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Band.Gui.Widgets
{
    [Serializable]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ParticleSystem))]
    public class ExtendedButton : Button, ISelectHandler, IDeselectHandler
    {
        [FormerlySerializedAs("onSelect")]
        [SerializeField]
        private UnityEvent m_OnSelect;

        [FormerlySerializedAs("onDeselect")]
        [SerializeField]
        private UnityEvent m_OnDeselect;

        private AudioSource source;

        [FormerlySerializedAs("ClickAudioClip")]
        [SerializeField]
        private AudioClip clickClip;

        [FormerlySerializedAs("SelectAudioClip")]
        [SerializeField]
        private AudioClip selectClip;

        public UnityEvent onSelect { get { return m_OnSelect; } set { m_OnSelect = value; } }
        public UnityEvent onDeselect { get { return m_OnDeselect; } set { m_OnDeselect = value; } }

        public void OnClick()
        {
            source.clip = clickClip;
            source.Play();
        }

        public void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelect.Invoke();
            source.clip = selectClip;
            source.Play();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            onDeselect.Invoke();
        }

        protected void Start()
        {
            source = this.GetComponent<AudioSource>();
            this.onClick.AddListener(OnClick);
        }
    }
}
