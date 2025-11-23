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

        public static readonly string ConnectionString = "Host=192.168.178.143;Database=Rumpf;Username=AusguckInserter;Password=Geheim";
        public static readonly string TestConnectionString = "Host=192.168.178.143;Database=Rumpf;Username=AusguckTester;Password=dsafadsfef25ojnkoajn9oiujbasounsaejlkwsxx";
        public static readonly int NotCheckedDiscountCodeId = 999;

    }
}
