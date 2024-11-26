using UnityEngine;

namespace WinterUniverse
{
    public abstract class TypeConfig : ScriptableObject
    {
        [SerializeField] protected string _displayName = "Name";
        [SerializeField, TextArea] protected string _description = "Description";
        [SerializeField] protected Color _textColor = Color.white;
        [SerializeField] protected Sprite _iconSprite;

        public string DisplayName => _displayName;
        public string Description => _description;
        public Color TextColor => _textColor;
        public Sprite IconSprite => _iconSprite;
    }
}