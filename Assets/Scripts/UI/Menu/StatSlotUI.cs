using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class StatSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Setup(Stat stat)
        {
            _text.text = $"{stat.Data.DisplayName}: {stat.CurrentValue:0.##}{(stat.Data.IsPercent ? "%" : "")}";
        }
    }
}