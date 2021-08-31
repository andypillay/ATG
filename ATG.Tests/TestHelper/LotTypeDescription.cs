using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Tests.TestHelper
{
    // I'm guessing that Description is for describing the actual lot
    public static class LotTypeDescription
    {
        public const string Archived = "I'm a lot that has been archived";
        public const string Failover = "I'm a lot that has been failed over";
        public const string Datastore= "I'm a lot fresh from the datastore";
    }
}
