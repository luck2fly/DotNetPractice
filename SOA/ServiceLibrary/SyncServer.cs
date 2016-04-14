using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;

namespace ServiceLibrary
{
    public interface ISyncServer
    {
        object Get(string key);
        void Save(string key, object value);

    }

    public class SyncServer : MarshalByRefObject, ISyncServer
    {
        public object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        public void Save(string key, object value)
        {
            MemoryCache.Default.Set(key, value, DateTime.UtcNow.AddMinutes(5));
        }

        public static void StartServer(int port)
        {
            var channnel = new TcpChannel(port);
            ////该信道使用ChannelServices 注册，使之可用于远程对象，并禁用安全性；
            ChannelServices.RegisterChannel(channnel, false);
            // 远程对象类用RegisterWellKnownServiceType注册；
            //WellKnownObjectMode.SingleCall :
            //每一个方法调用都会产生一个新的对象实例，此模式不保存远程对象中的状态
            //WellKnownObjectMode.Singleton:
            //创建一个实例，供每次方法的调用，单一实例。
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SyncServer), "SyncServer", WellKnownObjectMode.SingleCall);

        }

        public static ISyncServer Server { get; private set; }

        public static void ContectServer(string address)
        {
            if (Server == null)
            {
                var channnel = new TcpChannel();
                ////该信道使用ChannelServices 注册，使之可用于远程对象，并禁用安全性；
                ChannelServices.RegisterChannel(channnel, false);

                Server = (ISyncServer)Activator.GetObject(typeof(ISyncServer), address);
            }
        }
    }
}
