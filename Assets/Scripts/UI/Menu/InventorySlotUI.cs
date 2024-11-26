using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;

        private ItemConfig _item;

        public void Setup(ItemStack stack)
        {
            _item = stack.Item;
            _icon.sprite = _item.IconSprite;
            _text.text = $"{stack.Item.DisplayName}{(stack.Amount > 1 ? $" ({stack.Amount})" : "")}";
        }

        public void Use()
        {
            _item.Use(GameManager.StaticInstance.Player.Pawn);
        }
    }
}