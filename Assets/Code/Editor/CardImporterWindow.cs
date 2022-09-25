using UnityEditor;
using UnityEngine;
using System;

namespace SimplyGreatGames.PokerHoops
{
    public class CardImporterWindow : EditorWindow
    {
        public string Prefix;
        public Sprite[] CardSprites;

        [MenuItem("Tools/Create/New Cards")]
        public static void OpenEditorWindow()
        {
            GetWindow<CardImporterWindow>("Card Importer");
        }

        public void OnGUI()
        {
            EditorGUILayout.LabelField("Card Sprites");

            ScriptableObject target = this;
            SerializedObject serializedObj = new SerializedObject(target);

            Prefix = EditorGUILayout.TextField("Prefix", Prefix);

            SerializedProperty cardSprites = serializedObj.FindProperty("CardSprites");
            EditorGUILayout.PropertyField(cardSprites, true);
            
            serializedObj.ApplyModifiedProperties();

            if (GUILayout.Button("Create Cards"))
                CreateCardScriptables();
        }

        public void CreateCardScriptables() 
        {
            if (CardSprites == null || CardSprites.Length == 0)
            {
                Debug.Log("No Card Sprites Found!");
                return;
            }

            foreach (Sprite sprite in CardSprites)
            {
                char[] characters = sprite.name.ToCharArray();
                string number = string.Concat(characters[characters.Length - 2].ToString() + string.Concat(characters[characters.Length - 1].ToString()));

                if (characters[0] == 'C')
                    CreateCard(Enums.CardSuits.Clubs, sprite, number);

                else if (characters[0] == 'D')
                    CreateCard(Enums.CardSuits.Diamonds, sprite, number);

                else if (characters[0] == 'H')
                    CreateCard(Enums.CardSuits.Hearts, sprite, number);

                else if (characters[0] == 'S')
                    CreateCard(Enums.CardSuits.Spades, sprite, number);
            }

            CardSprites = null;
        }

        private void CreateCard(Enums.CardSuits suit, Sprite sprite, string number)
        {
            CardScriptable newCardScriptable = CreateInstance<CardScriptable>();

            newCardScriptable.Sprite = sprite;
            newCardScriptable.Suit = suit;
            newCardScriptable.Value = int.Parse(number);

            AssetDatabase.CreateAsset(newCardScriptable, "Assets/Code/Scriptables/Cards/Generated/" + Prefix + suit.ToString() + number + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
