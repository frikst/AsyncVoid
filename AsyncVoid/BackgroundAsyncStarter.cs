using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid
{
	public abstract class BackgroundAsyncStarter : IAsyncStarter
	{
		protected abstract void LogError(Exception ex);

		public Task Start(Func<Task> task)
		{
			this.StartInBackground(task);
#if NETSTANDARD1_0
			return Task.FromResult(false);
#else
			return Task.CompletedTask;
#endif
		}

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
