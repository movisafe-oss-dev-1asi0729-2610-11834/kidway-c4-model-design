using Structurizr;

namespace kidway_c4_model_design
{
    public class AnalyticsReportsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AnalyticsReportsComponent";

        public Component reports_controller { get; private set; }
        public Component analytics_controller { get; private set; }
        public Component reports_service { get; private set; }
        public Component analytics_service { get; private set; }
        public Component reports_repository { get; private set; }
        public Component reports_entity { get; private set; }

        public AnalyticsReportsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            reports_controller = containerDiagram.rest_api.AddComponent(
                "Reports Controller",
                "Handles requests for exporting operational, fleet, trip, and attendance reports.",
                "Java, Spring Boot REST Controller"
            );

            analytics_controller = containerDiagram.rest_api.AddComponent(
                "Analytics Controller",
                "Handles requests for KPIs, metrics dashboards, trends, and business analytics.",
                "Java, Spring Boot REST Controller"
            );

            reports_service = containerDiagram.rest_api.AddComponent(
                "Reports Service",
                "Coordinates report generation, filtering, exports, and operational summaries.",
                "Java, Spring Service"
            );

            analytics_service = containerDiagram.rest_api.AddComponent(
                "Analytics Service",
                "Calculates indicators for routes, delays, attendance, fleet usage, and service quality.",
                "Java, Spring Service"
            );

            reports_repository = containerDiagram.rest_api.AddComponent(
                "Reports Repository",
                "Reads historical data, metrics, summaries, and reporting sources.",
                "Spring Data JPA Repository"
            );

            reports_entity = containerDiagram.rest_api.AddComponent(
                "Reports Entity",
                "Represents metrics, KPIs, historical summaries, trends, and report outputs.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.transport_company.Uses(
                reports_controller,
                "Exports company reports",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                analytics_controller,
                "Views business metrics",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                reports_controller,
                "Reviews global reports",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                analytics_controller,
                "Monitors global KPIs",
                "JSON/HTTPS"
            );

            reports_controller.Uses(
                reports_service,
                "Delegates reporting logic"
            );

            analytics_controller.Uses(
                analytics_service,
                "Delegates analytics logic"
            );

            reports_service.Uses(
                analytics_service,
                "Requests calculated metrics"
            );

            reports_service.Uses(
                reports_repository,
                "Reads reporting data"
            );

            analytics_service.Uses(
                reports_repository,
                "Reads historical metrics"
            );

            reports_repository.Uses(
                reports_entity,
                "Maps data to reporting model"
            );

            reports_repository.Uses(
                containerDiagram.database,
                "Reads analytics data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#37474F",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            reports_controller.AddTags(componentTag);
            analytics_controller.AddTags(componentTag);
            reports_service.AddTags(componentTag);
            analytics_service.AddTags(componentTag);
            reports_repository.AddTags(componentTag);
            reports_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-analytics-reports",
                "Component Diagram - Analytics & Reports Bounded Context"
            );

            componentView.Title = "KidWay - Analytics & Reports";

            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(reports_controller);
            componentView.Add(analytics_controller);
            componentView.Add(reports_service);
            componentView.Add(analytics_service);
            componentView.Add(reports_repository);
            componentView.Add(reports_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}