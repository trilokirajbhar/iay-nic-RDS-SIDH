using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class Batch
{
    public long Id { get; set; }

    public string BatchId { get; set; } = null!;

    public string? BatchName { get; set; }

    public DateTime? BatchStartDate { get; set; }

    public DateTime? BatchEndDate { get; set; }

    public int? BatchSize { get; set; }

    public string? BatchStage { get; set; }

    public string? TcId { get; set; }

    public string? TcName { get; set; }

    public string? TcLongitude { get; set; }

    public string? TcLatitude { get; set; }

    public string? TcSpocName { get; set; }

    public string? TcSpocMobile { get; set; }

    public string? TcSpocEmail { get; set; }

    public string? TcAddressLine { get; set; }

    public string? Pincode { get; set; }

    public string? TpId { get; set; }

    public string? TpName { get; set; }

    public string? SchemeName { get; set; }

    public string? SchemeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BatchJobRole> BatchJobRoles { get; set; } = new List<BatchJobRole>();
}
