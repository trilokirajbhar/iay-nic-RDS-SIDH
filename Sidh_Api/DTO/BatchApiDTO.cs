namespace Sidh_Api.DTO
{
    public class BatchApiDTO
    {
        public List<BatchData>? data { get; set; }
        public int statusCode { get; set; }
        public string? status { get; set; }
        public Pagination? pagination { get; set; }
    }

    public class BatchData
    {
        public long batchId { get; set; }
        public string? batchName { get; set; }
        public DateTime? batchStartDate { get; set; }
        public DateTime? batchEndDate { get; set; }
        public int? batchSize { get; set; }
        public string? batchStage { get; set; }
        public List<JobRole>? jobRoles { get; set; }
        public string? tcId { get; set; }
        public string? tcName { get; set; }
        public string? tcLongitude { get; set; }
        public string? tcLatitude { get; set; }
        public string? tcSpocName { get; set; }
        public string? tcSpocMobile { get; set; }
        public string? tcSpocEmail { get; set; }
        public string? tcAddressLine { get; set; }
        public string? pincode { get; set; }
        public string? tpId { get; set; }
        public string? tpName { get; set; }
        public string? schemeName { get; set; }
        public string? schemeID { get; set; }
    }

    public class JobRole
    {
        public string? jobName { get; set; }
        public string? qpCode { get; set; }
        public string? version { get; set; }
        public string? nsqfLevel { get; set; }
        public string? jobRoleDesc { get; set; }
        public string? sectorId { get; set; }
        public string? sectorName { get; set; }
    }

    public class Pagination
    {
        public int count { get; set; }
        public int limit { get; set; }
        public int pageNo { get; set; }
    }
}
