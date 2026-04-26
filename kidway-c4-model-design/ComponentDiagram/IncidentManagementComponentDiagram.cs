using Structurizr;

namespace kidway_c4_model_design
{
    public class IncidentManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "IncidentManagementComponent";

        public Component incident_controller { get; private set; }
        public Component emergency_controller { get; private set; }
        public Component incident_service { get; private set; }
        public Component emergency_service { get; private set; }
        public Component incident_repository { get; private set; }
        public Component incident_entity { get; private set; }

        public IncidentManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            incident_controller = containerDiagram.rest_api.AddComponent(
                "Incident Controller",
                "Handles requests for reporting and reviewing operational transport incidents.",
                "Java, Spring Boot REST Controller"
            );

            emergency_controller = containerDiagram.rest_api.AddComponent(
                "Emergency Controller",
                "Handles requests for urgent incidents, route emergencies, and critical alerts.",
                "Java, Spring Boot REST Controller"
            );

            incident_service = containerDiagram.rest_api.AddComponent(
                "Incident Service",
                "Coordinates incident records, operational follow-up, evidence, and resolution workflows.",
                "Java, Spring Service"
            );

            emergency_service = containerDiagram.rest_api.AddComponent(
                "Emergency Service",
                "Applies escalation rules for emergencies, student risk, delays, and severe incidents.",
                "Java, Spring Service"
            );

            incident_repository = containerDiagram.rest_api.AddComponent(
                "Incident Repository",
                "Reads and writes incidents, evidence, resolution status, and operational history.",
                "Spring Data JPA Repository"
            );

            incident_entity = containerDiagram.rest_api.AddComponent(
                "Incident Entity",
                "Represents incidents, severity level, evidence, route reference, status, and timestamps.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                incident_controller,
                "Reports incidents",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                incident_controller,
                "Reviews company incidents",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                emergency_controller,
                "Receives emergency incidents",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                incident_controller,
                "Reviews global incidents",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                emergency_controller,
                "Monitors emergencies",
                "JSON/HTTPS"
            );

            incident_controller.Uses(
                incident_service,
                "Delegates incident logic"
            );

            emergency_controller.Uses(
                emergency_service,
                "Delegates emergency logic"
            );

            incident_service.Uses(
                emergency_service,
                "Requests escalation validation"
            );

            incident_service.Uses(
                incident_repository,
                "Persists incident data"
            );

            emergency_service.Uses(
                incident_repository,
                "Reads incident severity data"
            );

            incident_repository.Uses(
                incident_entity,
                "Maps data to incident model"
            );

            incident_repository.Uses(
                containerDiagram.database,
                "Reads and writes incident data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#5D4037",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            incident_controller.AddTags(componentTag);
            emergency_controller.AddTags(componentTag);
            incident_service.AddTags(componentTag);
            emergency_service.AddTags(componentTag);
            incident_repository.AddTags(componentTag);
            incident_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-incident-management",
                "Component Diagram - Incident Management Bounded Context"
            );

            componentView.Title = "KidWay - Incident Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(incident_controller);
            componentView.Add(emergency_controller);
            componentView.Add(incident_service);
            componentView.Add(emergency_service);
            componentView.Add(incident_repository);
            componentView.Add(incident_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}