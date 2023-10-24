using MvvmGen;
using MvvmGen.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMGen_Issue73
{
    [ViewModel]
    [Inject(typeof(IEventAggregator))]
    public partial class MyViewModel : IEventSubscriber<MyMessage>
    {
        
        public void DoNothing()
        {
            Console.WriteLine("Hello, World from ViewModel!");
        }

        public void OnEvent(MyMessage eventData)
        {
            //Should not be invoked if the viewmodel is not in use anymore
        }

        public void CloseView()
        {
            EventAggregator.Publish(new CloseViewMessage());
        }
    }
}
