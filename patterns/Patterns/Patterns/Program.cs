using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
	class Program
	{
		static void Main( string[] args )
		{
			WsStatAdapter statAdapter = new WsStatAdapter();
			var stat = statAdapter.GetNameStatistic();

			WsCrawlerAdapter crawlerAdapter = new WsCrawlerAdapter();
			
			var names = crawlerAdapter.GetNames();
			
			var names2 = WsCrawlerSingletone.GetInstance().GetNames();

			WsFacade facade = new WsFacade();
			var names3 = facade.GetNames();
			var stat3 = facade.GetNameStatistic();
		}
	}
}
