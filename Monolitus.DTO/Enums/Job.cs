using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.Enums
{
    public enum JobCommands
    {
        None,

        SendMail
    }

    public enum JobStates
    {
        New,
        Processing,
        TryAgain,
        Done,
        Failed
    }

    public enum JobExecuters
    {
        Member,
        Technician,
        Machine
    }
}
