namespace FollowMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lala : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Album_id = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        Artist_id = c.Int(),
                        Genre_id = c.Int(),
                        Label_id = c.Int(),
                    })
                .PrimaryKey(t => t.Album_id)
                .ForeignKey("dbo.Artists", t => t.Artist_id)
                .ForeignKey("dbo.Genres", t => t.Genre_id)
                .ForeignKey("dbo.Labels", t => t.Label_id)
                .Index(t => t.Artist_id)
                .Index(t => t.Genre_id)
                .Index(t => t.Label_id);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Artist_id = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(nullable: false, maxLength: 30),
                        Biography = c.String(maxLength: 200),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Artist_id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Genre_id = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false, maxLength: 50),
                        Descripition = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Genre_id);
            
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        Label_id = c.Int(nullable: false, identity: true),
                        Label_name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Countries = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Label_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        User_id = c.Int(nullable: false),
                        Status_id = c.Int(nullable: false),
                        Shop_id = c.Int(nullable: false),
                        Album_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_id)
                .ForeignKey("dbo.Albums", t => t.Album_id, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.Shop_id, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Status_id)
                .Index(t => t.Shop_id)
                .Index(t => t.Album_id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Shop_id = c.Int(nullable: false, identity: true),
                        ShopName = c.String(nullable: false, maxLength: 50),
                        ShopCountry = c.String(),
                        ShopCity = c.String(),
                        ShopContact = c.String(),
                    })
                .PrimaryKey(t => t.Shop_id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_id = c.Int(nullable: false, identity: true),
                        Status1 = c.String(),
                        FAQ = c.String(),
                    })
                .PrimaryKey(t => t.Status_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        UserNickName = c.String(nullable: false, maxLength: 50),
                        UserConcacts = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.User_id);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Playlist_id = c.Int(nullable: false, identity: true),
                        PlaylistName = c.String(nullable: false, maxLength: 50),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Playlist_id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Song_id = c.Int(nullable: false, identity: true),
                        SongName = c.String(nullable: false, maxLength: 50),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Song_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NickName = c.String(),
                        Contacts = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.SongAlbums",
                c => new
                    {
                        Song_Song_id = c.Int(nullable: false),
                        Album_Album_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_Song_id, t.Album_Album_id })
                .ForeignKey("dbo.Songs", t => t.Song_Song_id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Album_id, cascadeDelete: true)
                .Index(t => t.Song_Song_id)
                .Index(t => t.Album_Album_id);
            
            CreateTable(
                "dbo.SongPlaylists",
                c => new
                    {
                        Song_Song_id = c.Int(nullable: false),
                        Playlist_Playlist_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_Song_id, t.Playlist_Playlist_id })
                .ForeignKey("dbo.Songs", t => t.Song_Song_id, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.Playlist_Playlist_id, cascadeDelete: true)
                .Index(t => t.Song_Song_id)
                .Index(t => t.Playlist_Playlist_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Playlists", "User_id", "dbo.Users");
            DropForeignKey("dbo.SongPlaylists", "Playlist_Playlist_id", "dbo.Playlists");
            DropForeignKey("dbo.SongPlaylists", "Song_Song_id", "dbo.Songs");
            DropForeignKey("dbo.SongAlbums", "Album_Album_id", "dbo.Albums");
            DropForeignKey("dbo.SongAlbums", "Song_Song_id", "dbo.Songs");
            DropForeignKey("dbo.Orders", "User_id", "dbo.Users");
            DropForeignKey("dbo.Orders", "Status_id", "dbo.Status");
            DropForeignKey("dbo.Orders", "Shop_id", "dbo.Shops");
            DropForeignKey("dbo.Orders", "Album_id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "Label_id", "dbo.Labels");
            DropForeignKey("dbo.Albums", "Genre_id", "dbo.Genres");
            DropForeignKey("dbo.Albums", "Artist_id", "dbo.Artists");
            DropIndex("dbo.SongPlaylists", new[] { "Playlist_Playlist_id" });
            DropIndex("dbo.SongPlaylists", new[] { "Song_Song_id" });
            DropIndex("dbo.SongAlbums", new[] { "Album_Album_id" });
            DropIndex("dbo.SongAlbums", new[] { "Song_Song_id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Playlists", new[] { "User_id" });
            DropIndex("dbo.Orders", new[] { "Album_id" });
            DropIndex("dbo.Orders", new[] { "Shop_id" });
            DropIndex("dbo.Orders", new[] { "Status_id" });
            DropIndex("dbo.Orders", new[] { "User_id" });
            DropIndex("dbo.Albums", new[] { "Label_id" });
            DropIndex("dbo.Albums", new[] { "Genre_id" });
            DropIndex("dbo.Albums", new[] { "Artist_id" });
            DropTable("dbo.SongPlaylists");
            DropTable("dbo.SongAlbums");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Songs");
            DropTable("dbo.Playlists");
            DropTable("dbo.Users");
            DropTable("dbo.Status");
            DropTable("dbo.Shops");
            DropTable("dbo.Orders");
            DropTable("dbo.Labels");
            DropTable("dbo.Genres");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
