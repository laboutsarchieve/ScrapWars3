using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;

namespace ScrapWars3
{
    static class ScrapWarsEventManager
    {
        private static BasicEventManager currentManager;
        public static void SetManager(BasicEventManager manager)
        {
            currentManager = manager;
        }
        public static BasicEventManager GetManager()
        {
            return currentManager;
        }
    }
}
