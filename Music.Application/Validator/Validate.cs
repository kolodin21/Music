namespace Music.Application.Validator
{
    public static class Validate
    {
        public static void ThrowIfNull<T>(T? obj, string message)
        {
            if (obj == null)
                throw new NullReferenceException(message);
        }
        public static void ThrowIfZero(int value, string message)
        {
            if (value == 0)
                throw new ArgumentException(message);
        }
        // Универсальный метод с любой логикой проверки
        public static void ThrowIf<T>(T obj, Func<T, bool> predicate, string message)
        {
            if (predicate(obj))
                throw new InvalidOperationException(message);
        }
    }
}
