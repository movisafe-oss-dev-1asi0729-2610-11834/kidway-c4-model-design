using Structurizr;

namespace kidway_c4_model_design
{
    public class CompanyManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "CompanyManagementComponent";

        public Component company_controller { get; private set; }
        public Component settings_controller { get; private set; }
        public Component company_service { get; private set; }
        public Component settings_service { get; private set; }
        public Component company_repository { get; private set; }
        public Component company_entity { get; private set; }

        public CompanyManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddComponents()
        {
            company_controller = containerDiagram.rest_api.AddComponent(
                "Company Controller",
                "Handles requests for company profiles, branches, contact data, and legal information.",
                "Java, Spring Boot REST Controller"
            );

            settings_controller = containerDiagram.rest_api.AddComponent(
                "Settings Controller",
                "Handles requests for business rules, operational settings, and organization preferences.",
                "Java, Spring Boot REST Controller"
            );

            company_service = containerDiagram.rest_api.AddComponent(
                "Company Service",
                "Coordinates company records, branch information, and organizational data.",
                "Java, Spring Service"
            );

            settings_service = containerDiagram.rest_api.AddComponent(
                "Settings Service",
                "Applies configuration rules for schedules, branding, notifications, and operations.",
                "Java, Spring Service"
            );

            company_repository = containerDiagram.rest_api.AddComponent(
                "Company Repository",
                "Reads and writes company records, branches, settings, and organization data.",
                "Spring Data JPA Repository"
            );

            company_entity = containerDiagram.rest_api.AddComponent(
                "Company Entity",
                "Represents company profile, tax data, branches, preferences, and operational settings.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.transport_company.Uses(
                company_controller,
                "Manages company profile",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                settings_controller,
                "Configures business settings",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                company_controller,
                "Reviews companies",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                settings_controller,
                "Applies platform policies",
                "JSON/HTTPS"
            );

            company_controller.Uses(
                company_service,
                "Delegates company logic"
            );

            settings_controller.Uses(
                settings_service,
                "Delegates settings logic"
            );

            company_service.Uses(
                company_repository,
                "Persists company data"
            );

            settings_service.Uses(
                company_repository,
                "Reads organization data"
            );

            company_repository.Uses(
                company_entity,
                "Maps data to company model"
            );

            company_repository.Uses(
                containerDiagram.database,
                "Reads and writes company data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#3E2723",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            company_controller.AddTags(componentTag);
            settings_controller.AddTags(componentTag);
            company_service.AddTags(componentTag);
            settings_service.AddTags(componentTag);
            company_repository.AddTags(componentTag);
            company_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-company-management",
                "Component Diagram - Company Management Bounded Context"
            );

            componentView.Title = "KidWay - Company Management";

            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(company_controller);
            componentView.Add(settings_controller);
            componentView.Add(company_service);
            componentView.Add(settings_service);
            componentView.Add(company_repository);
            componentView.Add(company_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}