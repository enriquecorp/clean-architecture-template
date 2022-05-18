namespace Versioning.Shared.Tests.Domain.Simples
{
    public static class DictionaryMother<TKey, TValue> where TKey : notnull
    {
        public static Dictionary<TKey, TValue> Create(int size, Func<KeyValuePair<TKey, TValue>> creator)
        {
            var dict = new Dictionary<TKey, TValue>();

            for (var i = 0; i < size; i++)
            {
                var element = creator();
                dict.Add(element.Key, element.Value);
            }

            return dict;
        }

        public static Dictionary<TKey, TValue> Random(Func<KeyValuePair<TKey, TValue>> creator)
        {
            return Create(IntegerMother.Between(1, 10), creator);
        }

        public static Dictionary<TKey, TValue> One(KeyValuePair<TKey, TValue> element)
        {
            return new Dictionary<TKey, TValue>() { { element.Key, element.Value } };
        }

        public static Dictionary<TKey, TValue> Many(Dictionary<TKey, TValue> elements)
        {
            return new Dictionary<TKey, TValue>(elements);
        }
    }
}
