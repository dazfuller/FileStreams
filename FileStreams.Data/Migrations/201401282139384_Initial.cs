namespace FileStreams.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Occupancy = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);

            Sql("alter table [dbo].[Photos] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Photos] add constraint [UQ_Photos_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Photos] add constraint [DF_Photos_RowId] default (newid()) for [RowId]");
            Sql("alter table [dbo].[Photos] add [Data] varbinary(max) FILESTREAM not null");
            Sql("alter table [dbo].[Photos] add constraint [DF_Photos_Data] default(0x) for [Data]");
        }
        
        public override void Down()
        {
            Sql("alter table [dbo].[Photos] drop constraint [DF_Photos_Data]");
            Sql("alter table [dbo].[Photos] drop column [Data]");

            Sql("alter table [dbo].[Photos] drop constraint [UQ_Photos_RowId]");
            Sql("alter table [dbo].[Photos] drop constraint [DF_Photos_RowId]");
            Sql("alter table [dbo].[Photos] drop column [RowId]");

            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Photos", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Hotels", "LocationId", "dbo.Locations");
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.Photos", new[] { "RoomId" });
            DropIndex("dbo.Hotels", new[] { "LocationId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Rooms");
            DropTable("dbo.Locations");
            DropTable("dbo.Hotels");
        }
    }
}
