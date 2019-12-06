using System;

namespace KST.AsyncVoid
{
	/// <summary>
	/// Background task starter with the ability to start task with error handling callback
	/// </summary>
	public class BackgroundErrorHandlingAsyncStarter : BackgroundAsyncStarter
	{
		private readonly Action<Exception> aLoggerCallback;

		/// <summary>
		/// Creates a new background starter with a specified error handling callback
		/// </summary>
		/// <param name="loggerCallback">Error handling callback</param>
		public BackgroundErrorHandlingAsyncStarter(Action<Exception> loggerCallback)
		{
			this.aLoggerCallback = loggerCallback;
		}

		/// <inheritdoc />
		protected override void LogError(Exception ex)
		{
			this.aLoggerCallback(ex);
		}
	}
}
