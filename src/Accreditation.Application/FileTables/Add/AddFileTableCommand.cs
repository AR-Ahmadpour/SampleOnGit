using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Add
{

    public sealed record AddFileTableCommand(
    //Guid Stream_Id,
    string Name,
    byte[] File_Stream,
    DateTime? Creation_Time,
    DateTime? Last_Write_Time
    ):ICommand<string>;
}
