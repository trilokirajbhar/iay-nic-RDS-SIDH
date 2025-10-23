namespace Sidh_Api.DTO
{
    public class CandidateResponseDto
    {
        public List<CandidateDataDto> data { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public PaginationDto Pagination { get; set; }
    }

    public class CandidateDataDto
    {
        public CandidatePersonalDetailsDto candidatePersonalDetails { get; set; }
        public CandidateAddressDto candidateAddress { get; set; }
        public List<CandidateTrainingDetailsDto> candidateTrainingDetails { get; set; }
    }


    public class CandidatePersonalDetailsDto
    {
        public string? candidateId { get; set; }
        public string? candidateName { get; set; }
        public string? dob { get; set; }
        public string? gender { get; set; }
        public string? religion { get; set; }
        public string? mobile { get; set; }
        public string? emailId { get; set; }
        public string? isPWD { get; set; }
        public string?    pwdDocumentUrl { get; set; }
        public string? isMinority { get; set; }
        public string? minorityDocumentUrl { get; set; }
        public string? isEWS { get; set; }
        public string? ewsDocumentUrl { get; set; }
        public string? aadharReference { get; set; }
    }


    public class CandidateAddressDto
    {
        public string? addressLine1 { get; set; }
        public string? state { get; set; }
        public int stateID { get; set; }
        public string? district { get; set; }
        public int districtId { get; set; }
    }


    public class CandidateTrainingDetailsDto
    {
        public int batchId { get; set; }
        public string? batchtype { get; set; }
        public string? batchStage { get; set; }
        public string?    AssessmentDate { get; set; }
        public string? CertificateLink { get; set; }
        public string? subSchemeName { get; set; }
        public string? tpId { get; set; }
        public string? tpName { get; set; }
        public string? tcId { get; set; }
        public string? tcName { get; set; }
    }

    public class PaginationDto
    {
        public int Count { get; set; }
        public int Limit { get; set; }
        public int PageNo { get; set; }
    }

}

