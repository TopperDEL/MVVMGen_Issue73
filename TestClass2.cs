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
    public class TestClass2 : IEventSubscriber<CloseViewMessage>
    {
        ServiceProvider _sp;
        MyViewModel _vm;

        public TestClass2()
        {
            var coll = new ServiceCollection();
            coll.AddSingleton<IEventAggregator, EventAggregator>();
            coll.AddTransient<MyViewModel>();

            _sp = coll.BuildServiceProvider();

            var ea = _sp.GetService<IEventAggregator>();
            ea.RegisterSubscriber(this);
        }

        public void Test()
        {
            UseViewModel();

            PublishEvent();
        }

        private void UseViewModel()
        {
            _vm = _sp.GetService<MyViewModel>();

            _vm.DoNothing();

            _vm.CloseView();
        }

        private void PublishEvent()
        {
            var ea = _sp.GetService<IEventAggregator>();

            ea.Publish(new MyMessage());
        }

        public void OnEvent(CloseViewMessage eventData)
        {
            var ea = _sp.GetService<IEventAggregator>();
            ea.UnregisterSubscriber(_vm);
        }
    }
}
