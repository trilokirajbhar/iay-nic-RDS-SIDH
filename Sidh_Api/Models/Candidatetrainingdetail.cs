using System;
using System.Collections.Generic;

namespace Sidh_Api.Models;

public partial class Candidatetrainingdetail
{
    public long Id { get; set; }

    public string? Candidateid { get; set; }

    public long? Batchid { get; set; }

    public string? Batchtype { get; set; }

    public string? Batchstage { get; set; }

    public string? Assessmentdate { get; set; }

    public string? Certificatelink { get; set; }

    public string? Subschemename { get; set; }

    public string? Tpid { get; set; }

    public string? Tpname { get; set; }

    public string? Tcid { get; set; }

    public string? Tcname { get; set; }

    public virtual Candidate? Candidate { get; set; }
}
