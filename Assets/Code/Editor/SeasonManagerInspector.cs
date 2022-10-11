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

                seasonManager.LoadNewSeason(new SeasonSettings());
            }
        }
    }
}