using UnityEngine;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class Card : MonoBehaviour, Interfaces.IInteractable
    {
        [Header("Data")]
        [SerializeField] private CardScriptable cardScriptable;
        public CardScriptable CardScriptable
        {
            get { return cardScriptable; }
            set
            {
                cardScriptable = value;
                SetCardArt();
                SetCardSuit();
                SetCardValue();
            }
        }

        [SerializeField] private CardBackScriptable cardBackScriptable;
        public CardBackScriptable CardBackScriptable
        {
            get { return cardBackScriptable; }
            set
            {
                cardBackScriptable = value;
                SetCardArt();
            }
        }

        [Header("Components")]
        [SerializeField] private Animator animator;
        public Animator Animator { get { return animator; } private set { animator = value; } }

        [Header("Sprites")]
        [SerializeField] private SpriteRenderer suitSprite;
        public SpriteRenderer SuitSprite { get { return suitSprite; } private set { suitSprite = value; } }

        [SerializeField] private SpriteRenderer valueSprite;
        public SpriteRenderer ValueSprite { get { return valueSprite; } private set { valueSprite = value; } }

        [SerializeField] private SpriteRenderer frameSprite;
        public SpriteRenderer FrameSprite { get { return frameSprite; } private set { frameSprite = value; } }

        [SerializeField] private SpriteRenderer artSprite;
        public SpriteRenderer ArtSprite { get { return artSprite; } private set { artSprite = value; } }

        [SerializeField] private SpriteRenderer backSprite;
        public SpriteRenderer BackSprite { get { return backSprite; } private set { backSprite = value; } }


        public Enums.CardSuits Suit { get; private set; }
        public int Value { get; private set; }

        private bool isFacing = true;

        #region Unity Methods

        public void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (animator == null)
                Animator = GetComponent<Animator>();

            if (artSprite == null)
                ArtSprite = GetComponentInChildren<SpriteRenderer>();
        }

        #endregion

        #region Card Scriptable Methods

        public void SetCardArt()
        {
            if (CardScriptable != null)
            {
                SuitSprite.sprite = CardScriptable.SuitOverlay;
                ValueSprite.sprite = CardScriptable.ValueOverlay;
                FrameSprite.sprite = CardScriptable.FrameOverlay;
                ArtSprite.sprite = CardScriptable.ArtBackground;
            }

            if (CardBackScriptable != null)
                BackSprite.sprite = CardBackScriptable.Sprite;
        }

        public void SetCardSuit()
        {
            if (CardScriptable == null)
            {
                Debug.LogWarning("Card Scriptable not set on " + gameObject.name);
                return;
            }

            Suit = CardScriptable.Suit;
        }

        public void SetCardValue()
        {
            if (CardScriptable == null)
            {
                Debug.LogWarning("Card Scriptable not set on " + gameObject.name);
                return;
            }

            Value = CardScriptable.Value;
        }

        #endregion

        #region Movement Animations

        public void FlipCard()
        {
            isFacing = !isFacing;
            Animator.SetBool("IsFacing", isFacing);
        }

        #endregion

        #region IInteractable Interface

        public void OnLeftClick()
        {
            Debug.Log("Left Clicked: " + CardScriptable.name);
        }

        public void OnRightClick()
        {
            Debug.Log("Right Clicked: " + CardScriptable.name);
        }

        #endregion
    }
}
