using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid.Testing
{
	/// <summary>
	/// Task starter targeted especially for the use in unit tests. All tasks are awaited
	/// in DisposeAsync to process background exceptions
	/// </summary>
	public class LocalAsyncStarter : IAsyncStarter, IAsyncDisposable
	{
		private readonly List<(Task Task, CancellationTokenSource Cts, CancellationTokenRegistration Registration)> aTasks;
		private bool aDisposed;

		/// <summary>
		/// Creates a task starter
		/// </summary>
		public LocalAsyncStarter()
		{
			this.aTasks = new List<(Task, CancellationTokenSource, CancellationTokenRegistration)>();
			this.aDisposed = false;
		}

		/// <inheritdoc />
		public Task Start(Func<Task> task)
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			this.aTasks.Add((task(), null, default));
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			var cts = new CancellationTokenSource();
			var registration = cancellationToken.Register(cts.Cancel);
			this.aTasks.Add((task(cts.Token), cts, registration));
			return Task.CompletedTask;
		}

		/// <summary>
		/// Cancels all started cancellable tasks
		/// </summary>
		/// <exception cref="InvalidOperationException"></exception>
		public void CancelAll()
		{
			if (this.aDisposed)
				throw new InvalidOperationException("Async starter already disposed");

			foreach (var task in this.aTasks)
				task.Cts?.Cancel();
		}

		/// <inheritdoc />
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
	}
}
