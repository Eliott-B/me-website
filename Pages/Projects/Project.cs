using System.Xml;

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
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public ProjectType Type { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

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

        public static List<Project> LoadProjects(string xmlContent)
        {
            List<Project> projects = new List<Project>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);
                XmlNodeList projectsList = xmlDoc.GetElementsByTagName("project");
                if (projectsList.Count == 0)
                {
                    throw new FileNotFoundException("Aucun projet trouvé dans le fichier");
                }
                foreach (XmlNode project in projectsList)
                {
                    XmlNode? title = project.SelectSingleNode("name");
                    XmlNode? image = project.SelectSingleNode("image");
                    XmlNode? description = project.SelectSingleNode("description");
                    XmlNode? page = project.SelectSingleNode("page");
                    XmlNode? type = project.SelectSingleNode("type");
                    XmlNode? startDate = project.SelectSingleNode("date-start");
                    XmlNode? endDate = project.SelectSingleNode("date-end");
                    if (title == null || image == null || description == null || page == null || type == null || startDate == null || endDate == null)
                    {
                        throw new XmlException("Erreur de formatage du fichier XML");
                    }
                    projects.Add(new Project
                    {
                        Title = title.InnerText,
                        Image = image.InnerText,
                        Description = description.InnerText,
                        Link = page.InnerText,
                        Type = StringToProjectType(type.InnerText),
                        StartDate = startDate.InnerText,
                        EndDate = endDate.InnerText
                    });
                }
            }
            catch (XmlException ex)
            {
                throw new Exception("Erreur lors du chargement des projets : erreur de formatage du fichier XML", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement des projets", ex);
            }

            return projects;
        }
    }
}
