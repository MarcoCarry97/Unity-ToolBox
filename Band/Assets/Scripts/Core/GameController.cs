using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Band.Control.Explorer2D;
using Band.GUI.Utils;
using System.Linq;
using Band.Utils;

namespace Band.Core
{
    public class GameController : MonoBehaviour,IController
    {
        private static GameController instance;

        public static GameController Instance { get; private set; }
        

        private List<IController> controllers;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            controllers=this.GetComponents<IController>().ToList<IController>();
        }

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this);
            else instance = this;
        }

        public T Get<T>() where T : IController
        {
            IController controller = null;
            foreach (IController cont in controllers)
                if (cont is T)
                    controller = cont;
            if (controller == null)
                throw new NullReferenceException($"There is no object of type ${typeof(T)}");
            return (T) controller;
        }

        public void Clear()
        {
            controllers.ForEach(controller => controller.Clear());
        }
    }
}
