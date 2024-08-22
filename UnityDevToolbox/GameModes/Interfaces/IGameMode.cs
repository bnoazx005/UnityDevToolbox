namespace UnityDevToolbox.Interfaces
{
    internal interface IGameMode
    {
        IGameModesManager Owner { get; set; }

        void OnEnter();
        void OnExit();

        void OnUpdate(float dt);
    }
}
