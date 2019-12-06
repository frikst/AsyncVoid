using KST.AsyncVoid.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace KST.AsyncVoid.AspNetCore
{
	/// <summary>
	/// Extensions for registering async void classes to a DI container
	/// </summary>
	public static class AsyncVoidExtensions
	{
		/// <summary>
		/// Registers async void to a DI container
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
		/// <returns></returns>
		public static IServiceCollection AddAsyncVoid(this IServiceCollection services)
		{
			services.AddSingleton<IAsyncStarter, AspNetAsyncStarter>();

			return services;
		}
	}
}
