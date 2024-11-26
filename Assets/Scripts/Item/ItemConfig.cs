using UnityEngine;

namespace WinterUniverse
{
    public abstract class ItemConfig : TypeConfig
    {
        [Header("Item Information")]
        [SerializeField] protected ItemTypeConfig _itemType;
        [SerializeField] protected GameObject _model;
        [SerializeField] protected float _weight = 1f;
        [SerializeField] protected int _maxCountInStack = 1;
        [SerializeField] protected int _price = 100;
        [SerializeField] protected float _rating = 1f;

        public ItemTypeConfig ItemType => _itemType;
        public GameObject Model => _model;
        public float Weight => _weight;
        public int MaxCountInStack => _maxCountInStack;
        public int Price => _price;
        public float Rating => _rating;

        public abstract bool Use(PawnController character, bool fromInventory = true);
    }
}