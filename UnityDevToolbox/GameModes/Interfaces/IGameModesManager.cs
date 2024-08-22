using UnityDevToolbox.Impls;


namespace UnityDevToolbox.Interfaces
{
    public interface IGameModesManager
    {
        void SwitchMode(BaseGameMode newMode);
        void PushMode(BaseGameMode newMode);
        void PopMode();

        TGameMode GetMode<TGameMode>() where TGameMode : BaseGameMode;

        void Update(float dt);
    }
}
