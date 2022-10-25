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

            if (GUILayout.Button("Start New Season"))
            {
                if (!Application.isPlaying)
                {
                    Debug.LogError("Must be In Play Mode to start new Season");
                    return;
                }

                SeasonSettings seasonSettings = new SeasonSettings()
                {
                    NumberOfPlayers = SeasonManager.Instance.NumberOfPlayers
                };

                seasonManager.LoadNewSeason(seasonSettings);
                seasonManager.StartSeason();
            }
        }
    }
}