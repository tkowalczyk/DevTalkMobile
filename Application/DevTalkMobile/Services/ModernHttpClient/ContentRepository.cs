using System;
using System.Net.Http;
using System.Threading.Tasks;

using ModernHttpClient;

namespace DevTalkMobile.Services
{
	public class ContentRepository : IContentRepository
	{
		public ContentRepository ()
		{
		}


		#region IContentRepository implementation
		public async Task<string> GetContent (string url)
		{
			bool successResponse = false;
			string html = string.Empty;

			using (var httpClient = new HttpClient (new NativeMessageHandler ())) 
			{
				using (HttpResponseMessage response = await httpClient
				.GetAsync(url)
				.ConfigureAwait (false)) 
				{
					successResponse = response.IsSuccessStatusCode;

					response.EnsureSuccessStatusCode ();

					using (HttpContent content = response.Content) 
					{   
						string responseUri = response.RequestMessage
						.RequestUri.ToString ();

						html = await content
							.ReadAsStringAsync()
							.ConfigureAwait(false);
					}
				}
			}

			return html;
		}
		#endregion
	}
}