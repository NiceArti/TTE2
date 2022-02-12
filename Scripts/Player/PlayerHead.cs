using UnityEngine;
using MeteorStudio.Utils;

namespace MeteorStudio.TryToEscape
{
    public class PlayerHead : MonoBehaviour
    {
        private RaycastHit hit;

        [SerializeField]
        private Doors _doors;


        public bool IsDoor()
        {
            _doors = !hit.collider.gameObject.GetComponent<Doors>() ?
                null : hit.collider.gameObject.GetComponent<Doors>();

            if (_doors)
                return true;

            return false;
        }

        private void Update() 
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 3f, Color.green);

            if (Physics.Raycast(ray, out hit))
            {
                IsDoor();
                IsOnSelected();

                Debug.Log(IsOnSelected());

                DoorMechanism();
                SelectMechanism();

            }
        }


        private void DoorMechanism() 
        {
            if (hit.distance <= 3f && IsDoor() && _doors.Opened == false)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _doors.Open();
                }
            }
            else if (hit.distance <= 3f && IsDoor() && _doors.Opened == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _doors.Close();
                }
            }
        }

        private void SelectMechanism()
        {
            if (IsOnSelected() && hit.distance <= 3f)
            {
                AdvancedRay<Outline>.Select(hit.collider.gameObject.GetComponent<Outline>(), "first");
                hit.collider.gameObject.GetComponent<Outline>().enabled = true;
            }
            else if(!IsOnSelected() || hit.distance > 3f)
                AdvancedRay<Outline>.GetByTag("first").enabled = false;
        }

        private bool IsOnSelected() 
        {
            if (hit.collider.gameObject.GetComponent<Outline>() != null)
                return true;

            return false;
        }

        private void Awake()
        {
            if (_doors == null)
                _doors = null;

            //if (_selection == null)
            //    _selection = null;
        }
    }
}
