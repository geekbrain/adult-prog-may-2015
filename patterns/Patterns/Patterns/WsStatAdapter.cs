using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Patterns
{
	class WsStatAdapter
	{
		public List<NameStatistic> GetNameStatistic()
		{
			using(var client = new HttpClient())
			{
				var result = client.GetStringAsync( "http://ws.adult-prog-may-2015.esy.es/api/v1/stats" ).Result;
				Statistic stat = JsonConvert.DeserializeObject<Statistic>( result );
				return stat.result;
			}
		}
	}
}
