namespace AusguckBackend
{
    public static class Globals
    {
        public static readonly DateOnly CampStart = new DateOnly(2026,07,19);
        public static readonly DateOnly CampEnd = new DateOnly(2026,08,15);
        public static readonly int MinAge = 5;
        public static readonly int MaxAge = 20;

        public static readonly List<string> ValideSlots = new List<string>
        {
            "D1",
            "D2",
            "Special"
        };

        public static readonly List<string> ValidePerms = new List<string>
        {
            "alone",
            "Small group",
            "supervised"
        };

        public static readonly string ConnectionString = Environment.GetEnvironmentVariable("DB_Ausguck_Inserter_ConnectionString") ?? string.Empty;
        public static readonly string TestConnectionString = Environment.GetEnvironmentVariable("DB_Ausguck_Tester_ConnectionString") ?? string.Empty;
        public static readonly int NotCheckedDiscountCodeId = 999;

    }
}
