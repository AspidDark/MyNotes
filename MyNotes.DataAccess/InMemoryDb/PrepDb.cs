using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.InMemoryDb
{
    public static class PrepDb
    {
        public static Guid user1Id = Guid.Parse("c2f8bbf7-0564-4ad6-9a4d-4925a037e152");
        public static Guid user2Id = Guid.Parse("c2f8bbf7-0564-4ad6-9a4d-4925a037e153");

        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Topics.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                Topic topic1 = new()
                {
                    Id=Guid.NewGuid(),
                    CreateDate=GetRandomDate(DateTime.Now),
                    EditDate= GetRandomDate(DateTime.Now),
                    IsDeleted=0,
                    Name="Topic1",
                    OwnerId= user1Id
                };
                Topic topic2 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now.AddYears(-1)),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Name = "Topic2",
                    OwnerId = user1Id
                };
                Topic topic3 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Name = "Topic3",
                    OwnerId = user1Id
                };
                context.Topics.AddRange(topic1, topic2, topic3);

                //Paragrphs
                //For First Topic
                Paragraph paragraph1 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic=topic1,
                    TopicId= topic1.Id,
                    OwnerId = user1Id,
                    Name= "paragraph1"
                };
                Paragraph paragraph2 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic1,
                    TopicId = topic1.Id,
                    OwnerId = user1Id,
                    Name = "paragraph2"
                };
                Paragraph paragraph3 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic1,
                    TopicId = topic1.Id,
                    OwnerId = user1Id,
                    Name = "paragraph3"
                };
                Paragraph paragraph4 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic1,
                    TopicId = topic1.Id,
                    OwnerId = user1Id,
                    Name = "paragraph4"
                };

                //For Second Topic
                Paragraph paragraph5 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic2,
                    TopicId = topic2.Id,
                    OwnerId = user1Id,
                    Name = "paragraph5"
                };
                Paragraph paragraph6 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic2,
                    TopicId = topic2.Id,
                    OwnerId = user1Id,
                    Name = "paragraph6"
                };
                Paragraph paragraph7 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    Topic = topic2,
                    TopicId = topic2.Id,
                    OwnerId = user1Id,
                    Name = "paragraph7"
                };
                context.Paragraphs.AddRange(paragraph1, paragraph2, paragraph3, paragraph4, paragraph5, paragraph6, paragraph7);

                //Notes
                //For Paragraph1
                Note note1 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph=paragraph1,
                    ParagraphId=paragraph1.Id,
                    Message = "note1note1 note1note1 note1note1"
                };

                Note note2 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph1,
                    ParagraphId = paragraph1.Id,
                    Message = "note2note2 note2note2 note2note2"
                };

                Note note3 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph1,
                    ParagraphId = paragraph1.Id,
                    Message = "note3note2 note2note2 note2note3"
                };

                Note note4 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph1,
                    ParagraphId = paragraph1.Id,
                    Message = "note4note2 note2note2 note2note4"
                };

                Note note5 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph2,
                    ParagraphId = paragraph2.Id,
                    Message = "note5note2 note2note2 note2note4"
                };

                Note note6 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph2,
                    ParagraphId = paragraph2.Id,
                    Message = "note6note2 note2note2 note2note6"
                };

                Note note7 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph3,
                    ParagraphId = paragraph3.Id,
                    Message = "note7note2 note2note2 note2note7"
                };

                Note note8 = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = GetRandomDate(DateTime.Now),
                    EditDate = GetRandomDate(DateTime.Now),
                    IsDeleted = 0,
                    OwnerId = user1Id,
                    Paragraph = paragraph3,
                    ParagraphId = paragraph3.Id,
                    Message = "note8note2 note2note2 note2note8"
                };
                context.Notes.AddRange(note1, note2, note3, note4, note5, note6, note7, note8);

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }

        private static DateTime GetRandomDate(DateTime startingDate)
        {
            Random randomMonth = new();
            Random randomDays = new();
            Random randomMinuts = new Random();
            return startingDate.AddMonths(-randomMonth.Next(1, 10))
                .AddDays(-randomDays.Next(1, 25))
                .AddMinutes(-randomMinuts.Next(1, 12000));
        }
    }
}
