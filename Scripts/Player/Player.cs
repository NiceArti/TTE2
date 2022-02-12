using UnityEngine;
using MeteorStudio.CharacterController;
using MeteorStudio.Utils;

namespace MeteorStudio.TryToEscape
{
    class Player : FPS
    {
        public Player() : base() { }


        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            HeadRotation();
            Jump();
            Crounch();
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _originalScale = transform.localScale;
        }
    }
}
