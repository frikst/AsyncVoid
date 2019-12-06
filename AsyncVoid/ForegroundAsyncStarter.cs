using System;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid
{
	/// <summary>
	/// Starter which is starting tasks synchronously
	/// </summary>
	public class ForegroundAsyncStarter : IAsyncStarter
	{
		/// <inheritdoc />
		public Task Start(Func<Task> task)
		{
			return task();
		}

		/// <inheritdoc />
		public Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			return task(cancellationToken);
		}
	}
}
