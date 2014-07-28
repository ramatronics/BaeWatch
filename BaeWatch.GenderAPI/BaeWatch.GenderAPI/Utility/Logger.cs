using System;

namespace BaeWatch.GenderAPI.Utility
{
    public enum LOG_STATE { Warn, Debug, Critical };

    public class Logger
    {
        private static volatile Logger _instance;
        private static object _locksync = new object();

        private Logger()
        {
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                    lock (_locksync)
                        if (_instance == null)
                            _instance = new Logger();

                return _instance;
            }
        }

        public void LogIt(string inMessage_, LOG_STATE inState_)
        {
            //TODO
        }

        public void LogIt(string inMessage_, LOG_STATE inState_, Exception inException_)
        {
            //TODO
        }
    }
}