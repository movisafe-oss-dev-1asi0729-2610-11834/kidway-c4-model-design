using Structurizr;

namespace kidway_c4_model_design
{
    public class AttendanceTrackingComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AttendanceTrackingComponent";

        public Component attendance_controller { get; private set; }
        public Component boarding_controller { get; private set; }
        public Component attendance_service { get; private set; }
        public Component boarding_service { get; private set; }
        public Component attendance_repository { get; private set; }
        public Component attendance_entity { get; private set; }

        public AttendanceTrackingComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            attendance_controller = containerDiagram.rest_api.AddComponent(
                "Attendance Controller",
                "Handles requests for reviewing student attendance records during school transport trips.",
                "Java, Spring Boot REST Controller"
            );

            boarding_controller = containerDiagram.rest_api.AddComponent(
                "Boarding Controller",
                "Handles requests for marking student boarding, drop-off, absence, and transport confirmation.",
                "Java, Spring Boot REST Controller"
            );

            attendance_service = containerDiagram.rest_api.AddComponent(
                "Attendance Service",
                "Coordinates attendance records for students, routes, trips, and daily transport operations.",
                "Java, Spring Service"
            );

            boarding_service = containerDiagram.rest_api.AddComponent(
                "Boarding Service",
                "Applies boarding and drop-off rules to validate student pickup and arrival status.",
                "Java, Spring Service"
            );

            attendance_repository = containerDiagram.rest_api.AddComponent(
                "Attendance Repository",
                "Reads and writes student attendance, boarding, drop-off, absence, and confirmation records.",
                "Spring Data JPA Repository"
            );

            attendance_entity = containerDiagram.rest_api.AddComponent(
                "Attendance Entity",
                "Represents student attendance data, trip reference, boarding status, drop-off status, and timestamps.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                boarding_controller,
                "Marks student boarding and drop-off",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                attendance_controller,
                "Reviews attendance records",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                attendance_controller,
                "Monitors student attendance",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                boarding_controller,
                "Reviews boarding activity",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                attendance_controller,
                "Reviews attendance records",
                "JSON/HTTPS"
            );

            attendance_controller.Uses(
                attendance_service,
                "Delegates attendance logic"
            );

            boarding_controller.Uses(
                boarding_service,
                "Delegates boarding logic"
            );

            attendance_service.Uses(
                attendance_repository,
                "Persists attendance data"
            );

            boarding_service.Uses(
                attendance_service,
                "Updates attendance state"
            );

            boarding_service.Uses(
                attendance_repository,
                "Reads boarding data"
            );

            attendance_repository.Uses(
                attendance_entity,
                "Maps data to attendance model"
            );

            attendance_repository.Uses(
                containerDiagram.database,
                "Reads and writes attendance data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#6A1B9A",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            attendance_controller.AddTags(componentTag);
            boarding_controller.AddTags(componentTag);
            attendance_service.AddTags(componentTag);
            boarding_service.AddTags(componentTag);
            attendance_repository.AddTags(componentTag);
            attendance_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-attendance-tracking",
                "Component Diagram - Attendance Tracking Bounded Context"
            );

            componentView.Title = "KidWay - Attendance Tracking";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(attendance_controller);
            componentView.Add(boarding_controller);
            componentView.Add(attendance_service);
            componentView.Add(boarding_service);
            componentView.Add(attendance_repository);
            componentView.Add(attendance_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}