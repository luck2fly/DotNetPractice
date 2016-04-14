using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Security;

namespace ServiceLibrary
{
    [ServiceContract(Namespace = "http://www.artech.com/", CallbackContract = typeof(IWcfServerCallback))]
    public interface IWcfServer
    {
        [OperationContract(IsOneWay = false)]
        object Get(string key);
        [OperationContract(IsOneWay = true)]
        void Save(string key, object value);

    }

    public interface IWcfServerCallback
    {
        [OperationContract(IsOneWay = true)]
        void Save(string key, object value);

    }

    public class WcfServerCallback : IWcfServerCallback
    {
        public void Save(string key, object value)
        {
            throw new NotImplementedException();
        }
    }

    public class WcfServer : IWcfServer
    {
        public object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        public void Save(string key, object value)
        {
            MemoryCache.Default.Set(key, value, DateTime.UtcNow.AddMinutes(5));
        }

        public static void StartServer()
        {
            ServiceHost host = new ServiceHost(typeof(WcfServer));
            host.Open();

        }

        public static IWcfServer Server { get; private set; }

        public static void ContectServer()
        {
            if (Server == null)
            {
                InstanceContext instanceContext = new InstanceContext(new WcfServerCallback());
                DuplexChannelFactory<IWcfServer> channelFactory = new DuplexChannelFactory<IWcfServer>(instanceContext, "ServiceLibrary.WcfServer");
                Server = channelFactory.CreateChannel();
            }
        }
    }
}
