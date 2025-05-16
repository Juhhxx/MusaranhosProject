using System;
using UnityEngine;

namespace Map
{
    public class CaveExitWall : InteractiveObject
    {
        [SerializeField] private GameObject dynamiteObj;

        public override void Interact()
        {
            dynamiteObj.GetComponent<Dynamite>().Enable();
            print("Interact");
            base.Interact();
        }
    }
}