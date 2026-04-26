using Structurizr;

namespace kidway_c4_model_design
{
    public class RouteManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "RouteManagementComponent";

        public Component route_controller { get; private set; }
        public Component schedule_controller { get; private set; }
        public Component route_service { get; private set; }
        public Component schedule_service { get; private set; }
        public Component route_repository { get; private set; }
        public Component route_entity { get; private set; }

        public RouteManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            route_controller = containerDiagram.rest_api.AddComponent(
                "Route Controller",
                "Handles requests for creating, updating, and reviewing school transport routes.",
                "Java, Spring Boot REST Controller"
            );

            schedule_controller = containerDiagram.rest_api.AddComponent(
                "Schedule Controller",
                "Handles requests for route schedules, pickup times, and stop planning.",
                "Java, Spring Boot REST Controller"
            );

            route_service = containerDiagram.rest_api.AddComponent(
                "Route Service",
                "Applies route rules such as stops, capacity, coverage area, and optimization.",
                "Java, Spring Service"
            );

            schedule_service = containerDiagram.rest_api.AddComponent(
                "Schedule Service",
                "Coordinates route schedules, calendars, and daily operation times.",
                "Java, Spring Service"
            );

            route_repository = containerDiagram.rest_api.AddComponent(
                "Route Repository",
                "Reads and writes route records, stops, schedules, and status data.",
                "Spring Data JPA Repository"
            );

            route_entity = containerDiagram.rest_api.AddComponent(
                "Route Entity",
                "Represents route data, stops, timetable, assigned vehicle, and operational status.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                route_controller,
                "Manages personal routes",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                schedule_controller,
                "Manages trip schedules",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                route_controller,
                "Manages company routes",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                schedule_controller,
                "Plans route schedules",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                route_controller,
                "Reviews route records",
                "JSON/HTTPS"
            );

            route_controller.Uses(
                route_service,
                "Delegates route logic"
            );

            schedule_controller.Uses(
                schedule_service,
                "Delegates schedule logic"
            );

            route_service.Uses(
                route_repository,
                "Persists route data"
            );

            schedule_service.Uses(
                route_repository,
                "Reads schedule data"
            );

            route_repository.Uses(
                route_entity,
                "Maps data to route model"
            );

            route_repository.Uses(
                containerDiagram.database,
                "Reads and writes route data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#00838F",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            route_controller.AddTags(componentTag);
            schedule_controller.AddTags(componentTag);
            route_service.AddTags(componentTag);
            schedule_service.AddTags(componentTag);
            route_repository.AddTags(componentTag);
            route_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-route-management",
                "Component Diagram - Route Management Bounded Context"
            );

            componentView.Title = "KidWay - Route Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(route_controller);
            componentView.Add(schedule_controller);
            componentView.Add(route_service);
            componentView.Add(schedule_service);
            componentView.Add(route_repository);
            componentView.Add(route_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}