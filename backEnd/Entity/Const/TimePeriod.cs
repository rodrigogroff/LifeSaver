
namespace Master.Infra
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

    }
}
