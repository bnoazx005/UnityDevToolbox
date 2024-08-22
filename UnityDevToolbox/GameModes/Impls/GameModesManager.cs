using System.Collections.Generic;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolbox.Impls
{
    public class GameModesManager : IGameModesManager
    {
        private Stack<BaseGameMode> mModesContext = new Stack<BaseGameMode>();

        private GameModesCollection mGameModes = null;

        public GameModesManager(GameModesCollection gameModes)
        {
            mGameModes = gameModes;
        }

        public void SwitchMode(BaseGameMode newMode)
        {
            if (newMode == null)
            {
                UnityEngine.Debug.LogError($"[GameModesManager] Invalid argument newMode is null");
                return;
            }

            var currMode = GetCurrGameMode();
            if (currMode != null)
            {
                currMode.OnExit();
                mModesContext.Pop();
            }

            mModesContext.Push(newMode);

            newMode.Owner = this;
            newMode.OnEnter();
        }

        public void PushMode(BaseGameMode newMode)
        {
            if (newMode == null)
            {
                UnityEngine.Debug.LogError($"[GameModesManager] Invalid argument newMode is null");
                return;
            }

            mModesContext.Push(newMode);

            newMode.Owner = this;
            newMode.OnEnter();
        }

        public void PopMode()
        {
            var currGameMode = GetCurrGameMode();
            if (currGameMode == null)
            {
                UnityEngine.Debug.LogError($"[GameModesManager] Try to pop state but current is none");
                return;
            }

            currGameMode.OnExit();
            mModesContext.Pop();

            // \todo Is we need here another OnEnter for previous state
        }

        public TGameMode GetMode<TGameMode>() where TGameMode : BaseGameMode => mGameModes.GetMode<TGameMode>();

        public void Update(float dt)
        {
            GetCurrGameMode()?.OnUpdate(dt);
        }

        private BaseGameMode GetCurrGameMode() => mModesContext.Count > 0 ? mModesContext.Peek() : null;
    }
}
