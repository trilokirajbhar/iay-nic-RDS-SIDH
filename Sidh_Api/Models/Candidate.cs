using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class Candidate
{
    public long Id { get; set; }

    public string Candidateid { get; set; } = null!;

    public string? Candidatename { get; set; }

    public string? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Religion { get; set; }

    public string? Mobile { get; set; }

    public string? Emailid { get; set; }

    public string? Ispwd { get; set; }

    public string? Pwddocumenturl { get; set; }

    public string? Isminority { get; set; }

    public string? Minoritydocumenturl { get; set; }

    public string? Isews { get; set; }

    public string? Ewsdocumenturl { get; set; }

    public string? Aadharreference { get; set; }

    public string? Addressline1 { get; set; }

    public string? State { get; set; }

    public int? Stateid { get; set; }

    public string? District { get; set; }

    public int? Districtid { get; set; }

    public virtual ICollection<Candidatetrainingdetail> Candidatetrainingdetails { get; set; } = new List<Candidatetrainingdetail>();
}
