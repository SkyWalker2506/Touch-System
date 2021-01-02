using System;
using UnityEngine;

namespace TouchSystem
{
    public class ScreenTouch: MonoBehaviour
    {
        #region Action

        public Action OnTouchStarted;
        public Action OnTouchEnded;
        public Action<Vector2> OnTouch;

        #endregion Action

        #region ContinuesCallback

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnTouchStarted?.Invoke();
            if (Input.GetMouseButton(0))
                OnTouch?.Invoke(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
                OnTouchEnded?.Invoke();
        }

        #endregion ContinuesCallback

    }
}