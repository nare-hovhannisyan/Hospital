﻿namespace Wpf_UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Wpf_UI.Services;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Start = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                        Type = c.String(),
                        Diagnosis = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        DateofBirth = c.DateTime(nullable: false, storeType: "date"),
                        Address = c.String(),
                        TelephoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            string password = HashFunction.ComputeSha256Hash("admin");
            Sql($"INSERT INTO Users (Login, Password) VALUES ('admin','{password}')");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropTable("dbo.Users");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
        }
    }
}
