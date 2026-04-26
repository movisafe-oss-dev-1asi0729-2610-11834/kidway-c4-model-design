using Structurizr;

namespace kidway_c4_model_design
{
    public class DriverManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "DriverManagementComponent";

        public Component driver_controller { get; private set; }
        public Component assignment_controller { get; private set; }
        public Component driver_service { get; private set; }
        public Component assignment_service { get; private set; }
        public Component driver_repository { get; private set; }
        public Component driver_entity { get; private set; }

        public DriverManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            driver_controller = containerDiagram.rest_api.AddComponent(
                "Driver Controller",
                "Handles requests for registering, updating, and reviewing drivers.",
                "Java, Spring Boot REST Controller"
            );

            assignment_controller = containerDiagram.rest_api.AddComponent(
                "Assignment Controller",
                "Handles requests for assigning drivers to vehicles and routes.",
                "Java, Spring Boot REST Controller"
            );

            driver_service = containerDiagram.rest_api.AddComponent(
                "Driver Service",
                "Applies driver validation rules, license status, availability, and profile management.",
                "Java, Spring Service"
            );

            assignment_service = containerDiagram.rest_api.AddComponent(
                "Assignment Service",
                "Coordinates driver assignments for routes, vehicles, and daily operations.",
                "Java, Spring Service"
            );

            driver_repository = containerDiagram.rest_api.AddComponent(
                "Driver Repository",
                "Reads and writes driver records, assignments, and availability data.",
                "Spring Data JPA Repository"
            );

            driver_entity = containerDiagram.rest_api.AddComponent(
                "Driver Entity",
                "Represents driver data, license, contact information, status, and company association.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.transport_company.Uses(
                driver_controller,
                "Manages company drivers",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                assignment_controller,
                "Assigns drivers to operations",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                driver_controller,
                "Reviews driver records",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                assignment_controller,
                "Audits assignments",
                "JSON/HTTPS"
            );

            driver_controller.Uses(
                driver_service,
                "Delegates driver logic"
            );

            assignment_controller.Uses(
                assignment_service,
                "Delegates assignment logic"
            );

            driver_service.Uses(
                driver_repository,
                "Persists driver data"
            );

            assignment_service.Uses(
                driver_repository,
                "Reads assignment data"
            );

            driver_repository.Uses(
                driver_entity,
                "Maps data to driver model"
            );

            driver_repository.Uses(
                containerDiagram.database,
                "Reads and writes driver data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#5E35B1",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            driver_controller.AddTags(componentTag);
            assignment_controller.AddTags(componentTag);
            driver_service.AddTags(componentTag);
            assignment_service.AddTags(componentTag);
            driver_repository.AddTags(componentTag);
            driver_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-driver-management",
                "Component Diagram - Driver Management Bounded Context"
            );

            componentView.Title = "KidWay - Driver Management";

            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(driver_controller);
            componentView.Add(assignment_controller);
            componentView.Add(driver_service);
            componentView.Add(assignment_service);
            componentView.Add(driver_repository);
            componentView.Add(driver_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}