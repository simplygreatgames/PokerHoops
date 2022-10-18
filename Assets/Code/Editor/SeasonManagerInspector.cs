using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(SeasonManager))]
    public class SeasonManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SeasonManager seasonManager = (SeasonManager)target;

            if (GUILayout.Button("Load New Season"))
            {
                if (!Application.isPlaying)
                    return;

                SeasonSettings seasonSettings = new SeasonSettings()
                {
                    NumberOfPlayers = SeasonManager.Instance.NumberOfPlayers
                };

                seasonManager.LoadNewSeason(seasonSettings);
            }

            else if (GUILayout.Button("Start Season"))
            {
                if (!Application.isPlaying)
                    return;

                seasonManager.StartSeason();
            }
        }
    }
}