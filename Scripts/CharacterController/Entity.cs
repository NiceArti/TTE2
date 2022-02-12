using UnityEngine;

namespace MeteorStudio.CharacterController
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _entity;

        [SerializeField]
        private Camera _head;

        [SerializeField]
        [Range(3, 25)]
        private float _speed;

        [SerializeField]
        [Range(0.0f, 0.5f)]
        private float _crounchSpeed;

        [SerializeField]
        [Range(0.1f, 1.0f)]
        protected float _crounchHeight; // value in percents


        protected const float DEFAULT_SPRINT_MIN = 0;
        protected const float DEFAULT_SPRINT_MAX = 10;

        [SerializeField]
        [Range(DEFAULT_SPRINT_MIN, DEFAULT_SPRINT_MAX)]
        private float _sprint;


        [SerializeField]
        [Range(3, 10)]
        private float _jumpForce;

        [SerializeField]
        private bool _allowJump;

        [SerializeField]
        private bool _allowRun;

        [SerializeField]
        private bool _allowCrunch;

        [SerializeField]
        private bool _isRunning;

        [SerializeField]
        private bool _isGrounded;

        private float _yaw;
        private float _pitch;

 
        private float maxVelocityChange = 5f;
      

        public Entity()
        {
            _speed = 5;
            _jumpForce = 5;
            _allowJump = _allowRun = _isGrounded = _allowCrunch = true;
            _isRunning = false;
            _yaw = _pitch = 0f;
            _sprint = DEFAULT_SPRINT_MIN;
            _crounchHeight = 0.5f;
        }

        public Entity(float speed)
        {
            _speed = speed;
            _jumpForce = 5;
            _allowJump = _allowRun = _isGrounded = _allowCrunch = true;
            _isRunning = false;
            _yaw = _pitch = 0f;
            _sprint = DEFAULT_SPRINT_MIN;
            _crounchHeight = 0.5f;
        }


        
        protected float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        protected float CrounchSpeed
        {
            get => _crounchSpeed;
            set => _crounchSpeed = value;
        }
        protected bool AllowJump
        {
            get => _allowJump;
            set => _allowJump = value;
        }
        protected bool AllowRun
        {
            get => _allowRun;
            set => _allowRun = value;
        }
        protected bool AllowCrunch
        {
            get => _allowCrunch;
            set => _allowCrunch = value;
        }
        protected float Sprint
        {
            get => _sprint;
            set => _sprint = value;
        }
        protected bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }

        protected virtual bool Move()
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity) * (_speed + _sprint);

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = _entity.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);


            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            _entity.AddForce(velocityChange, ForceMode.VelocityChange);

            return true;
        }

        protected virtual bool Jump()
        {
            _entity.AddForce(0f, _jumpForce, 0f, ForceMode.Impulse);

            return true;
        }

        protected virtual bool Crounch(float height)
        {
            _allowJump = _allowRun = false;
            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);

            return true;
        }


        protected bool HeadRotation(float angleUp, float angleDown, float sensitivity)
        {
            _yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            _pitch -= sensitivity * Input.GetAxis("Mouse Y");

            // Clamp pitch between lookAngle
            _pitch = Mathf.Clamp(_pitch, -angleUp, angleDown);

            transform.localEulerAngles = new Vector3(0, _yaw, 0);
            _head.transform.localEulerAngles = new Vector3(_pitch, 0, 0);

            return true;
        }

        // Sets isGrounded based on a raycast sent straigth down from the player object
        protected bool IsGrounded()
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
            Vector3 direction = transform.TransformDirection(Vector3.down);
            float distance = .75f;

            Debug.DrawRay(origin, direction * distance, Color.red);

            _isGrounded = !Physics.Raycast(origin, direction, out RaycastHit hit, distance) ? false : true;

            return _isGrounded;
        }
    }
}
