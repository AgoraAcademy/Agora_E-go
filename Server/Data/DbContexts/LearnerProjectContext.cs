using System;
using AgoraAcademy.AgoraEgo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AgoraAcademy.AgoraEgo.Server.Data.DbContexts
{
    public class LearnerProjectContext : DbContext
    {
        public DbSet<LearnerProject> Projects { get; set; }  

        public LearnerProjectContext(DbContextOptions<LearnerProjectContext> options) : base(options)
        {

        }
    }
}
