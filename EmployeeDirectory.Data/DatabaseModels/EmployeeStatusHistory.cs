using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.DatabaseModels;

public partial class EmployeeStatusHistory
{
    public int EmployeeId { get; set; }

    public int Status { get; set; }

    public DateOnly ChangedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
