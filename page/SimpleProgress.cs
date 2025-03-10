using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public class SimpleProgress<T> : IProgress<T>
    {
        private Action<T> action;

        public SimpleProgress(Action<T> action)
        {
            this.action = action;
        }
        public void Report(T value)
        {
            action(value);
        }
    }
}
