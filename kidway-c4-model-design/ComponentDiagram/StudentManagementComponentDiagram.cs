using Structurizr;

namespace kidway_c4_model_design
{
    public class StudentManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "StudentManagementComponent";

        public Component student_controller { get; private set; }
        public Component guardian_controller { get; private set; }
        public Component student_service { get; private set; }
        public Component guardian_service { get; private set; }
        public Component student_repository { get; private set; }
        public Component student_entity { get; private set; }

        public StudentManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            student_controller = containerDiagram.rest_api.AddComponent(
                "Student Controller",
                "Handles requests for registering, updating, and reviewing student records.",
                "Java, Spring Boot REST Controller"
            );

            guardian_controller = containerDiagram.rest_api.AddComponent(
                "Guardian Controller",
                "Handles requests for managing guardians, contacts, and pickup authorization data.",
                "Java, Spring Boot REST Controller"
            );

            student_service = containerDiagram.rest_api.AddComponent(
                "Student Service",
                "Applies student management rules such as school data, pickup points, status, and assigned transport service.",
                "Java, Spring Service"
            );

            guardian_service = containerDiagram.rest_api.AddComponent(
                "Guardian Service",
                "Coordinates guardian contact data, authorization rules, and emergency contact information.",
                "Java, Spring Service"
            );

            student_repository = containerDiagram.rest_api.AddComponent(
                "Student Repository",
                "Reads and writes student, guardian, pickup point, and school transport data.",
                "Spring Data JPA Repository"
            );

            student_entity = containerDiagram.rest_api.AddComponent(
                "Student Entity",
                "Represents student data, school information, guardian contact, pickup point, and transport status.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                student_controller,
                "Manages assigned students",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                guardian_controller,
                "Manages guardian contacts",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                student_controller,
                "Manages company student records",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                guardian_controller,
                "Reviews guardian information",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                student_controller,
                "Reviews student records",
                "JSON/HTTPS"
            );

            student_controller.Uses(
                student_service,
                "Delegates student logic"
            );

            guardian_controller.Uses(
                guardian_service,
                "Delegates guardian logic"
            );

            student_service.Uses(
                student_repository,
                "Persists student data"
            );

            guardian_service.Uses(
                student_repository,
                "Persists guardian data"
            );

            student_repository.Uses(
                student_entity,
                "Maps data to student model"
            );

            student_repository.Uses(
                containerDiagram.database,
                "Reads and writes student data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#AD6C00",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            student_controller.AddTags(componentTag);
            guardian_controller.AddTags(componentTag);
            student_service.AddTags(componentTag);
            guardian_service.AddTags(componentTag);
            student_repository.AddTags(componentTag);
            student_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-student-management",
                "Component Diagram - Student Management Bounded Context"
            );

            componentView.Title = "KidWay - Student Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(student_controller);
            componentView.Add(guardian_controller);
            componentView.Add(student_service);
            componentView.Add(guardian_service);
            componentView.Add(student_repository);
            componentView.Add(student_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}