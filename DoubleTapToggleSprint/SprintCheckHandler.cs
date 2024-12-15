using Player;
using System;
using UnityEngine;

namespace DoubleTapToggleSprint
{
    internal sealed class SprintCheckHandler : MonoBehaviour
    {
        public SprintCheckHandler(IntPtr ptr) : base(ptr) { }
#pragma warning disable CS8618
        public static SprintCheckHandler Instance { get; private set; }
#pragma warning restore CS8618

        public static bool ToggleSprint { get; private set; }
        private PlayerAgent? _cachedPlayer;
        private PlayerLocomotion? _cachedLocomotion;
        private float _lastTapTime = 0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (_cachedPlayer == null)
            {
                _cachedPlayer = PlayerManager.GetLocalPlayerAgent();
                if (_cachedPlayer == null) return;
                ToggleSprint = false;
                _cachedLocomotion = _cachedPlayer.Locomotion;
            }

            if (Configuration.RequireForward && !_cachedLocomotion!.InputIsForwardEnoughForRun())
            {
                ToggleSprint = false;
                return;
            }

            if (InputMapper.GetButtonDown.Invoke(InputAction.Run, _cachedPlayer.InputFilter))
            {
                if (ToggleSprint)
                {
                    ToggleSprint = false;
                }
                else
                {
                    if (Clock.Time - _lastTapTime < Configuration.ToggleBufferTime)
                        ToggleSprint = true;
                    _lastTapTime = Clock.Time;
                }
            }
        }

        public static void StopSprint() => ToggleSprint = false;
    }
}
