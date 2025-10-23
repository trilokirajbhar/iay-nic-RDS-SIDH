using Microsoft.EntityFrameworkCore;
using Sidh_Api.Controllers;
using Sidh_Api.Database;
using Sidh_Api.Models;
using System;
using System.Collections.Generic;



namespace Sidh_Api.Database;

public partial class MoprContext : DbContext
{
    public readonly PostgreSqlTcp _postgreSqlTcp;
    public MoprContext()
    {
    }

    public MoprContext(DbContextOptions<MoprContext> options, PostgreSqlTcp postgreSqlTcp)
        : base(options)
    {
        _postgreSqlTcp = postgreSqlTcp;
    }

    public virtual DbSet<TrainingCenter> TrainingCenters { get; set; }

    public virtual DbSet<TrainingScheme> TrainingSchemes { get; set; }


    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<BatchJobRole> BatchJobRoles { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Candidatetrainingdetail> Candidatetrainingdetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseNpgsql(_postgreSqlTcp.NewPostgreSqlTCPConnectionString().ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingCenter>(entity =>
        {
            entity.HasKey(e => e.TcId).HasName("training_centers_pkey");

            entity.ToTable("training_centers", "skill_india");

            entity.Property(e => e.TcId)
                .HasMaxLength(50)
                .HasColumnName("tc_id");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .HasColumnName("district_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Latitude)
               .HasMaxLength(100)
               .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
               .HasMaxLength(100)
               .HasColumnName("longitude");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("state_name");
            entity.Property(e => e.TcAddress).HasColumnName("tc_address");
            entity.Property(e => e.TcName)
                .HasMaxLength(255)
                .HasColumnName("tc_name");
            entity.Property(e => e.TcType)
                .HasMaxLength(50)
                .HasColumnName("tc_type");
            entity.Property(e => e.TpUsername)
                .HasMaxLength(50)
                .HasColumnName("tp_username");
        });


        modelBuilder.Entity<TrainingScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("training_schemes_pkey");

            entity.ToTable("training_schemes", "skill_india");

            entity.HasIndex(e => e.SchemeId, "training_schemes_scheme_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BasicschemeId)
                .HasMaxLength(50)
                .HasColumnName("basicscheme_id");
            entity.Property(e => e.BasicschemeName)
                .HasMaxLength(255)
                .HasColumnName("basicscheme_name");
           
            entity.Property(e => e.InitiativeOf)
                .HasMaxLength(255)
                .HasColumnName("initiative_of");
            entity.Property(e => e.IsQpLinkedScheme)
                .HasDefaultValue(false)
                .HasColumnName("is_qp_linked_scheme");
            entity.Property(e => e.ProgramBy)
                .HasMaxLength(255)
                .HasColumnName("program_by");

            entity.Property(e => e.RulesEffectiveFrom)
               .HasMaxLength(255)
               .HasColumnName("rules_effective_from");

            entity.Property(e => e.RulesEffectiveTo)
               .HasMaxLength(255)
               .HasColumnName("rules_effective_to");
            entity.Property(e => e.SchemeId)
                .HasMaxLength(50)
                .HasColumnName("scheme_id");
            entity.Property(e => e.SchemeName)
                .HasMaxLength(255)
                .HasColumnName("scheme_name");
            entity.Property(e => e.SchemeReferenceType)
                .HasMaxLength(100)
                .HasColumnName("scheme_reference_type");
            entity.Property(e => e.SchemeWorkflowDiscrip)
                .HasMaxLength(255)
                .HasColumnName("scheme_workflow_discrip");
            entity.Property(e => e.SchemeWorkflowId).HasColumnName("scheme_workflow_id");
            entity.Property(e => e.SchemeWorkflowName)
                .HasMaxLength(100)
                .HasColumnName("scheme_workflow_name");
            entity.Property(e => e.Schemeworkflownames)
                .HasMaxLength(100)
                .HasColumnName("schemeworkflownames");
            entity.Property(e => e.SidSchemeDisplayName)
                .HasMaxLength(255)
                .HasColumnName("sid_scheme_display_name");
            entity.Property(e => e.SidSchemeName)
                .HasMaxLength(255)
                .HasColumnName("sid_scheme_name");
            entity.Property(e => e.SubSchemeName)
                .HasMaxLength(255)
                .HasColumnName("sub_scheme_name");
            entity.Property(e => e.TrainingType)
                .HasMaxLength(100)
                .HasColumnName("training_type");
        });


        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("batches_pkey");

            entity.ToTable("batches", "skill_india");

            entity.Property(e => e.BatchId)
                .HasMaxLength(50)
                .HasColumnName("batch_id");
            entity.Property(e => e.BatchEndDate).HasColumnName("batch_end_date");
            entity.Property(e => e.BatchName)
                .HasMaxLength(200)
                .HasColumnName("batch_name");
            entity.Property(e => e.BatchSize).HasColumnName("batch_size");
            entity.Property(e => e.BatchStage)
                .HasMaxLength(50)
                .HasColumnName("batch_stage");
            entity.Property(e => e.BatchStartDate).HasColumnName("batch_start_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, 2147483647L, null, null)
                .HasColumnName("id");
            entity.Property(e => e.Pincode)
                .HasMaxLength(20)
                .HasColumnName("pincode");
            entity.Property(e => e.SchemeId)
                .HasMaxLength(100)
                .HasColumnName("scheme_id");
            entity.Property(e => e.SchemeName)
                .HasMaxLength(255)
                .HasColumnName("scheme_name");
            entity.Property(e => e.TcAddressLine)
                .HasMaxLength(500)
                .HasColumnName("tc_address_line");
            entity.Property(e => e.TcId)
                .HasMaxLength(50)
                .HasColumnName("tc_id");
            entity.Property(e => e.TcLatitude)
                .HasMaxLength(50)
                .HasColumnName("tc_latitude");
            entity.Property(e => e.TcLongitude)
                .HasMaxLength(50)
                .HasColumnName("tc_longitude");
            entity.Property(e => e.TcName)
                .HasMaxLength(255)
                .HasColumnName("tc_name");
            entity.Property(e => e.TcSpocEmail)
                .HasMaxLength(150)
                .HasColumnName("tc_spoc_email");
            entity.Property(e => e.TcSpocMobile)
                .HasMaxLength(20)
                .HasColumnName("tc_spoc_mobile");
            entity.Property(e => e.TcSpocName)
                .HasMaxLength(150)
                .HasColumnName("tc_spoc_name");
            entity.Property(e => e.TpId)
                .HasMaxLength(50)
                .HasColumnName("tp_id");
            entity.Property(e => e.TpName)
                .HasMaxLength(255)
                .HasColumnName("tp_name");
        });

        modelBuilder.Entity<BatchJobRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("batch_job_roles_pkey");

            entity.ToTable("batch_job_roles", "skill_india");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId)
                .HasColumnType("character varying")
                .HasColumnName("batch_id");
            entity.Property(e => e.JobName)
                .HasMaxLength(200)
                .HasColumnName("job_name");
            entity.Property(e => e.JobRoleDesc)
                .HasMaxLength(500)
                .HasColumnName("job_role_desc");
            entity.Property(e => e.NsqfLevel)
                .HasMaxLength(10)
                .HasColumnName("nsqf_level");
            entity.Property(e => e.QpCode)
                .HasMaxLength(50)
                .HasColumnName("qp_code");
            entity.Property(e => e.SectorId)
                .HasMaxLength(50)
                .HasColumnName("sector_id");
            entity.Property(e => e.SectorName)
                .HasMaxLength(200)
                .HasColumnName("sector_name");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .HasColumnName("version");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchJobRoles)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("batch_job_roles_batch_id_fkey");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Candidateid).HasName("candidates_pkey");

            entity.ToTable("candidates", "skill_india");

            entity.Property(e => e.Candidateid)
                .HasMaxLength(50)
                .HasColumnName("candidateid");
            entity.Property(e => e.Aadharreference)
                .HasMaxLength(100)
                .HasColumnName("aadharreference");
            entity.Property(e => e.Addressline1)
                .HasMaxLength(200)
                .HasColumnName("addressline1");
            entity.Property(e => e.Candidatename)
                .HasMaxLength(200)
                .HasColumnName("candidatename");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Dob)
                .HasMaxLength(100)
                .HasColumnName("dob");
            entity.Property(e => e.Emailid)
                .HasMaxLength(100)
                .HasColumnName("emailid");
            entity.Property(e => e.Ewsdocumenturl)
                .HasMaxLength(200)
                .HasColumnName("ewsdocumenturl");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, 2147483647L, null, null)
                .HasColumnName("id");
            entity.Property(e => e.Isews)
                .HasMaxLength(10)
                .HasColumnName("isews");
            entity.Property(e => e.Isminority)
                .HasMaxLength(10)
                .HasColumnName("isminority");
            entity.Property(e => e.Ispwd)
                .HasMaxLength(10)
                .HasColumnName("ispwd");
            entity.Property(e => e.Minoritydocumenturl)
                .HasMaxLength(200)
                .HasColumnName("minoritydocumenturl");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("mobile");
            entity.Property(e => e.Pwddocumenturl)
                .HasMaxLength(200)
                .HasColumnName("pwddocumenturl");
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .HasColumnName("religion");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Stateid).HasColumnName("stateid");
        });

        modelBuilder.Entity<Candidatetrainingdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("candidatetrainingdetails_pkey");

            entity.ToTable("candidatetrainingdetails", "skill_india");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, 2147483647L, null, null)
                .HasColumnName("id");
            entity.Property(e => e.Assessmentdate)
                .HasMaxLength(100)
                .HasColumnName("assessmentdate");
            entity.Property(e => e.Batchid).HasColumnName("batchid");
            entity.Property(e => e.Batchstage)
                .HasMaxLength(50)
                .HasColumnName("batchstage");
            entity.Property(e => e.Batchtype)
                .HasMaxLength(50)
                .HasColumnName("batchtype");
            entity.Property(e => e.Candidateid)
                .HasColumnType("character varying")
                .HasColumnName("candidateid");
            entity.Property(e => e.Certificatelink)
                .HasMaxLength(200)
                .HasColumnName("certificatelink");
            entity.Property(e => e.Subschemename)
                .HasMaxLength(200)
                .HasColumnName("subschemename");
            entity.Property(e => e.Tcid)
                .HasMaxLength(50)
                .HasColumnName("tcid");
            entity.Property(e => e.Tcname)
                .HasMaxLength(200)
                .HasColumnName("tcname");
            entity.Property(e => e.Tpid)
                .HasMaxLength(50)
                .HasColumnName("tpid");
            entity.Property(e => e.Tpname)
                .HasMaxLength(200)
                .HasColumnName("tpname");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Candidatetrainingdetails)
                .HasForeignKey(d => d.Candidateid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("candidatetrainingdetails_candidateid_fkey");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
