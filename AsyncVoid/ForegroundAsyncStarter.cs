using System;
using System.Threading;
using System.Threading.Tasks;
using KST.AsyncVoid.Abstractions;

namespace KST.AsyncVoid
{
	public class ForegroundAsyncStarter : IAsyncStarter
	{
		public Task Start(Func<Task> task)
		{
			return task();
		}

		public Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken)
		{
			return task(cancellationToken);
		}
	}
}
