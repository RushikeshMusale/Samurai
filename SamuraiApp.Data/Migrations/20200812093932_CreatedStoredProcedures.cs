using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class CreatedStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create or alter procedure dbo.GetSamuraiWhoSaidAWord                
                @text varchar(30)
                as
                select samurais.id, samurais.name, samurais.ClanId
                from samurais inner join 
                    Quotes on quotes.samuraiId = samurais.id
                where (quotes.text like '%'+@text+'%')
            ");

            migrationBuilder.Sql(@"
                create procedure dbo.DeleteQuotesForSmaurai
                @samuraiId int
                as
                delete from quotes where quotes.samuraiId = @samuraiId    
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop procedure dbo.GetSamuraiWhoSaidAWord");
            migrationBuilder.Sql(@"drop procedure dbo.DeleteQuotesForSmaurai");
        }
    }
}
