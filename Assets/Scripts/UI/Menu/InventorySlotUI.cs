using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private ItemConfig _item;

        public void Setup(ItemStack stack)
        {
            _item = stack.Item;
            _text.text = $"{stack.Item.DisplayName}: {stack.Amount}";
        }

        public void Use()
        {
            if (_item.CanUse(GameManager.StaticInstance.Player, out string error))
            {
                _item.Use(GameManager.StaticInstance.Player);
            }
            else
            {
                GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification(error);
            }
        }
    }
}