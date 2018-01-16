namespace BackOfficeWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ctest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ApplicationStatId = c.Int(nullable: false),
                        EmployeeId = c.String(maxLength: 128),
                        BilateralProtocol1Id = c.Int(),
                        BilateralProtocol2Id = c.Int(),
                        BilateralProtocol3Id = c.Int(),
                        CreationDate = c.DateTime(nullable: false),
                        ArithmeticMean = c.Double(),
                        ECTS = c.Int(),
                        MotivationLetter = c.Double(),
                        Enterview = c.Double(),
                        FinalGrade = c.Double(),
                        Motivations = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationStats", t => t.ApplicationStatId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .ForeignKey("dbo.BilateralProtocols", t => t.BilateralProtocol1Id)
                .ForeignKey("dbo.BilateralProtocols", t => t.BilateralProtocol2Id)
                .ForeignKey("dbo.BilateralProtocols", t => t.BilateralProtocol3Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId)
                .Index(t => t.StudentId)
                .Index(t => t.ApplicationStatId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BilateralProtocol1Id)
                .Index(t => t.BilateralProtocol2Id)
                .Index(t => t.BilateralProtocol3Id);
            
            CreateTable(
                "dbo.ApplicationStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BilateralProtocols",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Destination = c.String(nullable: false),
                        OpenSlots = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CollegeSubjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.CollegeSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 100),
                        SubjectAlias = c.String(nullable: false, maxLength: 10),
                        CollegeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colleges", t => t.CollegeId, cascadeDelete: true)
                .Index(t => t.CollegeId);
            
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollegeName = c.String(nullable: false, maxLength: 100),
                        CollegeAlias = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserFullname = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        UserAddress = c.String(nullable: false, maxLength: 450),
                        UserCc = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        StudentNumber = c.String(),
                        ALOGrade = c.Int(),
                        CollegeSubjectId = c.Int(),
                        EmployeeNumber = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        College_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CollegeSubjects", t => t.CollegeSubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Colleges", t => t.College_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CollegeSubjectId)
                .Index(t => t.College_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        FileUrl = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        ApplicationId = c.Int(),
                        EmployeeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId)
                .Index(t => t.ApplicationId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ApplicationStatHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Int(nullable: false),
                        ApplicationStat = c.String(),
                        DateOfUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 500),
                        TextContent = c.String(nullable: false, maxLength: 1000),
                        IsPublished = c.Boolean(nullable: false),
                        DocumentId = c.Int(),
                        OpenDate = c.DateTime(),
                        CloseDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorCode = c.String(nullable: false),
                        ErrorDescription = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HelpName = c.String(nullable: false),
                        HelpDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quizzs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                        QuizzUrl = c.String(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.News", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.News", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.ApplicationStatHistories", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Applications", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Applications", "BilateralProtocol3Id", "dbo.BilateralProtocols");
            DropForeignKey("dbo.Applications", "BilateralProtocol2Id", "dbo.BilateralProtocols");
            DropForeignKey("dbo.Applications", "BilateralProtocol1Id", "dbo.BilateralProtocols");
            DropForeignKey("dbo.BilateralProtocols", "SubjectId", "dbo.CollegeSubjects");
            DropForeignKey("dbo.CollegeSubjects", "CollegeId", "dbo.Colleges");
            DropForeignKey("dbo.AspNetUsers", "College_Id", "dbo.Colleges");
            DropForeignKey("dbo.AspNetUsers", "CollegeSubjectId", "dbo.CollegeSubjects");
            DropForeignKey("dbo.Applications", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applications", "ApplicationStatId", "dbo.ApplicationStats");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.News", new[] { "DocumentId" });
            DropIndex("dbo.News", new[] { "EmployeeId" });
            DropIndex("dbo.ApplicationStatHistories", new[] { "ApplicationId" });
            DropIndex("dbo.Documents", new[] { "EmployeeId" });
            DropIndex("dbo.Documents", new[] { "ApplicationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "College_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CollegeSubjectId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CollegeSubjects", new[] { "CollegeId" });
            DropIndex("dbo.BilateralProtocols", new[] { "SubjectId" });
            DropIndex("dbo.Applications", new[] { "BilateralProtocol3Id" });
            DropIndex("dbo.Applications", new[] { "BilateralProtocol2Id" });
            DropIndex("dbo.Applications", new[] { "BilateralProtocol1Id" });
            DropIndex("dbo.Applications", new[] { "EmployeeId" });
            DropIndex("dbo.Applications", new[] { "ApplicationStatId" });
            DropIndex("dbo.Applications", new[] { "StudentId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Helps");
            DropTable("dbo.Errors");
            DropTable("dbo.News");
            DropTable("dbo.ApplicationStatHistories");
            DropTable("dbo.Documents");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Colleges");
            DropTable("dbo.CollegeSubjects");
            DropTable("dbo.BilateralProtocols");
            DropTable("dbo.ApplicationStats");
            DropTable("dbo.Applications");
        }
    }
}
