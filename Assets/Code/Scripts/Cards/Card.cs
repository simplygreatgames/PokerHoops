using UnityEngine;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class Card : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Animator animator;
        public Animator Animator { get { return animator; } private set { animator = value; } }

        [SerializeField] private SpriteRenderer spriteRendererFront;
        public SpriteRenderer SpriteRendererFront { get { return spriteRendererFront; } private set { spriteRendererFront = value; } }

        [SerializeField] private SpriteRenderer spriteRendererBack;
        public SpriteRenderer SpriteRendererBack { get { return spriteRendererBack; } private set { spriteRendererBack = value; } }

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

            if (spriteRendererFront == null)
                SpriteRendererFront = GetComponentInChildren<SpriteRenderer>();
        }

        #endregion

        #region Card Scriptable Methods

        public void SetCardArt()
        {
            if (CardScriptable != null)
                SpriteRendererFront.sprite = CardScriptable.Sprite;

            if (CardBackScriptable != null)
                SpriteRendererBack.sprite = CardBackScriptable.Sprite;
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
    }
}
