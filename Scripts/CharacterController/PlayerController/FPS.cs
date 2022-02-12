using UnityEngine;


namespace MeteorStudio.CharacterController
{
    public class FPS : Entity
    {
        [SerializeField]
        [Range(30, 180)]
        private float _lookAngleUp;

        [SerializeField]
        [Range(30, 180)]
        private float _lookAngleDown;

        [SerializeField]
        [Range(3, 15)]
        private float _roatationSensitivity;

        protected Vector3 _originalScale;

        private float _currentSpeed;


        public FPS() : base()
        {
            _lookAngleDown = 60f;
            _lookAngleUp = 40f;
            _roatationSensitivity = 10f;
            _currentSpeed = Speed;
        }



        protected override bool Move() 
        {
            IsRunning = Input.GetKey(KeyCode.LeftShift) && AllowJump == true ? true : false;
            Sprint = IsRunning == true ? 5f : DEFAULT_SPRINT_MIN;

            base.Move();
             
            return true;
        }
        
        protected override bool Jump() 
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true && AllowJump == true)
                base.Jump();

            return true;
        }

        protected bool Crounch()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded() == true)
            {
                base.Crounch(_originalScale.y * _crounchHeight);
                AllowJump = AllowRun = false;
                Speed *= CrounchSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                base.Crounch(_originalScale.y);
                AllowJump = AllowRun = true;
                Speed = _currentSpeed;
            }


            return true;
        }

        protected bool HeadRotation()
        {
            base.HeadRotation(_lookAngleUp, _lookAngleDown, _roatationSensitivity);
            return true;
        }
    }
}
