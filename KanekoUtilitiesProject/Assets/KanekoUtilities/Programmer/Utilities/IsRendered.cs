using System.Collections;
using UnityEngine;

namespace KKUtilities
{
    public class IsRendered : MonoBehaviour
    {
        public bool isRendered { get; private set; }

        void Update()
        {
            isRendered = false;
        }

        void OnWillRenderObject()
        {
            if (Camera.current.name == "MainCamera")
            {
                isRendered = true;
            }
        }
    }
}