namespace ArcheryTraining.Enums
{
    public class Performance
    {
        private Performance(string value) { Value = value; }

        public string Value { get; private set; }

        public static Performance VERY_LOWER { get { return new Performance("VERY_LOWER"); } }
        public static Performance LOWER { get { return new Performance("LOWER"); } }
        public static Performance EQUAL { get { return new Performance("EQUAL"); } }
        public static Performance UPPER { get { return new Performance("UPPER"); } }
        public static Performance VERY_UPPER { get { return new Performance("VERY_UPPER"); } }
    }
}