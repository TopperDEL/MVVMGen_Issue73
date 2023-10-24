using Microsoft.Extensions.DependencyInjection;
using MvvmGen;
using MvvmGen.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMGen_Issue73
{
    public class TestClass1
    {
        ServiceProvider _sp;
        public TestClass1()
        {
            var coll = new ServiceCollection();
            coll.AddSingleton<IEventAggregator, EventAggregator>();
            coll.AddTransient<MyViewModel>();

            _sp = coll.BuildServiceProvider();
        }

        public void Test()
        {
            UseViewModel();

            //The ViewModel is not referenced anymore, but the event is still published to it
            PublishEvent();
        }

        private void UseViewModel()
        {
            var vm = _sp.GetService<MyViewModel>();

            vm.DoNothing();
        }

        private void PublishEvent()
        {
            var ea = _sp.GetService<IEventAggregator>();

            ea.Publish(new MyMessage());
        }
    }
}
