using Assets.Scripts.UI.Overlay;
using UnityEngine;

namespace Assets.Scripts
{
    public class CircularControls : MonoBehaviour
    {
        public float RotateSpeed = 2f;
        public float RotateawaySpeed = 2f;
        public float Radius = 2.4f;

        public GameController GameController;


        public bool touchControls = false;
        private float Width => GetComponent<BoxCollider2D>().size.x;


        public Vector2 rotation;
        public GameObject _centre;
        private float _angle;


        private void Update()
        {

            if (!GameController.IsGamePaused)
            {
                if (Input.touchCount > 0)
                {
                    var touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (touch.deltaPosition.x > 0 && CanMoveRight())
                        {

                            _angle += touch.deltaPosition.x / 1000;
                            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                            transform.position = _centre.transform.position + offset;
                        }
                        else if (touch.deltaPosition.x < 0 && CanMoveLeft())
                        {
                            _angle += touch.deltaPosition.x / 1000;
                            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                            transform.position = _centre.transform.position + offset;
                        }
                    }
                }

                if (Input.GetKey(KeyCode.RightArrow) && CanMoveRight())
                {
                    _angle += RotateSpeed * Time.deltaTime;

                    var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                    transform.position = _centre.transform.position + offset;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && CanMoveLeft())
                {
                    _angle -= RotateSpeed * Time.deltaTime;
                    var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                    transform.position = _centre.transform.position + offset;
                }


                Vector2 direction = transform.position - _centre.transform.position;

                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);

                transform.rotation = rotation;

            }



        }

        private bool CanMoveRight()
        {
            return CanMove(Vector2.right, 1 + Width / 2);

        }

        private bool CanMoveLeft()
        {
            return CanMove(Vector2.left, 1 + Width / 2);
        }

        private bool CanMove(Vector2 direction, float size)
        {
            Vector2 a = transform.position;


            var hit = Physics2D.Raycast(a, direction, size, LayerMask.GetMask("World Border"));


            Debug.DrawRay(a, direction * size, Color.red, 10);

            return hit.collider == null;
        }
    }
}
