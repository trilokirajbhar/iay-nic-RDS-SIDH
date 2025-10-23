using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class TrainingScheme
{
    public int Id { get; set; }

    public string SchemeId { get; set; } = null!;

    public string SchemeName { get; set; } = null!;

    public string BasicschemeId { get; set; } = null!;

    public string BasicschemeName { get; set; } = null!;

    public string? TrainingType { get; set; }

    public string? SubSchemeName { get; set; }

    public string? SchemeReferenceType { get; set; }

    public string? SchemeWorkflowName { get; set; }

    public string? SchemeWorkflowDiscrip { get; set; }

    public int? SchemeWorkflowId { get; set; }

    public string? RulesEffectiveFrom { get; set; }

    public string? RulesEffectiveTo { get; set; }

    public string? InitiativeOf { get; set; }

    public string? ProgramBy { get; set; }

    public string? SidSchemeDisplayName { get; set; }

    public string? Schemeworkflownames { get; set; }

    public bool? IsQpLinkedScheme { get; set; }

    public string? SidSchemeName { get; set; }

    //public DateTime? CreatedAt { get; set; }

    //public DateTime? UpdatedAt { get; set; }
}
