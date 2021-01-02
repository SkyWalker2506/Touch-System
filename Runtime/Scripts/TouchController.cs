using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TouchSystem
{
    public class TouchController : IDisposable
    {
        #region Field

        LayerMask interactableMask;
        List<TouchWorldPoint> worldPoints = new List<TouchWorldPoint>();
        float pointInterval = .01f;
        float rayLength = 100;
        ScreenTouch screenTouch;
        public bool isListeningTouch;
        #endregion Field

        #region Action

        public Action OnFirstWorldPointAdded;
        public Action<Vector3> OnWorldPointAdded;
        public Action OnAllWorldPointsCreated;

        #endregion Action

        #region Constructor

        public TouchController(TouchData touchData)
        {
            interactableMask = touchData.InteractableMask;
            pointInterval = touchData.TouchInterval;
            rayLength = touchData.RayLength;
            screenTouch = new GameObject("Screen Touch").AddComponent<ScreenTouch>();
            StartListeningTouch();
        }

        #endregion Constructor

        #region Dispose

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
                if (disposing)
                {
                    StopListeningTouch();
                }
        }

        #endregion Dispose

        #region Touch Listener

        public void StartListeningTouch()
        {
            if (isListeningTouch) return;
            screenTouch.OnTouchStarted += ResetWorldPoints;
            screenTouch.OnTouch += TryAddWorldPoint;
            screenTouch.OnTouchEnded += FinishAddingWorldPoints;
            isListeningTouch = true;
        }

        public void StopListeningTouch()
        {
            if (!isListeningTouch) return;
            screenTouch.OnTouchStarted -= ResetWorldPoints;
            screenTouch.OnTouch -= TryAddWorldPoint;
            screenTouch.OnTouchEnded -= FinishAddingWorldPoints;
            isListeningTouch = false;
        }


        #endregion Touch Listener

        #region WorldPoint

        public void TryAddWorldPoint(Vector2 touchPoint)
        {
            var worldPoint = GetWorldPoint(touchPoint, rayLength);
            if (!worldPoint.IsPointSet)
                return;
            if (worldPoints.Count == 0)
            {
                AddWorldPoint(worldPoint);
                OnFirstWorldPointAdded?.Invoke();
            }
            else
            {
                if (Vector3.Distance(worldPoint.Position, worldPoints.LastOrDefault().Position)>pointInterval)
                {
                    AddWorldPoint(worldPoint);
                }
                else
                    return ;
            }
        }

        public TouchWorldPoint GetWorldPoint(Vector2 touchPoint, float? rayLength = null)
        {
            if (rayLength == null)
                rayLength = this.rayLength;
            return new TouchWorldPoint(touchPoint, interactableMask, (float)rayLength);
        }

        public Vector3[] GetWorldPositions()
        {
            var length = worldPoints.Count;
            var positions = new Vector3[length];
            for (int i = 0; i < length; i++)
            {
                positions[i] = worldPoints[i].Position;
            }
            return positions;
        }

        public Vector3 GetFirstWorldPosition()
        {
            return worldPoints[0].Position; 
        }

        void AddWorldPoint(TouchWorldPoint worldPoint)
        {
            worldPoints.Add(worldPoint);
            OnWorldPointAdded?.Invoke(worldPoint.Position);
        }

        void ResetWorldPoints()
        {
            worldPoints.Clear();
        }

        void FinishAddingWorldPoints()
        {
            OnAllWorldPointsCreated?.Invoke();
        }

        #endregion WorldPoint
    }

}