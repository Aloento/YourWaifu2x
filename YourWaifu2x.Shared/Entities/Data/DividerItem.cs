namespace YourWaifu2x.Entities.Data {
    using System.Collections.Generic;
    using System.Linq;

    public class DividerItem {
        public DividerItem(int i) => SubItems = Enumerable.Range(1, 2).Select(x => $"group {i} item {x}");

        public IEnumerable<string> SubItems { get; }
    }

    public class DividerItems : List<DividerItem> {
        public DividerItems() : base(GetItems()) {
        }

        private static IEnumerable<DividerItem> GetItems() {
            yield return new DividerItem(1);
            yield return new DividerItem(2);
            yield return new DividerItem(3);
        }
    }
}
