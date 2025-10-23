using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class BatchJobRole
{
    public long Id { get; set; }

    public string? BatchId { get; set; }

    public string? JobName { get; set; }

    public string? QpCode { get; set; }

    public string? Version { get; set; }

    public string? NsqfLevel { get; set; }

    public string? JobRoleDesc { get; set; }

    public string? SectorId { get; set; }

    public string? SectorName { get; set; }

    public virtual Batch? Batch { get; set; }
}
