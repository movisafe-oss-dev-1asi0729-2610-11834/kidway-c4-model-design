using Structurizr;

namespace kidway_c4_model_design
{
    public class DashboardComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "DashboardComponent";

        public Component dashboard_controller { get; private set; }
        public Component overview_controller { get; private set; }
        public Component dashboard_service { get; private set; }
        public Component kpi_service { get; private set; }
        public Component overview_query_service { get; private set; }
        public Component dashboard_repository { get; private set; }

        public DashboardComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            dashboard_controller = containerDiagram.rest_api.AddComponent(
                "Dashboard Controller",
                "Handles requests for KidWay role-based dashboard summaries and operational overview data.",
                "Java, Spring Boot REST Controller"
            );

            overview_controller = containerDiagram.rest_api.AddComponent(
                "Overview Controller",
                "Handles requests for platform status, fleet summaries, route summaries, and alert overviews.",
                "Java, Spring Boot REST Controller"
            );

            dashboard_service = containerDiagram.rest_api.AddComponent(
                "Dashboard Service",
                "Coordinates dashboard information from routes, trips, vehicles, students, alerts, and reports.",
                "Java, Spring Service"
            );

            kpi_service = containerDiagram.rest_api.AddComponent(
                "KPI Service",
                "Calculates operational indicators such as active routes, trip status, fleet usage, incidents, and attendance.",
                "Java, Spring Service"
            );

            overview_query_service = containerDiagram.rest_api.AddComponent(
                "Overview Query Service",
                "Builds optimized read models for dashboard and overview screens.",
                "Java Query Service"
            );

            dashboard_repository = containerDiagram.rest_api.AddComponent(
                "Dashboard Repository",
                "Reads dashboard metrics, summaries, and historical overview data.",
                "Spring Data JPA Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                dashboard_controller,
                "Views route and trip dashboard",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                dashboard_controller,
                "Views fleet and operations dashboard",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                overview_controller,
                "Views company operation overview",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                overview_controller,
                "Monitors platform overview",
                "JSON/HTTPS"
            );

            dashboard_controller.Uses(
                dashboard_service,
                "Delegates dashboard logic"
            );

            overview_controller.Uses(
                dashboard_service,
                "Requests overview data"
            );

            dashboard_service.Uses(
                kpi_service,
                "Calculates dashboard indicators"
            );

            dashboard_service.Uses(
                overview_query_service,
                "Builds dashboard read models"
            );

            kpi_service.Uses(
                dashboard_repository,
                "Reads metric source data"
            );

            overview_query_service.Uses(
                dashboard_repository,
                "Reads overview source data"
            );

            dashboard_repository.Uses(
                containerDiagram.database,
                "Reads dashboard and overview data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#455A64",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            dashboard_controller.AddTags(componentTag);
            overview_controller.AddTags(componentTag);
            dashboard_service.AddTags(componentTag);
            kpi_service.AddTags(componentTag);
            overview_query_service.AddTags(componentTag);
            dashboard_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-dashboard",
                "Component Diagram - Dashboard Bounded Context"
            );

            componentView.Title = "KidWay - Dashboard";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(dashboard_controller);
            componentView.Add(overview_controller);
            componentView.Add(dashboard_service);
            componentView.Add(kpi_service);
            componentView.Add(overview_query_service);
            componentView.Add(dashboard_repository);

            componentView.Add(containerDiagram.database);
        }
    }
}