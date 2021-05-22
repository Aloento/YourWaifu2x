namespace YourWaifu2x {
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PageAttribute : Attribute {
        public PageAttribute(PageCategory category, string title, SourceSdk source = SourceSdk.WinUI) {
            Category = category;
            Title = title;
            Source = source;
        }

        /// <summary>
        /// MyPage category with null reserved for Home/Overview.
        /// </summary>
        public PageCategory Category { get; }

        public string Title { get; }

        public string Description { get; set; }

        public string DocumentationLink { get; set; }

        public Type DataType { get; set; }

        public SourceSdk Source { get; }

        /// <summary>
        /// Sort order with the same <see cref="Category"/>.
        /// </summary>
        public int SortOrder { get; set; } = int.MaxValue;
    }
}
