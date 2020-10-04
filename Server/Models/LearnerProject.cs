using System;
using System.Linq;
namespace AgoraAcademy.AgoraEgo.Server.Models
{
    /// <summary>
    /// 代表学习者项目的数据模型
    /// </summary>
    public class LearnerProject
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        public string ProjectInfo { get; set; }

        /// <summary>
        /// 项目参与者ID数组
        /// </summary>
        public int[] ProjectCollaborators
        {
            get
            {
                try
                {
                    return Collaborators?.Split(' ')?.Select((str) => int.Parse(str))?.ToArray();
                }
                catch
                {

                }
                return new int[0];
            }
            set
            {
                Collaborators = string.Join(' ', value);
            }
        }

        /// <summary>
        /// 项目参与者ID数组合并的字符串，用于在数据库储存
        /// </summary>
        public string Collaborators { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public int ProjectOwner { get; set; }

        /// <summary>
        /// 项目指导导师
        /// </summary>
        public int ProjectMentor { get; set; }
    }
}


