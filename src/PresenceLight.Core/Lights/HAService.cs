using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HADotNet.Core;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices.ComTypes;
using HADotNet.Core.Clients;

namespace PresenceLight.Core.Lights
{

    public interface IHAService
    {
        Task SetScene(string availability);


    }
    public class HAService : IHAService
    {
        private readonly ConfigWrapper _options;
        private ServiceClient serviceClient;



        public HAService(IOptionsMonitor<ConfigWrapper> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
        }

        public HAService(ConfigWrapper options)
        {
            _options = options;
            if (_options.IsHAEnabled & _options.HAURL != string.Empty)
            {
                ClientFactory.Initialize(_options.HAURL, _options.HAToken);
                serviceClient = ClientFactory.GetClient<ServiceClient>();
            }
        }
        public async Task SetScene(string availability)
        {

            var entityid = string.Empty;
            switch (availability)
            {
                case "Available":
                    entityid = "working_free";
                    break;
                case "Busy":
                    entityid = "working_busy";
                    break;
                case "BeRightBack":
                    entityid = "working_away";
                    break;
                case "Away":
                    entityid = "working_away";
                    break;
                case "DoNotDisturb":
                    entityid = "working_video";
                    break;
                case "Offline":
                    entityid = "working_free";
                    break;
                case "Off":
                    entityid = "working_free"; ;
                    break;
                default:
                    entityid = "working_free";
                    break;
            }

            await this.serviceClient.CallService("scene.turn_on", new { entity_id = entityid });

        }
    }
    
}
