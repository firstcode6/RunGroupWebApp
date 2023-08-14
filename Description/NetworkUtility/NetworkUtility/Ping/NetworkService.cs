using NetworkUtility.DNS;


namespace NetworkUtility.Ping //Свист звон
{
    public class AggregateGroupVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class NetworkService
    {
        private readonly IDNS _dNS;

        public NetworkService(IDNS dNS)
        {
            _dNS = dNS;
        }
        public string SendPing()
        {
            var dnsSeccess = _dNS.SendDNS(); //fake SendDNS
            if (dnsSeccess)
            {
                return "Success: Ping Sent!";
            }
            else
            {
                return "Failed: Ping not sent";
            }
        }

        public int PingTimeout(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public AggregateGroupVM GetPingOptions()
        {
            return new AggregateGroupVM()
            {
                Id = 1,
                Name = "First"
            };
        }

        public IEnumerable<AggregateGroupVM> MostRecentPings()
        {
            IEnumerable<AggregateGroupVM> aggregateGroups = new[]
            {
                new AggregateGroupVM()
                {
                    Id = 1,
                    Name = "First"
                },
                new AggregateGroupVM()
                {
                    Id = 2,
                    Name = "Second"
                },
                new AggregateGroupVM()
                {
                    Id = 3,
                    Name = "Third"
                },

            };
            return aggregateGroups;
        }


    }
}

