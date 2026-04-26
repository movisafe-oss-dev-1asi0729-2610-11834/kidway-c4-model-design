using Structurizr;

namespace kidway_c4_model_design
{
    public class AssignmentManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AssignmentManagementComponent";

        public Component assignment_controller { get; private set; }
        public Component validation_controller { get; private set; }
        public Component assignment_service { get; private set; }
        public Component validation_service { get; private set; }
        public Component assignment_repository { get; private set; }
        public Component assignment_entity { get; private set; }

        public AssignmentManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            assignment_controller = containerDiagram.rest_api.AddComponent(
                "Student Route Assignment Controller",
                "Handles requests for assigning students, vehicles, and drivers to routes.",
                "Java, Spring Boot REST Controller"
            );

            validation_controller = containerDiagram.rest_api.AddComponent(
                "Assignment Validation Controller",
                "Handles requests for assignment conflicts, availability, and capacity checks.",
                "Java, Spring Boot REST Controller"
            );

            assignment_service = containerDiagram.rest_api.AddComponent(
                "Student Route Assignment Service",
                "Coordinates operational assignments between students, routes, vehicles, and drivers.",
                "Java, Spring Service"
            );

            validation_service = containerDiagram.rest_api.AddComponent(
                "Assignment Validation Service",
                "Validates route capacity, driver availability, schedule overlap, and vehicle status.",
                "Java, Spring Service"
            );

            assignment_repository = containerDiagram.rest_api.AddComponent(
                "Student Route Assignment Repository",
                "Reads and writes assignment records, capacity status, and route allocations.",
                "Spring Data JPA Repository"
            );

            assignment_entity = containerDiagram.rest_api.AddComponent(
                "Student Route Assignment Entity",
                "Represents the relationship between student, route, driver, vehicle, and assigned schedule.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                assignment_controller,
                "Assigns students to routes",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                assignment_controller,
                "Manages operational assignments",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                validation_controller,
                "Checks assignment availability",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                assignment_controller,
                "Reviews assignment records",
                "JSON/HTTPS"
            );

            assignment_controller.Uses(
                assignment_service,
                "Delegates assignment logic"
            );

            validation_controller.Uses(
                validation_service,
                "Delegates validation logic"
            );

            assignment_service.Uses(
                validation_service,
                "Requests validation rules"
            );

            assignment_service.Uses(
                assignment_repository,
                "Persists assignment data"
            );

            validation_service.Uses(
                assignment_repository,
                "Reads allocation data"
            );

            assignment_repository.Uses(
                assignment_entity,
                "Maps data to assignment model"
            );

            assignment_repository.Uses(
                containerDiagram.database,
                "Reads and writes assignment data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#3949AB",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            assignment_controller.AddTags(componentTag);
            validation_controller.AddTags(componentTag);
            assignment_service.AddTags(componentTag);
            validation_service.AddTags(componentTag);
            assignment_repository.AddTags(componentTag);
            assignment_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-assignment-management",
                "Component Diagram - Assignment Management Bounded Context"
            );

            componentView.Title = "KidWay - Assignment Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(assignment_controller);
            componentView.Add(validation_controller);
            componentView.Add(assignment_service);
            componentView.Add(validation_service);
            componentView.Add(assignment_repository);
            componentView.Add(assignment_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}