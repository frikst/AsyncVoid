using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid
{
	/// <summary>
	/// Base class for background task starters
	/// </summary>
	public abstract class BackgroundAsyncStarter : IAsyncStarter
	{
		/// <summary>
		/// Log exception from background task
		/// </summary>
		/// <param name="ex">A catched exception</param>
		protected abstract void LogError(Exception ex);

		/// <inheritdoc />
		public Task Start(Func<Task> task)
		{
			this.StartInBackground(task);
#if NETSTANDARD1_0
			return Task.FromResult(false);
#else
			return Task.CompletedTask;
#endif
		}

		/// <inheritdoc />
		public Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			this.StartInBackground(task, cancellationToken);
#if NETSTANDARD1_0
			return Task.FromResult(false);
#else
			return Task.CompletedTask;
#endif
		}

		private async void StartInBackground(Func<Task> task)
		{
			try
			{
				await task();
			}
			catch (Exception e)
			{
				this.LogError(e);

				if (Debugger.IsAttached)
					Debugger.Break();
			}
		}

		private async void StartInBackground(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			try
			{
				await task(cancellationToken);
			}
			catch (Exception e)
			{
				this.LogError(e);

				if (Debugger.IsAttached)
					Debugger.Break();
			}
		}
	}
}
