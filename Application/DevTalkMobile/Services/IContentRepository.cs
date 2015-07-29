using System;
using System.Threading.Tasks;

namespace DevTalkMobile.Services
{
	public interface IContentRepository
	{
		Task<string> GetContent(string url);
	}
}