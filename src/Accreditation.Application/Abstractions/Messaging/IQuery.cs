using MediatR;
using SharedKernel;

namespace Accreditation.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
