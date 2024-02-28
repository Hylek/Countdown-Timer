using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DC.Countdown
{
    [Serializable]
    public struct CountdownContent
    {
        public float Duration;
        public string Message;
    }

    public class Countdown : MonoBehaviour
    {
        public event Action CountdownStarted;
        public event Action CountdownComplete;

        [SerializeField] private bool hasAnimator;
        [SerializeField] private TMP_Text countdownLabel;
        [SerializeField] private List<CountdownContent> content;

        private int _currentIndex;
        private Animator _animator;

        private void Awake()
        {
            if (!hasAnimator) return;
            _animator = GetComponent<Animator>();
        }

        public void BeginCountDown()
        {
            _currentIndex = 0;
            CountdownStarted?.Invoke();
            
            SetCountdownContent();
        }

        private void SetCountdownContent()
        {
            if (_currentIndex < content.Count)
            {
                if (hasAnimator)
                {
                    _animator.Play(0);
                }
                countdownLabel.SetText(content[_currentIndex].Message);
                Invoke(nameof(SetCountdownContent), content[_currentIndex].Duration);
                _currentIndex++;
            }
            else
            {
                FinishCountdown();
            }
        }

        private void FinishCountdown()
        {
            countdownLabel.SetText("");
            
            CountdownComplete?.Invoke();
        }
    }
}