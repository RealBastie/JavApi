using System;

namespace biz.ritter.javapi.util.concurrent.lockj
{
    public class ReentrantLock
    {

        private Object simpleLockHelper;

        public void lockJ()
        {
            System.Threading.Monitor.Enter(this);
        }

        public void unlock () {
            System.Threading.Monitor.Exit(this);        
        }
    }
}
