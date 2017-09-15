using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using Sino.Extensions.Redis;

namespace Sino.NLog.Redis
{
    [Target("Redis")]
    public sealed class RedisTarget : TargetWithLayout
    {
        private static PoolRedisClient _pool;

        [RequiredParameter]
        public string Host { get; set; }

        [RequiredParameter]
        public int Port { get; set; }

        public string Password { get; set; }

        [RequiredParameter]
        public string RedisKey { get; set; }

        public RedisTarget() { }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            if(_pool != null)
                return;

            if(string.IsNullOrEmpty(Password))
            {
                _pool = new PoolRedisClient(Host, Port);
            }
            else
            {
                _pool = new PoolRedisClient(Host, Port, Password);
            }
        }

        protected override void CloseTarget()
        {
            base.CloseTarget();

            _pool.Dispose();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            _pool.LPush(RedisKey, Layout.Render(logEvent));
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            _pool.LPush(RedisKey, Layout.Render(logEvent.LogEvent));
        }
    }
}
