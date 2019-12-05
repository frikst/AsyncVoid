using KST.AsyncVoid.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace KST.AsyncVoid.AspNetCore
{
	public static class AsyncVoidExtensions
	{
		public static IServiceCollection AddAsyncVoid(this IServiceCollection services)
		{
			services.AddSingleton<IAsyncStarter, AspNetAsyncStarter>();

			return services;
		}
	}
}
