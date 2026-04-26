using Structurizr;

namespace kidway_c4_model_design
{
    public class RealTimeTrackingComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "RealTimeTrackingComponent";

        public Component tracking_controller { get; private set; }
        public Component live_map_controller { get; private set; }
        public Component tracking_service { get; private set; }
        public Component eta_service { get; private set; }
        public Component tracking_repository { get; private set; }
        public Component tracking_entity { get; private set; }

        public RealTimeTrackingComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            tracking_controller = containerDiagram.rest_api.AddComponent(
                "Tracking Controller",
                "Handles requests for real-time vehicle tracking and route position updates.",
                "Java, Spring Boot REST Controller"
            );

            live_map_controller = containerDiagram.rest_api.AddComponent(
                "Live Map Controller",
                "Handles requests for live map visualization, current position, and route progress.",
                "Java, Spring Boot REST Controller"
            );

            tracking_service = containerDiagram.rest_api.AddComponent(
                "Tracking Service",
                "Coordinates GPS position updates, route progress, and live trip monitoring.",
                "Java, Spring Service"
            );

            eta_service = containerDiagram.rest_api.AddComponent(
                "ETA Service",
                "Calculates estimated arrival times, delays, and route progress predictions.",
                "Java, Spring Service"
            );

            tracking_repository = containerDiagram.rest_api.AddComponent(
                "Tracking Repository",
                "Reads and writes live location records, route positions, and tracking history.",
                "Spring Data JPA Repository"
            );

            tracking_entity = containerDiagram.rest_api.AddComponent(
                "Tracking Entity",
                "Represents trip location data, coordinates, timestamps, route progress, and ETA.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                tracking_controller,
                "Views personal trip tracking",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                live_map_controller,
                "Monitors fleet in real time",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                live_map_controller,
                "Monitors platform tracking activity",
                "JSON/HTTPS"
            );

            tracking_controller.Uses(
                tracking_service,
                "Delegates tracking logic"
            );

            live_map_controller.Uses(
                tracking_service,
                "Requests live tracking data"
            );

            tracking_service.Uses(
                eta_service,
                "Requests ETA calculations"
            );

            tracking_service.Uses(
                tracking_repository,
                "Persists tracking data"
            );

            eta_service.Uses(
                tracking_repository,
                "Reads route progress data"
            );

            tracking_repository.Uses(
                tracking_entity,
                "Maps data to tracking model"
            );

            tracking_repository.Uses(
                containerDiagram.database,
                "Reads and writes tracking data",
                "SQL"
            );

            tracking_service.Uses(
                contextDiagram.gps_tracking,
                "Receives live GPS coordinates",
                "JSON/HTTPS"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#00897B",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            tracking_controller.AddTags(componentTag);
            live_map_controller.AddTags(componentTag);
            tracking_service.AddTags(componentTag);
            eta_service.AddTags(componentTag);
            tracking_repository.AddTags(componentTag);
            tracking_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-real-time-tracking",
                "Component Diagram - Real-Time Tracking Bounded Context"
            );

            componentView.Title = "KidWay - Real-Time Tracking";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(tracking_controller);
            componentView.Add(live_map_controller);
            componentView.Add(tracking_service);
            componentView.Add(eta_service);
            componentView.Add(tracking_repository);
            componentView.Add(tracking_entity);

            componentView.Add(contextDiagram.gps_tracking);
            componentView.Add(containerDiagram.database);
        }
    }
}