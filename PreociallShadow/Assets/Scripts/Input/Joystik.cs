using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class Joystik : MonoBehaviour
    {
        public enum CursorState
        {
            OnWaiting,
            OnAction,
            OnReturn
        }

        public Transform Joystic;
        public Transform Cursor;
        public Transform CursorHelper;
        public Player.Player Player;
        public float CursorSpeed;
        public float CursorRadiusBorders;
        public float CursorRadiusAction;
        public float ActionDelta;
        private Animator _animator;
        private Vector3 _cursorStartPosition;
        private CursorState _cursorState = CursorState.OnWaiting;

        void Start()
        {
            _animator = Player.GetComponent<Animator>();
            _cursorStartPosition = Cursor.position;
        }

        public void Down()
        {
            _cursorState = CursorState.OnAction;

            Player.PlayerBody.drag = 0.0f;
        }

        public void Up()
        {
            _cursorState = CursorState.OnReturn;

            Player.SetDefaultPlayerState();

            Player.PlayerBody.drag = 0.0f;
        }

        private void Update()
        {
            switch (_cursorState)
            {
                case CursorState.OnAction:

                    float cursorAngleDirection = DefineCursorAngleDirection();

                    CheckCursorRadiusBorders(cursorAngleDirection);
                    MoveCursor();
                    DefinePlayerAction(cursorAngleDirection);
                    break;

                case CursorState.OnReturn:

                    ReturnCursor();
                    break;
            }
        }

        private void DefinePlayerAction(float cursorAngleDirection)
        {
            if (IsCursorActivated(cursorAngleDirection))
            {
                if (cursorAngleDirection > 360.0f - ActionDelta || cursorAngleDirection < 0.0f + ActionDelta)
                {
                    print("Jump");
                }

                if (cursorAngleDirection > 45.0f - ActionDelta && cursorAngleDirection < 45.0f + ActionDelta)
                {
                    print("forward jump");
                }

                if (cursorAngleDirection > 90.0f - ActionDelta && cursorAngleDirection < 90.0f + ActionDelta)
                {
                    print("walk forward");

                    Player.WalkForward();
                }

                if (cursorAngleDirection > 135.0f - ActionDelta && cursorAngleDirection < 135.0f + ActionDelta)
                {
                    print("down forward");
                }

                if (cursorAngleDirection > 180.0f - ActionDelta && cursorAngleDirection < 180.0f + ActionDelta)
                {
                    print("down");
                }

                if (cursorAngleDirection > 225.0f - ActionDelta && cursorAngleDirection < 225.0f + ActionDelta)
                {
                    print("down backward");
                }

                if (cursorAngleDirection > 270.0f - ActionDelta && cursorAngleDirection < 270.0f + ActionDelta)
                {
                    print("walk backward");
                  
                    Player.WalkBackward();
                }

                if (cursorAngleDirection > 315.0f - ActionDelta && cursorAngleDirection < 315.0f + ActionDelta)
                {
                    print("jump backward");
                }
            }
        }

        private bool IsCursorActivated(float cursorAngleDirection)
        {
            float cos = (float)Math.Round(Mathf.Cos(cursorAngleDirection * Mathf.Deg2Rad) * CursorRadiusAction, 2);
            float sin = (float)Math.Round(Mathf.Sin(cursorAngleDirection * Mathf.Deg2Rad) * CursorRadiusAction, 2);

            if (Cursor.position.y > CursorHelper.position.y + Mathf.Abs(cos))
            {
                return true;
            }

            if (Cursor.position.y < CursorHelper.position.y - Mathf.Abs(cos))
            {
                return true;
            }

            if (Cursor.position.x > CursorHelper.position.x + Mathf.Abs(sin))
            {
                return true;
            }

            if (Cursor.position.x < CursorHelper.position.x - Mathf.Abs(sin))
            {
                return true;
            }

            return false;
        }

        private void ReturnCursor()
        {
            Cursor.position = Vector3.MoveTowards(Cursor.position, _cursorStartPosition, CursorSpeed);

            if (Cursor.position.x.Equals(_cursorStartPosition.x))
            {
                Cursor.position = _cursorStartPosition;

                _cursorState = CursorState.OnWaiting;
            }
        }

        private void MoveCursor()
        {
            Cursor.position = Vector3.MoveTowards(Cursor.position, UnityEngine.Input.mousePosition, CursorSpeed);
        }

        private float DefineCursorAngleDirection()
        {
            Vector3 direction = new Vector3(CursorHelper.position.x - UnityEngine.Input.mousePosition.x, CursorHelper.position.y - UnityEngine.Input.mousePosition.y, CursorHelper.position.z);

            CursorHelper.up = direction;

            float angle = Vector3.Angle(Cursor.up, CursorHelper.up);
            float sign = Mathf.Sign(Vector3.Cross(Cursor.up, CursorHelper.up).z);

            angle *= -sign;
            angle += 180.0f;
            angle = (float)Math.Round(angle, 2);

            return angle;
        }

        private void CheckCursorRadiusBorders(float cursorAngleDirection)
        {
            float cos = (float)Math.Round(Mathf.Cos(cursorAngleDirection * Mathf.Deg2Rad) * CursorRadiusBorders, 2);
            float sin = (float)Math.Round(Mathf.Sin(cursorAngleDirection * Mathf.Deg2Rad) * CursorRadiusBorders, 2);

            if (Cursor.position.y > CursorHelper.position.y + Mathf.Abs(cos))
            {
                Cursor.position = new Vector2(Cursor.position.x, CursorHelper.position.y + cos);
            }

            if (Cursor.position.y < CursorHelper.position.y - Mathf.Abs(cos))
            {
                Cursor.position = new Vector2(Cursor.position.x, CursorHelper.position.y + cos);
            }

            if (Cursor.position.x > CursorHelper.position.x + Mathf.Abs(sin))
            {
                Cursor.position = new Vector2(CursorHelper.position.x + sin, Cursor.position.y);
            }

            if (Cursor.position.x < CursorHelper.position.x - Mathf.Abs(sin))
            {
                Cursor.position = new Vector2(CursorHelper.position.x + sin, Cursor.position.y);
            }
        }
    }
}




