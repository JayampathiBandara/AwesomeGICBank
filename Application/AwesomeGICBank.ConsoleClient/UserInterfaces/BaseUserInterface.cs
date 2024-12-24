using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public abstract class BaseUserInterface : IUserInterface
{
    protected readonly IServiceProvider _serviceProvider;
    protected string Instruction { get; init; }

    public BaseUserInterface(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> DisplayAsync()
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        Console.Write(Instruction);

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

        return await ProcessInput(input, mediator);
    }

    protected abstract Task<bool> ProcessInput(string input, IMediator mediator);
}
