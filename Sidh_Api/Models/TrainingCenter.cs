using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class TrainingCenter
{
    public string TcId { get; set; } = null!;

    public string TcName { get; set; } = null!;

    public string? TcType { get; set; }

    public string? TpUsername { get; set; }

    public string? TcAddress { get; set; }

    public int? StateId { get; set; }

    public string? StateName { get; set; }

    public int? DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public string? Longitude { get; set; }

    public string? Latitude { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Email { get; set; }
}
