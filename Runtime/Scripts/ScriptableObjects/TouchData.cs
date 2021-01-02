using UnityEngine;

namespace TouchSystem
{
    [CreateAssetMenu(fileName = "TouchData", menuName = "TouchSystem/TouchData")]
    public class TouchData : ScriptableObject
    {
        public float RayLength = 100f;
        [Tooltip("Determines interactable layers for touch")]
        public LayerMask InteractableMask;
        [Tooltip("Determines how much distance will be between two consecutive touch point")]
        [Range(.01f, 1)]
        public float TouchInterval = .01f;
    }
}