using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;

public sealed record GetListAccreditationalInstanceAnswerMehvarQuery( Guid AccreditationalInstaneGuid) :
                                    IQuery<AccreditationalInstanceAnswersGetListAnswerMehvarDto>;