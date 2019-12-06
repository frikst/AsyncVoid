using System;
using Microsoft.Extensions.Logging;

namespace KST.AsyncVoid.AspNetCore
{
	/// <summary>
	/// Asynchronous task starter for asp .net core projects or
	/// other projects based on .net core DI and logging.
	/// </summary>
	public class AspNetAsyncStarter : BackgroundAsyncStarter
	{
		private readonly ILogger<AspNetAsyncStarter> aLogger;

		/// <summary>
		/// Initializes async starter with the given logger
		/// </summary>
		/// <param name="logger">Logger to log exceptions in background async tasks</param>
		public AspNetAsyncStarter(ILogger<AspNetAsyncStarter> logger)
		{
			this.aLogger = logger;
		}

		/// <inheritdoc />
		protected override void LogError(Exception ex)
		{
			this.aLogger.LogError(0, ex, "Exception in a background task");
		}
	}
}
