using System;
using System.Threading;
using System.Threading.Tasks;

namespace KST.AsyncVoid.Abstractions
{
	/// <summary>
	/// Starter for asynchronous tasks. Use instead of async void.
	/// </summary>
	public interface IAsyncStarter
	{
		/// <summary>
		/// Starts the task asynchronously.
		/// </summary>
		/// <param name="task">Task to start</param>
		Task Start(Func<Task> task);

		/// <summary>
		/// Starts the cancellable task asynchronously.
		/// </summary>
		/// <param name="task">Task to start</param>
		/// <param name="cancellationToken">Associated cancellation token</param>
		Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken);
	}
}
