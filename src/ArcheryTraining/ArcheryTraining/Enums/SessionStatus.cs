namespace ArcheryTraining.Enums
{
    public class SessionStatus
    {
        private SessionStatus(string value) { Value = value; }

        public string Value { get; private set; }

        public static SessionStatus IN_PROGRESS { get { return new SessionStatus("IN_PROGRESS"); } }
        public static SessionStatus SUSPENDED { get { return new SessionStatus("SUSPENDED"); } }
        public static SessionStatus FINISH { get { return new SessionStatus("FINISH"); } }
    }
}