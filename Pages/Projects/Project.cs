namespace Portfolio.Project
{
    public enum ProjectType
    {
        Personnal,
        School,
        Professional,
        Unknown
    }

    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public ProjectType Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public static string ProjectTypeToString(ProjectType type)
        {
            return type switch
            {
                ProjectType.Personnal => "Projet personnel",
                ProjectType.School => "Projet scolaire",
                ProjectType.Professional => "Projet professionnel",
                _ => "Projet"
            };
        }

        public static ProjectType StringToProjectType(string type)
        {
            return type switch
            {
                "Personnel" => ProjectType.Personnal,
                "Scolaire" => ProjectType.School,
                "Professionel" => ProjectType.Professional,
                _ => ProjectType.Unknown
            };
        }
    }
}
