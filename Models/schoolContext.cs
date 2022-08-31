using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class schoolContext : DbContext
    {
        public schoolContext()
        {
        }

        public schoolContext(DbContextOptions<schoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LabForm> LabForm { get; set; }
        public virtual DbSet<LabInsuranceFee> LabInsuranceFee { get; set; }
        public virtual DbSet<LabPayment> LabPayment { get; set; }
        public virtual DbSet<LabInsuranceRatio> LabInsuranceRatio { get; set; }
        public virtual DbSet<LabLaborerRank> LabLaborerRank { get; set; }
        public virtual DbSet<LabProject> LabProject { get; set; }
        public virtual DbSet<LabRetireRank> LabRetireRank { get; set; }
        public virtual DbSet<LabStudent> LabStudent { get; set; }
        public virtual DbSet<Management_table> Management_table { get; set; }
        public virtual DbSet<LabTeacher> LabTeacher { get; set; }
        public virtual DbSet<SpInsuranceFee> SpInsuranceFee { get; set; }
        public virtual DbSet<Sp_StudentInfo> Sp_StudentInfo { get; set; }
        public virtual DbSet<Sp_GetProjectBudget> Sp_GetProjectBudget { get; set; }
        public virtual DbSet<Sp_GetHiredDep> Sp_GetHiredDep { get; set; }
        public virtual DbSet<Sp_GetDepBudget> Sp_GetDepBudget { get; set; }
        public virtual DbSet<Sp_SearchProjectBudget> Sp_SearchProjectBudget { get; set; }
        public virtual DbSet<Sp_SearchDepBudget> Sp_SearchDepBudget { get; set; }
        public virtual DbSet<LabFormSearch> LabFormSearch { get; set; }
        public virtual DbSet<PaymentSearch> PaymentSearch { get; set; }
        public virtual DbSet<LabFormDetail> LabFormDetail { get; set; }
        public virtual DbSet<AllTime> AllTime { get; set; }
        public virtual DbSet<PaymentTime> PaymentTime { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("server=tcp: oppq1234.ddns.net\\MSSQLSERVER,1433;Database=school;Trusted_Connection=False;User ID=tke;Password=9527");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocDeparts>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Doc_Departs");

                entity.Property(e => e.DepCname)
                    .HasColumnName("DepCName")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Depalias)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<DocVwMultiUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Doc_vwMultiUser");

                entity.Property(e => e.DepAlias)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.DepartId)
                    .IsRequired()
                    .HasColumnName("DepartID")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserNo)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LabForm>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK_Lab_Form_1");

                entity.ToTable("Lab_Form");

                entity.Property(e => e.ActualStartTime)
                    .HasColumnName("Actual_start_time")
                    .HasColumnType("date");

                entity.Property(e => e.ApplyItem)
                    .IsRequired()
                    .HasColumnName("Apply_item")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Boss)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bossdep)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bossemail)
                    .HasColumnName("boss_email")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.pclass)
                    .HasColumnName("pclass")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Is_Nuk)
                    .HasColumnName("Is_Nuk")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Budgettype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Disability)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.FormStatus)
                    .IsRequired()
                    .HasColumnName("Form_status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fundname)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HiredDep)
                    .IsRequired()
                    .HasColumnName("Hired_dep")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HiredName)
                    .IsRequired()
                    .HasColumnName("Hired_name")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ID)
                    .IsRequired()
                    .HasColumnName("ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.BossID)
                    .HasColumnName("BossID")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InsuranceType)
                    .HasColumnName("Insurance_type")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsForeign)
                    .IsRequired()
                    .HasColumnName("Is_foreign")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PartSalary).HasColumnName("Part_salary");

                entity.Property(e => e.PartDay).HasColumnName("Part_day");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bugeno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReceivePerson)
                    .HasColumnName("Receive_person")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("Receive_time")
                    .HasColumnType("date");

                entity.Property(e => e.RejectReason)
                    .HasColumnName("Reject_reason")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SelfRetirePercent).HasColumnName("Self_retire_percent");

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .IsRequired()
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WorkType)
                    .IsRequired()
                    .HasColumnName("Work_type")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LabInsuranceFee>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK__Lab_Insu__3214D4A859351F06");

                entity.ToTable("Lab_InsuranceFee");

                entity.Property(e => e.StudentID)
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Boss)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DepLabor).HasColumnName("Dep_labor");

                entity.Property(e => e.DepRetire).HasColumnName("Dep_retire");

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.LaborFund).HasColumnName("Labor_fund");

                entity.Property(e => e.LaborRank).HasColumnType("decimal(16, 0)");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RetireRank).HasColumnType("decimal(16, 0)");

                entity.Property(e => e.SelfLabor).HasColumnName("Self_labor");

                entity.Property(e => e.SelfRetire).HasColumnName("Self_retire");

                entity.Property(e => e.SelfRetirePercent).HasColumnName("Self_retire_percent");

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .IsRequired()
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.FormNoNavigation)
                    .WithMany(p => p.LabInsuranceFee)
                    .HasForeignKey(d => d.FormNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab_Insur__FormN__2057CCD0");
            });

            modelBuilder.Entity<PaymentTimeFee>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.StudentID)
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Boss)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DepLabor).HasColumnName("Dep_labor");

                entity.Property(e => e.DepRetire).HasColumnName("Dep_retire");

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.LaborFund).HasColumnName("Labor_fund");

                entity.Property(e => e.LaborRank).HasColumnType("decimal(16, 0)");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RetireRank).HasColumnType("decimal(16, 0)");

                entity.Property(e => e.SelfLabor).HasColumnName("Self_labor");

                entity.Property(e => e.SelfRetire).HasColumnName("Self_retire");

                entity.Property(e => e.SelfRetirePercent).HasColumnName("Self_retire_percent");

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .IsRequired()
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LabPayment>(entity =>
            {entity.HasKey(e => e.No)
                    .HasName("PK__Lab_Paym__3214D4A82EA537B3");

                entity.ToTable("Lab_Payment");

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.FormNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Payment)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                //entity.Property(e => e.paid);
            });

            modelBuilder.Entity<LabInsuranceRatio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lab_InsuranceRatio");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LabLaborerRank>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lab_LaborerRank");

                entity.Property(e => e.LaborerRank).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<LabProject>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK__Lab_Proj__3214D4A898B85FD3");

                entity.ToTable("Lab_Project");

                entity.Property(e => e.No).ValueGeneratedNever();

                entity.Property(e => e.AccountNo).HasColumnName("Account_No");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<LabRetireRank>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lab_RetireRank");

                entity.Property(e => e.RetireRank).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<LabStudent>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__Lab_Stud__A2F4E9ACBCF6EF5B");

                entity.ToTable("Lab_Student");

                entity.Property(e => e.StudentId)
                    .HasColumnName("Student_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LabTeacher>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PK__Lab_Teac__92FF4C8B73DD47B7");

                entity.ToTable("Lab_Teacher");

                entity.Property(e => e.UserNo).ValueGeneratedNever();

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DepartId).HasColumnName("DepartID");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TranUsecode2>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("tran_usecode2");

                entity.Property(e => e.No).HasColumnName("no");

                entity.Property(e => e.Card)
                    .HasColumnName("card")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Deptno)
                    .HasColumnName("deptno")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Fundkind)
                    .IsRequired()
                    .HasColumnName("fundkind")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fundno)
                    .IsRequired()
                    .HasColumnName("fundno")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Projectno)
                    .HasColumnName("PROJECTNO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Setcard)
                    .HasColumnName("setcard")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Setcard2)
                    .HasColumnName("setcard2")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Setcard3)
                    .HasColumnName("setcard3")
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VDepBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("V_DepBudget");

                entity.Property(e => e.Budget)
                    .HasColumnName("BUDGET")
                    .HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Bugetno)
                    .IsRequired()
                    .HasColumnName("BUGETNO")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bugname)
                    .IsRequired()
                    .HasColumnName("BUGNAME")
                    .IsUnicode(false);

                entity.Property(e => e.Byear)
                    .IsRequired()
                    .HasColumnName("BYear")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Fundname)
                    .IsRequired()
                    .HasColumnName("FUNDNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fundno)
                    .IsRequired()
                    .HasColumnName("FUNDNO")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leader)
                    .IsRequired()
                    .HasColumnName("LEADER")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leaderdep)
                    .HasColumnName("LEADERDEP")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leaderid)
                    .IsRequired()
                    .HasColumnName("LEADERID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Remain)
                    .HasColumnName("REMAIN")
                    .HasColumnType("decimal(15, 0)");

                entity.Property(e => e.Shiftin)
                    .HasColumnName("SHIFTIN")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Shiftout)
                    .HasColumnName("SHIFTOUT")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Sourcetype)
                    .HasColumnName("SOURCETYPE")
                    .HasColumnType("decimal(1, 0)");
            });

            modelBuilder.Entity<VProjectBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("V_ProjectBudget");

                entity.Property(e => e.Budget)
                    .HasColumnName("BUDGET")
                    .HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Bugetno)
                    .IsRequired()
                    .HasColumnName("BUGETNO")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bugname)
                    .IsRequired()
                    .HasColumnName("BUGNAME")
                    .IsUnicode(false);

                entity.Property(e => e.Closetype)
                    .HasColumnName("closetype")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Com)
                    .IsRequired()
                    .HasColumnName("COM")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Comname)
                    .IsRequired()
                    .HasColumnName("COMNAME")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Deadline)
                    .IsRequired()
                    .HasColumnName("DEADLINE")
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fundname)
                    .IsRequired()
                    .HasColumnName("FUNDNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fundno)
                    .IsRequired()
                    .HasColumnName("FUNDNO")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leader)
                    .IsRequired()
                    .HasColumnName("LEADER")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leaderdep)
                    .HasColumnName("LEADERDEP")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Leaderid)
                    .IsRequired()
                    .HasColumnName("LEADERID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Pjttype)
                    .IsRequired()
                    .HasColumnName("PJTTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Projectno)
                    .IsRequired()
                    .HasColumnName("PROJECTNO")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Remain)
                    .HasColumnName("REMAIN")
                    .HasColumnType("decimal(15, 0)");

                entity.Property(e => e.Shiftin)
                    .HasColumnName("SHIFTIN")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Shiftout)
                    .HasColumnName("SHIFTOUT")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Sourcetype)
                    .HasColumnName("SOURCETYPE")
                    .HasColumnType("decimal(1, 0)");

                entity.Property(e => e.Start)
                    .IsRequired()
                    .HasColumnName("START")
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SpInsuranceFee>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LaborRank)
                        .HasColumnName("LaborRank")
                        .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.RetireRank)
                        .HasColumnName("RetireRank")
                        .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.SelfLabor).HasColumnName("Self_Labor");

                entity.Property(e => e.DepLabor).HasColumnName("Dep_Labor");

                entity.Property(e => e.SelfRetire).HasColumnName("Self_Retire");

                entity.Property(e => e.DepRetire).HasColumnName("Dep_Retire");

                entity.Property(e => e.LaborFund).HasColumnName("Labor_Fund");
            });
            modelBuilder.Entity<Sp_StudentInfo>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Student_ID).HasColumnName("Student_ID").HasMaxLength(10);
                entity.Property(e => e.cname).HasColumnName("cname").HasMaxLength(10);
                entity.Property(e => e.Birthday).HasColumnName("Birthday").HasMaxLength(8);
                entity.Property(e => e.pclass).HasColumnName("pclass").HasMaxLength(10);
                entity.Property(e => e.sclass).HasColumnName("sclass").HasMaxLength(10);
                entity.Property(e => e.Department).HasColumnName("Department").HasMaxLength(10);
                entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(30);
                entity.Property(e => e.Phone).HasColumnName("Phone").HasMaxLength(10);
            });
            modelBuilder.Entity<Sp_SearchProjectBudget>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.bugname).HasColumnName("bugname");
                entity.Property(e => e.start_time).HasColumnName("start_time").HasMaxLength(7);
                entity.Property(e => e.end_time).HasColumnName("end_time").HasMaxLength(7);
                entity.Property(e => e.source_type).HasColumnName("source_type").HasColumnType("decimal(1, 0)");
                entity.Property(e => e.fund_name).HasColumnName("fund_name").HasMaxLength(50);
                entity.Property(e => e.Proj_Num).HasColumnName("Proj_Num").HasMaxLength(50);
            });
            modelBuilder.Entity<Sp_SearchDepBudget>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.byear).HasColumnName("byear").HasMaxLength(3);
                entity.Property(e => e.bugname).HasColumnName("bugname");
                entity.Property(e => e.source_type).HasColumnName("source_type").HasColumnType("decimal(1, 0)");
                entity.Property(e => e.fund_name).HasColumnName("fund_name").HasMaxLength(50);
            });
            modelBuilder.Entity<Management_table>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.User_No).HasColumnName("User_No").HasMaxLength(10);
                entity.Property(e => e.Dep_Name).HasColumnName("Dep_Name").HasMaxLength(20);
            });            
            modelBuilder.Entity<Sp_GetProjectBudget>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ProjectNo).HasColumnName("ProjectNo").HasMaxLength(15);
                entity.Property(e => e.ProjectName).HasColumnName("ProjectName");
                entity.Property(e => e.FundNo).HasColumnName("FundNo").HasMaxLength(8);
                entity.Property(e => e.FundName).HasColumnName("FundName").HasMaxLength(30);
                entity.Property(e => e.Budget).HasColumnName("Budget").HasColumnType("decimal(11, 0)");
                entity.Property(e => e.Budget_remain).HasColumnName("Budget_remain").HasColumnType("decimal(15, 0)");
                entity.Property(e => e.SourceType).HasColumnName("SourceType").HasColumnType("decimal(1, 0)");
                entity.Property(e => e.ProjNo).HasColumnName("ProjNo").HasMaxLength(50);
                entity.Property(e => e.PJTTYPE).HasColumnName("PJTTYPE").HasMaxLength(1);
                entity.Property(e => e.StartDate).HasColumnName("StartDate").HasMaxLength(7);
                entity.Property(e => e.LeaderCard).HasColumnName("LeaderCard").HasMaxLength(10);
                entity.Property(e => e.LeaderName).HasColumnName("LeaderName").HasMaxLength(40);
                entity.Property(e => e.COM).HasColumnName("COM").HasMaxLength(4);
                entity.Property(e => e.COMName).HasColumnName("COMName").HasMaxLength(60);
                entity.Property(e => e.EndDate).HasColumnName("EndDate").HasMaxLength(7);
            });
            modelBuilder.Entity<Sp_GetDepBudget>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DepNo).HasColumnName("DepNo").HasMaxLength(4);
                entity.Property(e => e.DepartID).HasColumnName("DepartID").HasMaxLength(4);
                entity.Property(e => e.ProjectNo).HasColumnName("ProjectNo").HasMaxLength(15);
                entity.Property(e => e.FundNo).HasColumnName("FundNo").HasMaxLength(8);
                entity.Property(e => e.FundName).HasColumnName("FundName").HasMaxLength(30);
                entity.Property(e => e.Budget).HasColumnName("Budget").HasColumnType("decimal(11, 0)");
                entity.Property(e => e.Budget_remain).HasColumnName("Budget_remain").HasColumnType("decimal(15, 0)");
                entity.Property(e => e.SourceType).HasColumnName("SourceType").HasColumnType("decimal(1, 0)");
                entity.Property(e => e.Leader).HasColumnName("Leader").HasMaxLength(40);
                entity.Property(e => e.LeaderID).HasColumnName("LeaderID").HasMaxLength(10);
                entity.Property(e => e.DepCName).HasColumnName("DepCName").HasMaxLength(30);
            });
            modelBuilder.Entity<Sp_GetHiredDep>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.UserNo).HasColumnName("UserNo").HasMaxLength(4);
                entity.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(50);
                entity.Property(e => e.Depart).HasColumnName("Depart").HasMaxLength(60);
                entity.Property(e => e.DepName).HasColumnName("DepName").HasMaxLength(20);
            });
            modelBuilder.Entity<LabFormSearch>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.FormStatus)
                    .IsRequired()
                    .HasColumnName("Form_status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HiredName)
                    .IsRequired()
                    .HasColumnName("Hired_name")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bugeno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .IsRequired()
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasColumnName("Apply_item")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ID)
                    .IsRequired()
                    .HasColumnName("ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Is_foreign)
                    .IsRequired()
                    .HasColumnName("Is_foreign")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Birthday)
                    .HasColumnName("Birthday")
                    .HasColumnType("date");
                entity.Property(e => e.BossID)
                    .HasColumnName("BossID")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SelfRetirePercent)
                    .HasColumnName("SelfRetirePercent");
                entity.Property(e => e.Salary)
                    .HasColumnName("Salary");               

            });

            modelBuilder.Entity<LabFormDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActualStartTime)
                    .HasColumnName("Actual_start_time")
                    .HasColumnType("date");

                entity.Property(e => e.ActualEndTime)
                    .HasColumnName("Actual_end_time")
                    .HasColumnType("date");

                entity.Property(e => e.ApplyItem)
                    .HasColumnName("Apply_item")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Boss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.BossID)
                    .HasColumnName("BossID")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Bossemail)
                    .HasColumnName("boss_email")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bossdep)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Class)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Disability)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndTime)
                    .HasColumnName("End_time")
                    .HasColumnType("date");

                entity.Property(e => e.FormStatus)
                    .HasColumnName("Form_status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HiredDep)
                    .HasColumnName("Hired_dep")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HiredName)
                    .HasColumnName("Hired_name")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ID)
                    .HasColumnName("ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InsuranceType)
                    .HasColumnName("Insurance_type")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsForeign)
                    .HasColumnName("Is_foreign")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PartSalary).HasColumnName("Part_salary");

                entity.Property(e => e.PartDay).HasColumnName("Part_day");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bugeno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReceivePerson)
                    .HasColumnName("Receive_person")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("Receive_time")
                    .HasColumnType("date");

                entity.Property(e => e.RejectReason)
                    .HasColumnName("Reject_reason")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SelfRetirePercent).HasColumnName("Self_retire_percent");

                entity.Property(e => e.StartTime)
                    .HasColumnName("Start_time")
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .HasColumnName("Student_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WorkType)
                    .HasColumnName("Work_type")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.No)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LaborRank)
                        .HasColumnName("LaborRank")
                        .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.RetireRank)
                        .HasColumnName("RetireRank")
                        .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.SelfLabor).HasColumnName("Self_Labor");

                entity.Property(e => e.DepLabor).HasColumnName("Dep_Labor");

                entity.Property(e => e.SelfRetire).HasColumnName("Self_Retire");

                entity.Property(e => e.DepRetire).HasColumnName("Dep_Retire");

                entity.Property(e => e.LaborFund).HasColumnName("Labor_Fund");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.pclass)
                    .HasColumnName("pclass")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Is_Nuk)
                    .HasColumnName("Is_Nuk")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PaymentSearch>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Boss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FormNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndTime)
                    .HasColumnType("date");

                entity.Property(e => e.PaymentStartTime)
                    .HasColumnType("date");

                entity.Property(e => e.PaymentEndTime)
                    .HasColumnType("date");

                entity.Property(e => e.InsuranceType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsForeign)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartTime)
                    .HasColumnType("date");

                entity.Property(e => e.StudentID)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LaborRank)
                        .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.RetireRank)
                        .HasColumnType("decimal(16, 0)");
                entity.Property(e => e.BossID)
                    .HasColumnName("BossID")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ApplyItem)
                    .HasMaxLength(20);
                entity.Property(e => e.Hiredname)
                    .HasMaxLength(20);
                entity.Property(e => e.ID)
                    .HasMaxLength(20);
                entity.Property(e => e.Birthday)
                    .HasColumnType("date");
            });

            modelBuilder.Entity<AllTime>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Time).HasColumnType("date");
            });

            modelBuilder.Entity<PaymentTime>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.StartTime).HasColumnType("date");
                entity.Property(e => e.EndTime).HasColumnType("date");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
