using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid
{
	public class LocalAsyncStarter : IAsyncStarter, IAsyncDisposable
	{
		private readonly List<(Task Task, CancellationTokenSource Cts, CancellationTokenRegistration Registration)> aTasks;
		private bool aDisposed;

		public LocalAsyncStarter()
		{
			this.aTasks = new List<(Task, CancellationTokenSource, CancellationTokenRegistration)>();
			this.aDisposed = false;
		}

		#region Implementation of ITaskStarter

		public Task Start(Func<Task> task)
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			this.aTasks.Add((task(), null, default));
			return Task.CompletedTask;
		}

		public Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			var cts = new CancellationTokenSource();
			var registration = cancellationToken.Register(cts.Cancel);
			this.aTasks.Add((task(cts.Token), cts, registration));
			return Task.CompletedTask;
		}

		#endregion

		public void CancelAll()
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			foreach (var task in this.aTasks)
				task.Cts?.Cancel();
		}

		#region Implementation of IAsyncDisposable

		public async ValueTask DisposeAsync()
		{
			for (;;)
			{
				var tasks = this.aTasks.Select(x => x.Task).ToArray();
				await Task.WhenAll(tasks);
				if (tasks.Length == this.aTasks.Count)
					break;
			}

			this.aDisposed = true;

			foreach (var task in this.aTasks)
			{
				task.Cts?.Dispose();
				task.Registration.Dispose();
			}
		}

		#endregion
	}
}
