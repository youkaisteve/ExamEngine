﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExamSystemEntities : DbContext
    {
        public ExamSystemEntities()
            : base("name=ExamSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleUser> RoleUser { get; set; }
        public DbSet<StandardAnwser> StandardAnwser { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserAnwser> UserAnwser { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<WorkflowTeamRelation> WorkflowTeamRelation { get; set; }
        public DbSet<Functions> Functions { get; set; }
        public DbSet<RoleFunction> RoleFunction { get; set; }
        public DbSet<SocialSecurityPersonnel> SocialSecurityPersonnel { get; set; }
        public DbSet<AssignedUser> AssignedUser { get; set; }
        public DbSet<ProcessInfo> ProcessInfo { get; set; }
        public DbSet<TiKuDetail> TiKuDetail { get; set; }
        public DbSet<TiKuMaster> TiKuMaster { get; set; }
    }
}
