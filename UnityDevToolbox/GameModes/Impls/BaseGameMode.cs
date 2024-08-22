using UnityDevToolbox.Interfaces;
using UnityEngine;


namespace UnityDevToolbox.Impls
{
    public abstract class BaseGameMode : ScriptableObject, IGameMode
    {
        public string mName = "Default Mode";

        public IGameModesManager Owner { get; set; } = null;

        public abstract void OnEnter();
        public abstract void OnExit();

        public abstract void OnUpdate(float dt);
    }

}
