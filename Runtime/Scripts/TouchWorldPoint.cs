using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TouchSystem
{
    public class TouchWorldPoint 
    {
        public Vector3 Position { get; set; }
        public bool IsPointSet;
        public TouchWorldPoint(Vector2 TouchPoint ,LayerMask interactableMask,float rayLength=100)
        {
            Ray ray = Camera.main.ScreenPointToRay(TouchPoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength, interactableMask))
            {
                Position = hit.point;
                IsPointSet = true;
            }
        }


    }

}