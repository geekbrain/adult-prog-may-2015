using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
	class WsCrawlerAdapter
	{
		private WsClient.ServiceClient client = new WsClient.ServiceClient();
		
		public List<String> GetNames()
		{
			var names = client.GetNames();
			List<String> result = new List<string>();
			foreach ( var id in names.Keys )
			{
				result.Add( names[ id ] );
			}
			return result;
		}
	}
}
