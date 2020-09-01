namespace FOA {
    class Item {
        public string Name { get; }
        public int Count { get; set; }

        public Item(string name, int count) {
            Name = name;
            Count = count;
        }
    }
}
