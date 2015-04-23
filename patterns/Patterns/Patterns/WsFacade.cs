using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
	class WsFacade
	{
		WsStatAdapter _statAdapter = new WsStatAdapter();
		WsCrawlerAdapter _crawlerAdapter = new WsCrawlerAdapter();

		public List<String> GetNames()
		{
			return _crawlerAdapter.GetNames();
		}

		public List<NameStatistic> GetNameStatistic()
		{
			return _statAdapter.GetNameStatistic();
		}
	}
}
