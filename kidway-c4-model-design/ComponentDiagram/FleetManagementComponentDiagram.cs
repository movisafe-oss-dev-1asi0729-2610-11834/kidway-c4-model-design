using Structurizr;

namespace kidway_c4_model_design
{
    public class FleetManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "FleetManagementComponent";

        public Component vehicle_controller { get; private set; }
        public Component fleet_controller { get; private set; }
        public Component vehicle_service { get; private set; }
        public Component fleet_service { get; private set; }
        public Component vehicle_repository { get; private set; }
        public Component vehicle_entity { get; private set; }

        public FleetManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            vehicle_controller = containerDiagram.rest_api.AddComponent(
                "Vehicle Controller",
                "Handles requests for registering, updating, and reviewing school transport vehicles.",
                "Java, Spring Boot REST Controller"
            );

            fleet_controller = containerDiagram.rest_api.AddComponent(
                "Fleet Controller",
                "Handles requests for fleet availability, vehicle status, and operational fleet summaries.",
                "Java, Spring Boot REST Controller"
            );

            vehicle_service = containerDiagram.rest_api.AddComponent(
                "Vehicle Service",
                "Applies vehicle rules such as capacity, availability, ownership, and operational status.",
                "Java, Spring Service"
            );

            fleet_service = containerDiagram.rest_api.AddComponent(
                "Fleet Service",
                "Coordinates fleet information for operators and transport companies.",
                "Java, Spring Service"
            );

            vehicle_repository = containerDiagram.rest_api.AddComponent(
                "Vehicle Repository",
                "Reads and writes vehicle, fleet, capacity, and availability data.",
                "Spring Data JPA Repository"
            );

            vehicle_entity = containerDiagram.rest_api.AddComponent(
                "Vehicle Entity",
                "Represents school transport vehicle data, capacity, plate number, status, and company association.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                vehicle_controller,
                "Manages personal vehicle",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                fleet_controller,
                "Reviews vehicle status",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                vehicle_controller,
                "Manages company vehicles",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                fleet_controller,
                "Monitors fleet availability",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                fleet_controller,
                "Reviews fleet records",
                "JSON/HTTPS"
            );

            vehicle_controller.Uses(
                vehicle_service,
                "Delegates vehicle logic"
            );

            fleet_controller.Uses(
                fleet_service,
                "Delegates fleet logic"
            );

            vehicle_service.Uses(
                vehicle_repository,
                "Persists vehicle data"
            );

            fleet_service.Uses(
                vehicle_repository,
                "Reads fleet data"
            );

            vehicle_repository.Uses(
                vehicle_entity,
                "Maps data to vehicle model"
            );

            vehicle_repository.Uses(
                containerDiagram.database,
                "Reads and writes fleet data",
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
            vehicle_controller.AddTags(componentTag);
            fleet_controller.AddTags(componentTag);
            vehicle_service.AddTags(componentTag);
            fleet_service.AddTags(componentTag);
            vehicle_repository.AddTags(componentTag);
            vehicle_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-fleet-management",
                "Component Diagram - Fleet Management Bounded Context"
            );

            componentView.Title = "KidWay - Fleet Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(vehicle_controller);
            componentView.Add(fleet_controller);
            componentView.Add(vehicle_service);
            componentView.Add(fleet_service);
            componentView.Add(vehicle_repository);
            componentView.Add(vehicle_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}