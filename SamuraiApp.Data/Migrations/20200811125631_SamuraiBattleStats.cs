using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class SamuraiBattleStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create function [dbo].[EarliestBattleFoughtBySamurai] (@samuraiId int)
                returns char(30) as 
                begin
                    Declare @ret char(30)
                    select top 1 @ret = name
                    from Battles
                    where Battles.BattleId in (Select BattleId from SamuraiBattle
                                         where SamuraiId = @samuraiId)
                    order by startdate
                    return @ret
                end
            ");

            migrationBuilder.Sql(@"
                create or alter view [dbo].[SamuraiBattleStats]
                as 
                select dbo.samurais.name,
                count(dbo.samuraiBattle.battleId) as NumberOfBattles,
                dbo.EarliestBattleFoughtBySamurai(min(dbo.samurais.id)) as EarliestBattle
                from dbo.samuraiBattle inner join
                    dbo.samurais on dbo.samurais.Id = dbo.samuraiBattle.samuraiID   
                group by dbo.samurais.Name, dbo.samuraiBattle.SamuraiId                
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view dbo.samuraiBattleStats");
            migrationBuilder.Sql(@"drop function dbo.EarliestBattleFoughtBySamurai");
        }
    }
}
