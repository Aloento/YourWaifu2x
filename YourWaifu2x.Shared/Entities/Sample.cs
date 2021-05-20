namespace YourWaifu2x
{
    using System;
    using Uno.Extensions;
    using Uno.Logging;
    using Windows.UI.Xaml.Data;

    [Bindable]
    public class Sample
    {
        public Sample(SamplePageAttribute attribute, Type viewType)
        {
            this.Category = attribute.Category;
            this.Title = attribute.Title;
            this.Description = attribute.Description;
            this.DocumentationLink = attribute.DocumentationLink;
            this.Data = this.CreateData(attribute.DataType);
            this.Source = attribute.Source;
            this.SortOrder = attribute.SortOrder;
            this.ViewType = viewType;
        }

        private object CreateData(Type dataType)
        {
            if (dataType == null)
            {
                return null;
            }

            try
            {
                return Activator.CreateInstance(dataType);
            }
            catch (Exception e)
            {
                this.Log().Error($"Failed to initialize data for `{this.ViewType.Name}`:", e);
                return null;
            }
        }

        public SampleCategory Category { get; set; }

        public string Title { get; }

        public string Description { get; }

        public string DocumentationLink { get; }

        public object Data { get; }

        public int? SortOrder { get; }

        public SourceSdk Source { get; }

        public Type ViewType { get; }
    }
}
