using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
	static class WsCrawlerSingletone
	{
		static WsCrawlerAdapter _instance;

		public static WsCrawlerAdapter GetInstance()
		{
			if(_instance == null)
			{
				_instance = new WsCrawlerAdapter();
			}
			return _instance;
		}
	}
}
