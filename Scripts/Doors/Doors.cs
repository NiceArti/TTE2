using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorStudio.Utils
{
    public class Doors : MonoBehaviour
    {
        [SerializeField]
        private Transform pivot;

        [SerializeField]
        private bool _switchedPosition;

        [SerializeField]
        private bool _opened;

        [SerializeField]
        [Range(TIMEFRAME_MIN, TIMEFRAME_MAX)]
        private float _timeFrame;
        

        [SerializeField]
        private bool _allowRotation;

        private const float TIMEFRAME_MIN = 0.1f;
        private const float TIMEFRAME_MAX = 2f;

        public bool SwitchedPosition
        {
            get => _switchedPosition;
        }

        public bool Opened
        {
            get => _opened;
        }

        public Doors() 
        {
            _switchedPosition = true;
            _allowRotation = true;
            _opened = false;
            _timeFrame = 0.5f;
        }



        public bool Open()
        {
            DoorRotation(-90f, _timeFrame);
            _opened = true;

            return true;
        }

        public bool Close()
        {
            DoorRotation(90, _timeFrame);
            _opened = false;

            return true;
        }

        private void DoorRotation(float to, float timeFrame)
        {
            _timeFrame = timeFrame > TIMEFRAME_MAX ? TIMEFRAME_MAX : timeFrame;

            float rotationFromCurrent;

            if (_allowRotation == true) 
            {
                rotationFromCurrent = pivot.rotation.eulerAngles.y;
                rotationFromCurrent += to;
                LeanTween.rotateY(pivot.gameObject, rotationFromCurrent, timeFrame);
            }

            _allowRotation = _switchedPosition = false;
            StartCoroutine(AfterDoorSettedToPos(timeFrame));
        }


        // Hook
        protected IEnumerator AfterDoorSettedToPos(float timeFrame) 
        {
            yield return new WaitForSeconds(timeFrame);
            _allowRotation = _switchedPosition = true;
        }


        private void Awake() 
        {
            if(pivot == null)
                pivot = null;
        }
    }
}
