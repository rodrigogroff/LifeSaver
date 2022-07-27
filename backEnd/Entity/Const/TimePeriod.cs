
namespace Master.Infra.Constant
{
    public static class TimePeriod
    {
        public const int daily = 1;
        public const int weekly = 2;
        public const int monthly = 3;
        public const int yearly = 4;
        public const int random = 5;

        public static bool Check(int opt)
        {
            if (opt < 1 && opt > 5)
                return false;

            return true;
        }

        public static string translate(long? opt)
        {
            switch ((int)opt)
            {
                case 1: return "daily"; break;
                case 2: return "weekly"; break;
                case 3: return "monthly"; break;
                case 4: return "yearly"; break;
                case 5: return "random"; break;
            }

            return null;
        }
    }
}
