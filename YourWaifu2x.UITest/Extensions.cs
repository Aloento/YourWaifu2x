namespace YourWaifu2x.UITests
{
    public static class Extensions
    {
        public static Func<IAppQuery, IAppQuery> WaitThenTap(this IApp app, Func<IAppQuery, IAppQuery> query, TimeSpan? timeout = null)
        {
            app.WaitForElement(query, timeout: timeout);
            app.Tap(query);

            return query;
        }

        public static Func<IAppQuery, IAppQuery> WaitThenTap(this IApp app, string marked, TimeSpan? timeout = null)
        {
            return WaitThenTap(app, q => q.All().Marked(marked), timeout);
        }

        public static QueryEx ToQueryEx(this Func<IAppQuery, IAppQuery> query)
        {
            return new QueryEx(query);
        }
    }
}
