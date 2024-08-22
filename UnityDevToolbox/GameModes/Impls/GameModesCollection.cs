using System.Collections.Generic;
using UnityEngine;


namespace UnityDevToolbox.Impls
{
    [CreateAssetMenu(fileName = "GameModesCollection", menuName = "Game Modes/Create GameModesCollection")]
    public class GameModesCollection : ScriptableObject
    {
        public List<BaseGameMode> mValues = new List<BaseGameMode>();

        public TGameMode GetMode<TGameMode>() where TGameMode : BaseGameMode
        {
            int index = mValues.FindIndex(gameMode => gameMode.GetType() == typeof(TGameMode));
            if (index == -1)
            {
                return null;
            }

            return (TGameMode)mValues[index];
        }
    }
}
