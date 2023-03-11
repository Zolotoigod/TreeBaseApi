namespace TreeBase.Services
{
    public class IdGenerator
    {
        private readonly Random random = new Random(DateTime.UtcNow.Millisecond);

        public int GenerateInt64Id() => (int)random.NextInt64(0, int.MaxValue);
    }
}
