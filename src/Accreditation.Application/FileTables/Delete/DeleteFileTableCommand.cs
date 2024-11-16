using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.FileTables.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Delete
{
   public sealed record DeleteFileTableCommand(string FileName) :ICommand;

}
