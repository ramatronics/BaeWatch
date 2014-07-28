namespace BaeWatch.GenderAPI.ConsoleHost
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GenderAPIEngine tmp = new GenderAPIEngine();

            tmp.TwitterGender("ramatronics");
        }
    }
}