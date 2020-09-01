namespace FOA {
    /// <summary>
    /// A stack of systemic entities to be contained in inventories.
    /// </summary>
    class Item : SystemicEntity {
        public int Count { get; set; }

        public Item(string name, int count) {
            Name = name;
            Count = count;
        }
    }
}
