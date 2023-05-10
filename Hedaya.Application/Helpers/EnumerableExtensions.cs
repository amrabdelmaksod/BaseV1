namespace Hedaya.Application.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> CustomDistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(g => g.First());
        }



        public static IEnumerable<TSource> DistinctBy<TSource, TKey1, TKey2, TKey3, TKey4>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey1> keySelector1,
            Func<TSource, TKey2> keySelector2,
            Func<TSource, TKey3> keySelector3,
            Func<TSource, TKey4> keySelector4)
        {
            return source
                .GroupBy(x => new { Key1 = keySelector1(x), Key2 = keySelector2(x), Key3 = keySelector3(x), Key4 = keySelector4(x) })
                .Select(x => x.First());
        }
    }
}
