using System;
using System.Threading;
using System.Threading.Tasks;

namespace KST.AsyncVoid.Abstractions
{
	public interface IAsyncStarter
	{
		Task Start(Func<Task> task);
		Task Start(Func<CancellationToken, Task> task, CancellationToken cancellationToken);
	}
}
