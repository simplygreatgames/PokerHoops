using UnityEditor;
using UnityEngine;
using System;

namespace SimplyGreatGames.PokerHoops
{
    public class CardImporterWindow : EditorWindow
    {
        public string Prefix;

        public Sprite FrameSprite;

        public Sprite[] ArtSprites;
        public Sprite[] SuitSprites;
        public Sprite[] ValueSprites;

        [MenuItem("Tools/Create/New Cards")]
        public static void OpenEditorWindow()
        {
            GetWindow<CardImporterWindow>("Card Importer");
        }

        public void OnGUI()
        {
            SetWindowProperties();

            if (GUILayout.Button("Create Cards"))
                BuildCardScriptables();

            if (GUILayout.Button("Reset Import Data"))
                ResetWindowProperties();
        }

        private void SetWindowProperties()
        {
            ScriptableObject target = this;
            SerializedObject serializedObj = new SerializedObject(target);

            // Info
            EditorGUILayout.LabelField("Info");
            Prefix = EditorGUILayout.TextField("Prefix", Prefix);

            // Sprites
            EditorGUILayout.LabelField("Art");
            SerializedProperty frameSprites = serializedObj.FindProperty("FrameSprite");
            EditorGUILayout.PropertyField(frameSprites, true);

            SerializedProperty artSprites = serializedObj.FindProperty("ArtSprites");
            EditorGUILayout.PropertyField(artSprites, true);

            SerializedProperty suiteSprites = serializedObj.FindProperty("SuitSprites");
            EditorGUILayout.PropertyField(suiteSprites, true);

            SerializedProperty valueSprites = serializedObj.FindProperty("ValueSprites");
            EditorGUILayout.PropertyField(valueSprites, true);

            serializedObj.ApplyModifiedProperties();
        }
        private void ResetWindowProperties()
        {
            ArtSprites = null;
            FrameSprite = null;
            ValueSprites = null;
            SuitSprites = null;
            Prefix = string.Empty;
        }

        public void BuildCardScriptables()
        {
            if (ErrorHandlingResult() == false)
            {
                Debug.LogError("Error Found! Aborting, Check Console message above for description");
                return;
            }

            foreach (Sprite suitSprite in SuitSprites)
            {
                Enums.CardSuits cardSuit = ParseSuit(suitSprite);
                Sprite artSprite;
                Sprite valueSprite;

                for (int i = 0; i < ArtSprites.Length; i++)
                {
                    artSprite = ArtSprites[i];
                    valueSprite = ValueSprites[i];

                    GenerateScriptable(cardSuit, suitSprite, artSprite, valueSprite);
                }
            }
        }

        private void GenerateScriptable(Enums.CardSuits suit, Sprite suitSprite, Sprite artSprite, Sprite valueSprite)
        {
            CardScriptable newCardScriptable = CreateInstance<CardScriptable>();

            newCardScriptable.Suit = suit;
            newCardScriptable.FrameOverlay = FrameSprite;
            newCardScriptable.SuitOverlay = suitSprite;
            newCardScriptable.ArtBackground = artSprite;
            newCardScriptable.ValueOverlay = valueSprite;
            newCardScriptable.Value = ParseValue(valueSprite);

            AssetDatabase.CreateAsset(newCardScriptable, "Assets/Code/Scriptables/Cards/Generated/" + Prefix + suit.ToString() + newCardScriptable.Value.ToString() + ".asset");
            AssetDatabase.SaveAssets();
        }

        #region Helpers

        private bool ErrorHandlingResult()
        {
            if (Prefix == string.Empty)
            {
                Debug.LogError("Error! Need Prefix");
                return false;
            }

            else if (FrameSprite == null)
            {
                Debug.LogError("Error! Need Frame Sprite");
                return false;
            }

            else if (ArtSprites == null || ArtSprites.Length != 13)
            {
                Debug.LogError("Error! Need 13 Art Sprites");
                return false;
            }

            else if (ValueSprites == null || ValueSprites.Length != 13)
            {
                Debug.LogError("Error! Need 13 Value Sprites");
                return false;
            }

            else if (SuitSprites == null || SuitSprites.Length != 4)
            {
                Debug.LogError("Error! Need 4 Suit Sprites");
                return false;
            }

            foreach (Sprite suitSprite in SuitSprites)
            {
                if (suitSprite.name != "Club" && suitSprite.name != "Spade" && suitSprite.name == "Diamond" && suitSprite.name == "Heart")
                {
                    Debug.LogError("Error! suite sprite named: " + suitSprite.name + " Suit Sprite names must be either: Club, Spade, Diamond, or Heart");
                    return false;
                }
            }

            return true;
        }

        private Enums.CardSuits ParseSuit(Sprite suitSprite)
        {
            if (suitSprite.name == "Club") return Enums.CardSuits.Club;
            else if (suitSprite.name == "Spade") return Enums.CardSuits.Spade;
            else if (suitSprite.name == "Diamond") return Enums.CardSuits.Diamond;
            else if (suitSprite.name == "Heart") return Enums.CardSuits.Heart;

            Debug.LogError("Warning! Name: " + suitSprite.name + " is not a valid name. Must be either Club, Spade, Diamond, Heart");

            return Enums.CardSuits.Club;
        }

        private int ParseValue(Sprite valueSprite)
        {
            int value;

            if (valueSprite.name == "K") value = 13;
            else if (valueSprite.name == "Q") value = 12;
            else if (valueSprite.name == "J") value = 11;
            else int.TryParse(valueSprite.name, out value);

            return value;
        }

        #endregion
    }
}
