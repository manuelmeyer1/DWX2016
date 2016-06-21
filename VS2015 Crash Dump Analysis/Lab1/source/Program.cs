using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpLab
{
    class Program
    {
        static void Main(string[] args)
        {
            DWX2016.KeynoteManagingForHappiness();
        }
    }

    public static class DWX2016
    {
        public static void KeynoteManagingForHappiness()
        {
            Sessions();
        }

        public static void Sessions()
        {
            DevSessions();
        }

        public static void DevSessions()
        {
            WPFTroubleshootingInVS2015.DebuggingUndProfiling();
        }

        public static void KeynoteScottHanselman()
        {
            Workshops();
        }

        public static void Workshops()
        {
            Bar();
        }


        public static void Bar()
        {
            throw new StackOverflowException("Einfach zu viel gesoffen!. Unrechable code detected: AbNachHause()");

            AbNachHause();
        }

        public static void AbNachHause()
        {

        }




        public class WPFTroubleshootingInVS2015
        {
            public static void DebuggingUndProfiling()
            {
                VS2015Tools();
            }

            public static void VS2015Tools()
            {
                WPFTools();
            }

            public static void WPFTools()
            {
                ProfilingUndTimeline();
            }

            public static void ProfilingUndTimeline()
            {
                TakeAways();
            }

            public static void TakeAways()
            {
                DWX2016.KeynoteScottHanselman();
            }


        }
    }
}
