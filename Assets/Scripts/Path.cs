using UnityEngine;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        public Transform[] Waypoints { get; private set; }
        public Transform this[int index] => Waypoints[index];

        private void Awake()
        {
            Waypoints = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; ++i)
            {
                Waypoints[i] = transform.GetChild(i);
            }
        }
    }
}

