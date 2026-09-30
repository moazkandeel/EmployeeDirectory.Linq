using System.Linq.Expressions;

namespace EmployeeDirectory.Exercises
{
    public static class EnumerableExtensions
    {
        //  Applies a Where condition only when the supplied condition is true.
        // When condition is false, the original sequence is returned unchanged.
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            bool condition,
            Func<T, bool> predicate)
        {
            if (!condition)
                return source;

            return source.Where(predicate);
        }

        //  Returns each element together with the next element in the sequence.
        // yield return makes the method lazy, so pairs are generated only when requested.
        public static IEnumerable<(T Current, T Next)> Pairwise<T>(
            this IEnumerable<T> source)
        {
            using var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
                yield break;

            var current = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var next = enumerator.Current;

                yield return (current, next);

                current = next;
            }
        }

        //  Demonstrates the difference between Func and Expression<Func>.
        // Func represents executable C# code, while Expression<Func> represents
        // the same logic as an expression tree that LINQ providers such as EF Core
        // can inspect and translate to SQL.
        public static bool CompareFuncAndExpression(
            IEnumerable<int> numbers,
            Func<int, bool> funcPredicate,
            Expression<Func<int, bool>> expressionPredicate)
        {
            var funcResult = numbers
                .Where(funcPredicate)
                .ToList();

            var expressionResult = numbers
                .AsQueryable()
                .Where(expressionPredicate)
                .ToList();

            return funcResult.SequenceEqual(expressionResult);
        }
    }
}