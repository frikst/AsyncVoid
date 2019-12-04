using System;
using KST.AsyncVoid;
using Microsoft.Extensions.Logging;

namespace AsyncVoid.AspNetCore
{
	public class AspNetAsyncStarter : BackgroundAsyncStarter
	{
		private readonly ILogger<AspNetAsyncStarter> aLogger;

		public AspNetAsyncStarter(ILogger<AspNetAsyncStarter> logger)
		{
			this.aLogger = logger;
		}

		#region Overrides of BackgroundAsyncStarter

		protected override void LogError(Exception ex)
		{
			this.aLogger.LogError(0, ex, "Exception in a background task");
		}

		#endregion
	}
}
