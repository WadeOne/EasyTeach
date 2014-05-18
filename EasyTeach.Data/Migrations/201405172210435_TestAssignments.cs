namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestAssignments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedQuizDtoes",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        NumberOfQuestions = c.Int(nullable: false),
                        Group_GroupId = c.Int(),
                        Quiz_QuizId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.GroupDtoes", t => t.Group_GroupId)
                .ForeignKey("dbo.QuizDtoes", t => t.Quiz_QuizId)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Quiz_QuizId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedQuizDtoes", "Quiz_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.AssignedQuizDtoes", "Group_GroupId", "dbo.GroupDtoes");
            DropIndex("dbo.AssignedQuizDtoes", new[] { "Quiz_QuizId" });
            DropIndex("dbo.AssignedQuizDtoes", new[] { "Group_GroupId" });
            DropTable("dbo.AssignedQuizDtoes");
        }
    }
}
