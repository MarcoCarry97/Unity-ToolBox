using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Cameras
{
    public class CameraFollow : MonoBehaviour
    {
        private new Camera camera;

        [SerializeField]
        private Vector3 distance;


        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            GameObject game = GameObject.FindGameObjectWithTag("MainCharacter");
            if (game != null)
            {
                camera.transform.position = game.transform.position + distance;
            }
        }


    }
}
