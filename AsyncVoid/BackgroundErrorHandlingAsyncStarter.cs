using System;

namespace KST.AsyncVoid
{
	public class BackgroundErrorHandlingAsyncStarter : BackgroundAsyncStarter
	{
		private readonly Action<Exception> aLoggerCallback;

		public BackgroundErrorHandlingAsyncStarter(Action<Exception> loggerCallback)
		{
			this.aLoggerCallback = loggerCallback;
		}

		protected override void LogError(Exception ex)
		{
			this.aLoggerCallback(ex);
		}
	}
}
