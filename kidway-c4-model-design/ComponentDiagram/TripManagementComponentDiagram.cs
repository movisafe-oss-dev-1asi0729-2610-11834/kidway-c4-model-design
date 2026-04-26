using Structurizr;

namespace kidway_c4_model_design
{
    public class TripManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "TripManagementComponent";

        public Component trip_controller { get; private set; }
        public Component trip_status_controller { get; private set; }
        public Component trip_service { get; private set; }
        public Component trip_status_service { get; private set; }
        public Component trip_repository { get; private set; }
        public Component trip_entity { get; private set; }

        public TripManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            trip_controller = containerDiagram.rest_api.AddComponent(
                "Trip Controller",
                "Handles requests for creating, starting, completing, and reviewing school transport trips.",
                "Java, Spring Boot REST Controller"
            );

            trip_status_controller = containerDiagram.rest_api.AddComponent(
                "Trip Status Controller",
                "Handles requests for updating trip progress, delay status, and operational state.",
                "Java, Spring Boot REST Controller"
            );

            trip_service = containerDiagram.rest_api.AddComponent(
                "Trip Service",
                "Coordinates trip execution using assigned route, vehicle, driver, and student information.",
                "Java, Spring Service"
            );

            trip_status_service = containerDiagram.rest_api.AddComponent(
                "Trip Status Service",
                "Applies trip state rules such as scheduled, in progress, delayed, completed, or cancelled.",
                "Java, Spring Service"
            );

            trip_repository = containerDiagram.rest_api.AddComponent(
                "Trip Repository",
                "Reads and writes trip records, execution status, route references, and operation history.",
                "Spring Data JPA Repository"
            );

            trip_entity = containerDiagram.rest_api.AddComponent(
                "Trip Entity",
                "Represents trip data, assigned route, vehicle, driver, schedule, status, and execution timestamps.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                trip_controller,
                "Manages personal trips",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                trip_status_controller,
                "Updates trip status",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                trip_controller,
                "Supervises company trips",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                trip_status_controller,
                "Monitors trip progress",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                trip_controller,
                "Reviews trip records",
                "JSON/HTTPS"
            );

            trip_controller.Uses(
                trip_service,
                "Delegates trip logic"
            );

            trip_status_controller.Uses(
                trip_status_service,
                "Delegates status logic"
            );

            trip_service.Uses(
                trip_status_service,
                "Requests trip state validation"
            );

            trip_service.Uses(
                trip_repository,
                "Persists trip data"
            );

            trip_status_service.Uses(
                trip_repository,
                "Reads trip state data"
            );

            trip_repository.Uses(
                trip_entity,
                "Maps data to trip model"
            );

            trip_repository.Uses(
                containerDiagram.database,
                "Reads and writes trip data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#00695C",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            trip_controller.AddTags(componentTag);
            trip_status_controller.AddTags(componentTag);
            trip_service.AddTags(componentTag);
            trip_status_service.AddTags(componentTag);
            trip_repository.AddTags(componentTag);
            trip_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-trip-management",
                "Component Diagram - Trip Management Bounded Context"
            );

            componentView.Title = "KidWay - Trip Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(trip_controller);
            componentView.Add(trip_status_controller);
            componentView.Add(trip_service);
            componentView.Add(trip_status_service);
            componentView.Add(trip_repository);
            componentView.Add(trip_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}