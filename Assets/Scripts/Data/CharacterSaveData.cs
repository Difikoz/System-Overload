namespace WinterUniverse
{
    [System.Serializable]
    public class CharacterSaveData
    {
        public string CharacterName = "Faceless";

        public string Race = "Human";
        public string Gender = "Male";
        public string Faction = "Tramps";

        public int Level = 1;
        public int Experience;

        public float Health;
        public float Energy;

        public TransformValues Transform = new();
        public TransformValues RespawnTransform = new();

        public SerializableDictionary<string, float> BossCooldown = new();

        public SerializableDictionary<string, int> InventoryStacks = new();

        public string WeaponInRightHand = "Unarmed";
        public string WeaponInLeftHand = "Unarmed";

        public string HeadArmor = "Empty";
        public string ChestArmor = "Empty";
        public string PantsArmor = "Empty";
        public string BootsArmor = "Empty";
        public string GlovesArmor = "Empty";
    }
}